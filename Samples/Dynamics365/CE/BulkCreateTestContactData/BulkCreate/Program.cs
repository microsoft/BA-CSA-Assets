/*================================================================================================================================

  This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.  

  THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
  INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.  

  We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object 
  code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market Your software 
  product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the 
  Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims 
  or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code.

 =================================================================================================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Pfe.Xrm;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using System.Net;
using Microsoft.Xrm.Sdk.Client;
using System.Diagnostics;
using System.ServiceModel;
using System.IO;
using System.Threading;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Messages;
using System.Runtime.InteropServices;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Pfe.Xrm.Diagnostics;
using Microsoft.Pfe.Xrm.ConsoleListener;
using OptimizedDelete;
using Microsoft.Xrm.Tooling.Connector;
using RandomNameGen;
using Microsoft.Xrm.Sdk.Metadata;
using System.Security.Cryptography;

namespace BulkCreateTestContactData
{
    class Program
    {
        static string _dateTimeFormatString = "MM/dd/yy HH:mm:ss.ffff";

        static string _userNameContext = ""; //can also work with federated domains

        static string _clientId = ""; //can also work with federated domains

        static string _urlText = "https://org.crm.dynamics.com/";

        static Uri _instanceUri = null;

        static int _maxItemsToRetrieveMultiple_PerLoop = 10; //max # of items to retrieveMultiple per concurrent deletion batch

        static int _maxDegressOfParallel = -1; //default to use 

        static int _maxDegressOfParallel_Delete = 30; //sometime deletes take longer - limiting concurrency is one way to deal with this 

        static int _maxItemsPerExecuteMultiple = 10; //Usually you never use more than 10 - never use 1000 if you are concerned with perf

        static int _maxItemsPerExecuteMultiple_Delete = 10; //Usually you never use more than 10 - never use 1000 if you are concerned with perf

        static OrganizationServiceManager connectionManager = null;

        static ObservableEventListener _dataEventListener;

        public static void InitWorkingDataEventListener()
        {
            if (_dataEventListener != null)
            {
                _dataEventListener.DisableEvents(XrmCoreEventSource.Log);
                _dataEventListener.Dispose();
            }
            else
            {
                // TODO: Handle filtered log levels..
                // TODO: Configure roll filesize and max archive count
                _dataEventListener = new ObservableEventListener();
                _dataEventListener.EnableEvents(XrmCoreEventSource.Log, System.Diagnostics.Tracing.EventLevel.Informational, Keywords.All);
                _dataEventListener.LogToConsole(new EventTextFormatterForConsole(System.Diagnostics.Tracing.EventLevel.Informational));
            }
        }

        static void Main(string[] args)
        {
            //If you're using an old version of .NET this will enable TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12;

            //this initializes our new ETW Event listener 
            //this will print log entries to the console window
            InitWorkingDataEventListener();

            #region create a new "ThreadSafe" OrganizationServiceManager the CRM instance 
            _urlText = getChangedValueViaConsole(_urlText, $"Press <enter> to connect to: {_urlText}\r\nor enter another url and press enter to continue...");

            _userNameContext = getChangedValueViaConsole(_userNameContext, $"Press <enter> to connect with: {_userNameContext} or enter another username and press enter to continue...");

            _clientId = getChangedValueViaConsole(_userNameContext, $"Press <enter> to connect with: {_userNameContext} or enter another app Id and press enter to continue...");

            _instanceUri = XrmServiceUriFactory.CreateOrganizationServiceUri(_urlText);

            Console.WriteLine($"\r\n===============================");
            Console.WriteLine($"InstanceUrl: {_urlText}");
            Console.WriteLine($"User: {_userNameContext}");
            Console.WriteLine($"===============================\r\n");

            #region apply network optimizations to servicePointManager and WorkerThreadOptimizations
            Console.WriteLine($"===============================");
            Console.WriteLine($"Optimizing .NET connection settings for better throughput");
            Console.WriteLine($"===============================");

            printConnectionSettings();

            optimizeDotNetConnection();

            printConnectionSettings();

            #endregion

            //For CRM online o365 auth -comment out for onprem 
            _userNameContext = String.Empty;
            connectionManager = new OrganizationServiceManager(_instanceUri, _userNameContext, getPassword(), _clientId);
            #endregion

            #region Verify we're connected by printing the CRM version #

            Console.WriteLine("\r\n{0} CRM Version: {1}", DateTime.Now.ToString(_dateTimeFormatString), getCrmInstanceVersionNumber());

            #endregion

            Guid accountId = new Guid("88cea450-cb0c-ea11-a813-000d3a1b1223");
            List<Guid> contacts = createContacts(accountId);

            foreach (Guid contact in contacts)
            {
                List<Guid> oppts = CreateOpportunity(contact, accountId);
                foreach (Guid oppty in oppts)
                {
                    //GetOpportunityActivities(new Guid("3cbbd39d-d3f0-ea11-a815-000d3a33f3c3"));
                    CreateActivitiesForOpportunity(oppty, GenerateSubject(), GenerateDescription());
                }
            }

            createLeads();
            Console.WriteLine("Press any key to quit...");

            Console.ReadKey();
        }

        static void printConnectionSettings()
        {
            int minWorkerThreads = 0;
            int minCompletionPortThreads = 0;
            ThreadPool.GetMinThreads(out minWorkerThreads, out minCompletionPortThreads);

            Console.WriteLine("{0} Settings: \n\t[System.Net.MaxConnection Setting: {1}]\n\t[MinWorkerThreads: {2}] \n\t[MinIOCP Threads: {3}]\n\t[Expect100Continue: {4}]",
                DateTime.Now.ToString(_dateTimeFormatString),
                System.Net.ServicePointManager.DefaultConnectionLimit,
                minWorkerThreads,
                minCompletionPortThreads,
                System.Net.ServicePointManager.Expect100Continue);
        }

        static void optimizeDotNetConnection()
        {
            //Change max connections from .NET to a remote service default: 2
            System.Net.ServicePointManager.DefaultConnectionLimit = 65000;
            //Bump up the min threads reserved for this app to ramp connections faster - minWorkerThreads defaults to 4, minIOCP defaults to 4 
            System.Threading.ThreadPool.SetMinThreads(100, 100);
            //Turn off the Expect 100 to continue message - 'true' will cause the caller to wait until it round-trip confirms a connection to the server 
            System.Net.ServicePointManager.Expect100Continue = false;
            //more info on Nagle at WikiPedia - can help perf (helps w/ conn reliability)
            System.Net.ServicePointManager.UseNagleAlgorithm = false;

            //a new twist to existing connections
            var knownServicePointConnection = ServicePointManager.FindServicePoint(connectionManager == null ? _instanceUri : connectionManager.ServiceUri);
            if (knownServicePointConnection != null)
            {
                knownServicePointConnection.ConnectionLimit = System.Net.ServicePointManager.DefaultConnectionLimit;
                knownServicePointConnection.Expect100Continue = System.Net.ServicePointManager.Expect100Continue;
                knownServicePointConnection.UseNagleAlgorithm = System.Net.ServicePointManager.UseNagleAlgorithm;
            }
        }

        static string getCrmInstanceVersionNumber()
        {
            try
            {
                using (var proxy = connectionManager.GetProxy())
                {
                    var versionResponse = proxy.Execute(new RetrieveVersionRequest());
                    return versionResponse["Version"].ToString();
                }
            }
            catch (System.ServiceModel.FaultException<OrganizationServiceFault> fault)
            {
                throw;
            }
        }

        static string getRecsPerSec(Stopwatch t, int numberOfOperations)
        {
            return (numberOfOperations * 1.0 / t.ElapsedMilliseconds * 1000).ToString() + " rec/sec";
        }

        /// <summary>
        /// prompts the user for their password, because we *never* store passwords in files and especially not in code :) 
        /// </summary>
        /// <returns></returns>
        private static string getPassword(bool writeConsoleLine = true)
        {
            string pass = "";
            Console.Write("Enter your password: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }

        private static string getChangedValueViaConsole(string inputValue, string prompt)
        {
            Console.WriteLine($"{prompt}");
            var consolevalue = Console.ReadLine();
            if (!inputValue.Trim().Equals(consolevalue.Trim()) && !string.IsNullOrEmpty(consolevalue.Trim()))
            {
                //value is different and NOT empty or null 
                return consolevalue; 
            }
            else
            {
                return inputValue; 
            }
        }

        public static string GetRandomStringParallel(int minLen, int maxLen)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int length;
            using (var rng = RandomNumberGenerator.Create())
            {
                var lenBytes = new byte[4];
                rng.GetBytes(lenBytes);
                length = minLen + (int)(BitConverter.ToUInt32(lenBytes, 0) % (maxLen - minLen + 1));
                var result = new char[length];
                var buffer = new byte[4];

                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(buffer);
                    int idx = (int)(BitConverter.ToUInt32(buffer, 0) % alphabet.Length);
                    result[i] = alphabet[idx];
                }
                return new string(result);
            }
        }
        static Random rand = new Random();
        
        static Entity GetOpportunityActivities(Guid Opportunity)
        {
            string pageCookie;
            bool IsMoreRecords;
            var sort = new Dictionary<string, CrmServiceClient.LogicalSortOrder>();
            sort.Add("createdon", CrmServiceClient.LogicalSortOrder.Descending);
            List<CrmServiceClient.CrmSearchFilter> search = new List<CrmServiceClient.CrmSearchFilter>();
            //search.Add(CrmServiceClient)
            var activities = connectionManager.GetProxy().GetActivitiesBy("opportunity", Opportunity, null, CrmServiceClient.LogicalSearchOperator.Or,search, sort,  100, 1, null, out pageCookie, out IsMoreRecords, new Guid());
            Entity rtnObject = connectionManager.GetProxy().Retrieve("opportunity", Opportunity, new ColumnSet(true));
            return rtnObject;
        }

        static void CreateActivitiesForOpportunity(Guid Opportunity, string subject, string description)
        {
            var apptList = new List<OrganizationRequest>();
            Random random = new Random(100);
            for (int i = 1; i < 5; i++)
            {
                apptList.Add(new CreateRequest()
                {
                    Target = new Entity("appointment")
                    {
                        Attributes = new AttributeCollection()
                        {
                            new KeyValuePair<string, object>("subject", subject),
                            new KeyValuePair<string, object>("description", description),
                            //new KeyValuePair<string, object>("name",basedOffOpportunityRecord.GetAttributeValue<Money>("estimatedvalue").Value + " Deal for " + DateTime.Now.AddMonths(i)),
                            //new KeyValuePair<string, object>("estimatedvalue",basedOffOpportunityRecord.Attributes["estimatedvalue"]),
                            new KeyValuePair<string, object>("scheduledstart",DateTime.Now.AddDays(i)),
                            //new KeyValuePair<string, object>("parentaccountid", new EntityReference("account",parentAccountId)),
                            new KeyValuePair<string, object>("regardingobjectid", new EntityReference("opportunity",Opportunity))
                        }
                    }
                });
            }
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                connectionManager.ParallelProxy.Execute(apptList, (target, ex) =>
                {
                    Console.WriteLine($"Error encountered during an organizationrequest {ex.Detail.Message}");
                });
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Aggregate exceptions encountered during execution {ae.Flatten().Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered during execution {ex.Message}");
            }
        }
        
        static List<Guid> CreateOpportunity(Guid contactId, Guid parentAccountId)
        {
            var leadsList = new List<OrganizationRequest>();
            //Entity basedOffOpportunityRecord = GetOpportunityActivities(BasedOffOpportunity);
            Random random = new Random(100);  
            for (int i = 1; i < 20; i++)
            {
                decimal estValue = random.Next(1000, 80000);
                leadsList.Add(new CreateRequest()
                {
                    Target = new Entity("opportunity")
                    {
                        Attributes = new AttributeCollection()
                        {
                            new KeyValuePair<string, object>("name", estValue + " Deal for " + DateTime.Now.AddMonths(i)),
                            //new KeyValuePair<string, object>("name",basedOffOpportunityRecord.GetAttributeValue<Money>("estimatedvalue").Value + " Deal for " + DateTime.Now.AddMonths(i)),
                            new KeyValuePair<string, object>("estimatedvalue",new Money(estValue)),
                            new KeyValuePair<string, object>("estimatedclosedate",DateTime.Now.AddMonths(i)),
                            new KeyValuePair<string, object>("parentaccountid", new EntityReference("account",parentAccountId)),
                            new KeyValuePair<string, object>("parentcontactid", new EntityReference("contact",contactId))
                        }
                    }
                });
            }
            List<Guid> ids = new List<Guid>();
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                var response = connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
                {
                    Console.WriteLine($"Error encountered during an organizationrequest {ex.Detail.Message}");
                });
                foreach (var item in response)
                {
                    foreach (var resultResponse in item.Results)
                    {
                        ids.Add(new Guid(resultResponse.Value.ToString()));
                    }
                }
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Aggregate exceptions encountered during execution {ae.Flatten().Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered during execution {ex.Message}");
            }
            return ids;
        }
        static string GenerateJobTitle()
        {
            
            List<string> jobTitle = new List<string>();
            jobTitle.Add("Purchasing Manager");
            jobTitle.Add("CTO");
            jobTitle.Add("CEO");
            jobTitle.Add("Salesperson");
            jobTitle.Add("Delivery Manager");
            jobTitle.Add("Account Manager");
            jobTitle.Add("Purchasing Assistant");
            return jobTitle[rand.Next(0,6)];
        }

        static string GenerateSubject()
        {

            List<string> jobTitle = new List<string>();
            jobTitle.Add("Introduction Email");
            jobTitle.Add("Follow Up");
            jobTitle.Add("Thank You");
            jobTitle.Add("Touchpoint");
            jobTitle.Add("Reply");
            jobTitle.Add("Connection");
            jobTitle.Add("Request");
            return jobTitle[rand.Next(0, 6)];
        }
        static string GenerateDescription()
        {

            List<string> jobTitle = new List<string>();
            jobTitle.Add("Thank for being a valued customer and share details on the interested product/services.");
            jobTitle.Add("Dear Kidd,\n\nThank you for visiting our Web site. We have forwarded your request for additional information to Ali Youssefi, who will be contacting you shortly. If you want to contact Ali Youssefi immediately, you can call his or her direct line at our main phone number.\n\nWe look forward to assisting you and providing you with a world-class experience.");
            jobTitle.Add("Thank you for reaching out to us regarding [specific issue]. We apologize for any inconvenience you've experienced. Our team is actively working to resolve this, and we appreciate your patience. In the meantime, please feel free to [provide additional instructions or information].");
            jobTitle.Add("We're sorry to hear about your recent experience. We value your feedback and would appreciate the opportunity to make things right. Please let us know how we can assist you further.");
            jobTitle.Add("Thank you for your inquiry. The answer to your question is [provide answer]. If you have any more queries, feel free to ask!");
            return jobTitle[rand.Next(0, 4)];
        }
        static List<Guid> createContacts(Guid accountId)
        {
            var leadsList = new List<OrganizationRequest>();
            Random rand = new Random(DateTime.Now.Millisecond); // we need a random variable to select names randomly

            RandomName nameGen = new RandomName(rand); // create a new instance of the RandomName class

            string name = nameGen.Generate(Sex.Male, 1); // generate a male name, with one middal name.
            string secondName = nameGen.Generate(Sex.Female, 2, true); // generate a female name with two middle initials
            //string thridName = nameGen.Generate(Sex.Female); // a female name with no middle names

            List<string> Names = nameGen.RandomNames(100, 2); // generate 100 random names with up to two middle names
            List<string> Boys = nameGen.RandomNames(100, 0, Sex.Male); // generate 100 random boys names
            List<string> Girls = nameGen.RandomNames(100, 2, Sex.Female, true); // 100 girls names with intials
            for (int i = 1; i < 20; i++)
            {
                string thridName = nameGen.Generate(Sex.Female);
                leadsList.Add(new CreateRequest()
                {
                    Target = new Entity("contact")
                    {
                        Attributes = new AttributeCollection()
                        {
                            //new KeyValuePair<string, object>("fullname",thridName),
                            //new KeyValuePair<string, object>("subject",GetRandomString(5, 15)),
                            new KeyValuePair<string, object>("firstname",thridName.Split(' ')[0]),
                            new KeyValuePair<string, object>("lastname",thridName.Split(' ')[1]),
                            new KeyValuePair<string, object>("jobtitle",GenerateJobTitle()),
                            new KeyValuePair<string, object>("parentcustomerid", new EntityReference("account",accountId))
                        }
                    }
                });
            }
            List<Guid> ids = new List<Guid>();
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                var response = connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
                {
                    Console.WriteLine($"Error encountered during an organizationrequest {ex.Detail.Message}");
                });
                
                foreach (var item in response)
                {
                    foreach(var resultResponse in item.Results)
                    {
                        ids.Add(new Guid(resultResponse.Value.ToString()));
                    }
                }
                
                //response.Select(x=>x.Results).ToList().ForEach(x=>ids.Add(x.Select(x=>x.Value)));

            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Aggregate exceptions encountered during execution {ae.Flatten().Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered during execution {ex.Message}");
            }
            return ids;
        }
        static void createLeads()
        {
            var leadsList = new List<OrganizationRequest>();

            for (int i = 1; i < 25; i++)
            {
                leadsList.Add(new CreateRequest()
                {
                    Target = new Entity("lead")
                    {
                        Attributes = new AttributeCollection()
                        {
                            new KeyValuePair<string, object>("subject",GetRandomStringParallel(5, 15)),
                            new KeyValuePair<string, object>("firstname",GetRandomStringParallel(5, 15)),
                            new KeyValuePair<string, object>("lastname",GetRandomStringParallel(5, 15))
                        }
                    }
                });
            }
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
                {
                    Console.WriteLine($"Error encountered during an organizationrequest {ex.Detail.Message}");
                });
            }
            catch(AggregateException ae)
            {
                Console.WriteLine($"Aggregate exceptions encountered during execution {ae.Flatten().Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered during execution {ex.Message}");
            }
            
        }

    }

    public class Helper
    {

        public string GetRandomStringInstance(int minLen, int maxLen)
        {
            char[] Alphabet = ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcefghijklmnopqrstuvwxyz0123456789").ToCharArray();
            Random m_randomInstance = new Random();
            Object m_randLock = new object();

            int alphabetLength = Alphabet.Length;
            int stringLength;
            lock (m_randLock) { stringLength = m_randomInstance.Next(minLen, maxLen); }
            char[] str = new char[stringLength];

            // max length of the randomizer array is 5
            int randomizerLength = stringLength;
            //int randomizerLength = (stringLength > 5) ? 5 : stringLength;

            int[] rndInts = new int[randomizerLength];
            int[] rndIncrements = new int[randomizerLength];

            // Prepare a "randomizing" array
            for (int i = 0; i < randomizerLength; i++)
            {
                int rnd = m_randomInstance.Next(alphabetLength);
                rndInts[i] = rnd;
                rndIncrements[i] = rnd;
            }

            // Generate "random" string out of the alphabet used
            for (int i = 0; i < stringLength; i++)
            {
                int indexRnd = i % randomizerLength;
                int indexAlphabet = rndInts[indexRnd] % alphabetLength;
                str[i] = Alphabet[indexAlphabet];

                // Each rndInt "cycles" characters from the array, 
                // so we have more or less random string as a result
                rndInts[indexRnd] += rndIncrements[indexRnd];
            }

            return (new string(str));
        }

    }
}
