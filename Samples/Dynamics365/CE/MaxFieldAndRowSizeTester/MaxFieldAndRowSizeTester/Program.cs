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
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Pfe.Xrm;
using Microsoft.Pfe.Xrm.ConsoleListener;
using Microsoft.Pfe.Xrm.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;

namespace MaxFieldAndRowSize
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

            //CreateStringAttributeForExistingEntity("creb3_testmaxstringattributes");
            //CreateBooleanAttributeForExistingEntity("creb3_testmaxbooleanattributes");
            //CreateMemoAttributeForExistingEntity("creb3_testmaxmemoattributes");
            //CreateLookupAttributeForExistingEntity("creb3_testmaxlookupattributes");
            //WriteRandomStringOnAllStringFields("creb3_testmaxmemoattributes");
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
            //var knownServicePointConnection = ServicePointManager.FindServicePoint(new Uri("https://pfehandles.crm.dynamics.com"));
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

        #region MetadataGeneration
        static void CreateStringAttributeForExistingEntity(string _publisherPrefix, string _customEntityName)
        {
            Console.WriteLine("Begin CreateAttributesForExistingEntity at " + DateTime.Now.ToString());
            var leadsList = new List<OrganizationRequest>();
            // Create storage for new attributes being created
            var addedAttributes = new List<AttributeMetadata>();
            for (int i = 1; i < 1000; i++)
            {
                string randomString = GetRandomStringParallel(15,20);
                addedAttributes.Add(new StringAttributeMetadata
                    {
                        SchemaName = _publisherPrefix + randomString,
                        RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                        MaxLength = 2000,
                        FormatName = StringFormatName.Text,
                        DisplayName = new Label(GetRandomStringParallel(15, 20), 1033),
                        Description = new Label(GetRandomStringParallel(15, 20), 1033)
                    }
                );
                Console.WriteLine(randomString);
            }
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                foreach (AttributeMetadata columnDefinition in addedAttributes)
                {
                    // Create the request.
                    CreateAttributeRequest request = new CreateAttributeRequest()
                    {
                        EntityName = _customEntityName,
                        Attribute = columnDefinition
                    };

                    // Execute the request.
                    connectionManager.GetProxy().Execute(request);

                    Console.WriteLine($"Created the {columnDefinition.SchemaName} column.");
                }
                connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
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
            // Add some attributes to the Bank Account entity
            //Console.WriteLine(attributeName + " attribute has been added to the " + _customEntityName + " entity.");
            connectionManager.GetProxy().PublishEntity( _customEntityName );
        }
        static void CreateBooleanAttributeForExistingEntity(string _publisherPrefix, string _customEntityName)
        {
            Console.WriteLine("Begin CreateAttributesForExistingEntity at " + DateTime.Now.ToString());
            var leadsList = new List<OrganizationRequest>();
            // Create storage for new attributes being created
            var addedAttributes = new List<AttributeMetadata>();
            for (int i = 1; i < 10000; i++)
            {
                string randomString = GetRandomStringParallel(15, 20);
                addedAttributes.Add(new BooleanAttributeMetadata
                {
                    SchemaName = _publisherPrefix + randomString,
                    DefaultValue = false,
                    RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                    DisplayName = new Label(GetRandomStringParallel(1, 20), 1033),
                    Description = new Label(GetRandomStringParallel(1, 20), 1033),
                    // Set extended properties
                    OptionSet = new BooleanOptionSetMetadata(
            new OptionMetadata(new Label("True", 1033), 1),
            new OptionMetadata(new Label("False", 1033), 0)
            )
                }
                );
                Console.WriteLine(randomString);
            }
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                foreach (AttributeMetadata columnDefinition in addedAttributes)
                {
                    // Create the request.
                    CreateAttributeRequest request = new CreateAttributeRequest()
                    {
                        EntityName = _customEntityName,
                        Attribute = columnDefinition
                    };

                    // Execute the request.
                    connectionManager.GetProxy().Execute(request);

                    Console.WriteLine($"Created the {columnDefinition.SchemaName} column.");
                }
                connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
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
            // Add some attributes to the Bank Account entity
            //Console.WriteLine(attributeName + " attribute has been added to the " + _customEntityName + " entity.");
            connectionManager.GetProxy().PublishEntity(_customEntityName);
        }
        static void CreateMemoAttributeForExistingEntity(string _publisherPrefix, string _customEntityName)
        {
            Console.WriteLine("Begin CreateAttributesForExistingEntity at " + DateTime.Now.ToString());
            var leadsList = new List<OrganizationRequest>();
            // Create storage for new attributes being created
            var addedAttributes = new List<AttributeMetadata>();
            for (int i = 1; i < 10000; i++)
            {
                string randomString = GetRandomStringParallel(15, 20);
                addedAttributes.Add(      // Create a memo column 
                      new MemoAttributeMetadata()
                      {
                          // Set base properties
                          SchemaName = _publisherPrefix + randomString,
                          LogicalName = _publisherPrefix + randomString,
                          DisplayName = new Label("Sample Memo", 1033),
                          RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                          Description = new Label("Memo Attribute", 1033),
                          // Set extended properties
                          Format = StringFormat.TextArea,
                          ImeMode = ImeMode.Disabled,
                          MaxLength = 5000
                      }
                );
                Console.WriteLine(randomString);
            }
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                foreach (AttributeMetadata columnDefinition in addedAttributes)
                {
                    // Create the request.
                    CreateAttributeRequest request = new CreateAttributeRequest()
                    {
                        EntityName = _customEntityName,
                        Attribute = columnDefinition
                    };

                    // Execute the request.
                    connectionManager.GetProxy().Execute(request);

                    Console.WriteLine($"Created the {columnDefinition.SchemaName} column.");
                }
                connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
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
            // Add some attributes to the Bank Account entity
            //Console.WriteLine(attributeName + " attribute has been added to the " + _customEntityName + " entity.");
            connectionManager.GetProxy().PublishEntity(_customEntityName);
        }
        static void WriteRandomStringOnAllStringFields(string _entityName)
        {
            string entityLogicalName = _entityName; // Replace with your entity logical name
            string stringToInsert = GetRandomStringParallel(15, 20); // Replace with the string you want to insert

            // Retrieve entity metadata
            RetrieveEntityRequest retrieveEntityRequest = new RetrieveEntityRequest
            {
                EntityFilters = EntityFilters.Attributes,
                LogicalName = entityLogicalName
            };

            RetrieveEntityResponse retrieveEntityResponse = (RetrieveEntityResponse)connectionManager.GetProxy().Execute(retrieveEntityRequest);
            EntityMetadata entityMetadata = retrieveEntityResponse.EntityMetadata;

            // Get all string fields
            var stringFields = entityMetadata.Attributes
                .Where(attr => attr.AttributeType == AttributeTypeCode.Memo && attr.IsCustomAttribute.Value)
                .Select(attr => attr.LogicalName);

            // Retrieve records of the entity
            QueryExpression query = new QueryExpression(entityLogicalName)
            {
                ColumnSet = new ColumnSet(stringFields.ToArray())
            };

            EntityCollection entities = connectionManager.GetProxy().RetrieveMultiple(query);

            // Update each record
            Entity newEntity = new Entity(_entityName);
            foreach (var field in stringFields)
                {

                newEntity[field] = stringToInsert;
                }
                connectionManager.GetProxy().Create(newEntity);

            Console.WriteLine("String inserted into all string fields successfully.");
        }
        static void CreateLookupAttributeForExistingEntity(string _publisherPrefix, string _customEntityName)
        {
            Console.WriteLine("Begin CreateAttributesForExistingEntity at " + DateTime.Now.ToString());
            var leadsList = new List<OrganizationRequest>();
            // Create storage for new attributes being created
            var addedAttributes = new List<CreateOneToManyRequest>();
            for (int i = 1; i < 10000; i++)
            {
                string randomString = GetRandomStringParallel(15, 20);
                addedAttributes.Add(      // Create a memo column 
                     new CreateOneToManyRequest
                     {
                         OneToManyRelationship =
                                            new OneToManyRelationshipMetadata
                                            {
                                                ReferencedEntity = "account",
                                                ReferencingEntity = "contact",
                                                SchemaName = _publisherPrefix + randomString,
                                                AssociatedMenuConfiguration = new AssociatedMenuConfiguration
                                                {
                                                    Behavior = AssociatedMenuBehavior.UseLabel,
                                                    Group = AssociatedMenuGroup.Details,
                                                    Label = new Label("Account", 1033),
                                                    Order = 10000
                                                },
                                                CascadeConfiguration = new CascadeConfiguration
                                                {
                                                    Assign = CascadeType.NoCascade,
                                                    Delete = CascadeType.RemoveLink,
                                                    Merge = CascadeType.NoCascade,
                                                    Reparent = CascadeType.NoCascade,
                                                    Share = CascadeType.NoCascade,
                                                    Unshare = CascadeType.NoCascade
                                                }
                                            },
                         Lookup = new LookupAttributeMetadata
                         {
                             SchemaName = _publisherPrefix + randomString,
                             DisplayName = new Label("Account Lookup", 1033),
                             RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                             Description = new Label("Sample Lookup", 1033)
                         }
                     }
                );
                Console.WriteLine(randomString);
            }
            connectionManager.ParallelProxy.MaxDegreeOfParallelism = 35;
            try
            {
                foreach (CreateOneToManyRequest columnDefinition in addedAttributes)
                {

                    // Execute the request.
                    connectionManager.GetProxy().Execute(columnDefinition);

                    Console.WriteLine($"Created the {columnDefinition.OneToManyRelationship.SchemaName} column.");
                }
                connectionManager.ParallelProxy.Execute(leadsList, (target, ex) =>
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
            // Add some attributes to the Bank Account entity
            //Console.WriteLine(attributeName + " attribute has been added to the " + _customEntityName + " entity.");
            connectionManager.GetProxy().PublishEntity(_customEntityName);
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
        #endregion
    }

}
