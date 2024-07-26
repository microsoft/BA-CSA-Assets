using Microsoft.PowerPlatform.Dataverse.Client;

namespace Contoso.Dataverse.Samples
{
    public class DataverseClient
    {

        public DataverseClient GetClient(string instanceUrl)
        {
            
            var serviceClient = new ServiceClient(new Uri(instanceUrl), async (string dataverseUri) =>
            {
                return await tokenProvider.GetCachedToken(dataverseUri);
            }, false, _log);
        }

    }
}