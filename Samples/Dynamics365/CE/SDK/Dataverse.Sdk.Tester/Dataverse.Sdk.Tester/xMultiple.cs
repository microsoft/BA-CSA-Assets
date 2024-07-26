using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataverse.Sdk.Tester
{
    public class xMultiple
    {
        /// <summary>
        /// Demonstrates the use of the CreateMultiple Message
        /// </summary>
        /// <param name="service">The authenticated IOrganizationService instance.</param>
        /// <param name="recordsToCreate">A list of records of the same table to create.</param>
        /// <returns>The Guid values of the records created.</returns>
        public Guid[] CreateMultipleExample(IOrganizationService service,
            List<Entity> recordsToCreate)
        {

            // Create an EntityCollection populated with the list of entities.
            EntityCollection entities = new(recordsToCreate)
            {
                // All the records must be for the same table.
                EntityName = recordsToCreate[0].LogicalName
            };

            // Instantiate CreateMultipleRequest
            CreateMultipleRequest createMultipleRequest = new()
            {
                Targets = entities,
            };

            // Send the request
            CreateMultipleResponse createMultipleResponse =
                        (CreateMultipleResponse)service.Execute(createMultipleRequest);

            // Return the Ids of the records created.
            return createMultipleResponse.Ids;
        }
        /// <summary>
        /// Demonstrates the use of the UpdateMultiple message.
        /// </summary>
        /// <param name="service">The authenticated IOrganizationService instance.</param>
        /// <param name="recordsToUpdate">A list of records to create.</param>
        public void UpdateMultipleExample(IOrganizationService service, List<Entity> recordsToUpdate)
        {
            // Create an EntityCollection populated with the list of entities.
            EntityCollection entities = new(recordsToUpdate)
            {
                // All the records must be for the same table.
                EntityName = recordsToUpdate[0].LogicalName
            };

            // Use UpdateMultipleRequest
            UpdateMultipleRequest updateMultipleRequest = new()
            {
                Targets = entities,
            };

            service.Execute(updateMultipleRequest);
        }
    }
}
