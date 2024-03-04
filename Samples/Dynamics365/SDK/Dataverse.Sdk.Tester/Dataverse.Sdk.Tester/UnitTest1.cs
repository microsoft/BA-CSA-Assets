using Microsoft.Azure.ServiceBus.Primitives;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Concurrent;

namespace Dataverse.Sdk.Tester
{
    public class NonTestClass
    {
        public void BasicServiceClientConnection_shouldfail()
        {
            ServiceClient svc = new ServiceClient("");
            svc.Create(new Entity("account"));
        }

        public void BasicServiceClientConnection_CookieAffinitySetToFalse_shouldpass()
        {
            ServiceClient svc = new ServiceClient("");
            svc.EnableAffinityCookie = false;
            svc.Create(new Entity("account"));
        }
        public void BasicServiceClientConnection_CookieAffinitySetToTrue_shouldfail()
        {
            ServiceClient svc = new ServiceClient("");
            svc.EnableAffinityCookie = true;
            svc.Create(new Entity("account"));
        }
    }
    [TestClass]
    public class UnitTest1
    {
        private List<Entity> createBatch(string name, KeyValuePair<string, object> attribute)
        {
            List<Entity> entities = new List<Entity>();
            for(int i = 0; i < 10; i++)
            {
                Entity entity = new Entity(name);
                entity.Attributes = new AttributeCollection();
                entity.Attributes.Add(attribute);
                entities.Add(entity);
            }
            return entities;
        }
        [TestMethod]
        public void TestCreateMultiple()
        {
            ServiceClient svc = new ServiceClient(@"AuthType=OAuth;Username=alyousse@pfecrmonline.onmicrosoft.com;Password=AliIsACE!;Url=https://alyousse.crm.dynamics.com;AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;TokenCacheStorePath=c:\Data\MyTokenCache;LoginPrompt=Yes");
            xMultiple xMultiple = new xMultiple();
            xMultiple.CreateMultipleExample(svc, createBatch("lead", new KeyValuePair<string, object>("topic", "Create Multiple Example")));

        }
        [TestMethod]
        public void TestCustomAPI()
        {
            ServiceClient svc = new ServiceClient(@"AuthType=OAuth;Username=alyousse@pfecrmonline.onmicrosoft.com;Password=AliIsACE!;Url=https://alyousse.crm.dynamics.com;AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;TokenCacheStorePath=c:\Data\MyTokenCache;LoginPrompt=Yes");
            var req = new OrganizationRequest("sample_SendNotificationToAllUsers")
            {
                ["String.Message"] = "Test",
                ["Int32.IconType"] = true,
                ["String.Table"] = "salesliterature",
                ["String.ExternalLink"] = "https://www.bing.com"
            };

            var resp = svc.Execute(req);

        }
        [TestMethod]
        public void TestUpdateMultiple()
        {
            ServiceClient svc = new ServiceClient("");
            xMultiple xMultiple = new xMultiple();
            xMultiple.UpdateMultipleExample(svc, new List<Entity>());
        }
        [TestMethod]
        public void TestMethod1()
        {
            ServiceClient svc = new ServiceClient("");

        }

        public void BasicServiceClientConnection()
        {
            ServiceClient svc = new ServiceClient("");
        }

        /// <summary>
        /// Creates records in parallel
        /// </summary>
        /// <param name="serviceClient">The authenticated ServiceClient instance.</param>
        /// <param name="entityList">The list of entities to create.</param>
        /// <returns>The id values of the created records.</returns>
        static async Task<Guid[]> CreateRecordsInParallel(
            ServiceClient serviceClient,
            List<Entity> entityList)
        {
            ConcurrentBag<Guid> ids = new();

            // Disable affinity cookie
            //serviceClient.EnableAffinityCookie = false;

            var parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism =
                serviceClient.RecommendedDegreesOfParallelism
            };

            await Parallel.ForEachAsync(
                source: entityList,
                parallelOptions: parallelOptions,
                async (entity, token) =>
                {
                    ids.Add(await serviceClient.CreateAsync(entity, token));
                });

            return ids.ToArray();
        }


    }
}