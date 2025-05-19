using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizedDelete
{
    public static class BatchRequestExtensions
    {
        public const int maxBatchSize = 1000;
        public const bool continueSettingDefault = true;
        public const bool returnSettingDefault = true;
        public static IDictionary<string, ExecuteMultipleRequest> AsDeleteChangeHistoryBatches(this IEnumerable<EntityReference> entityReferences, int batchSize)
        {
            var requests = new List<DeleteRecordChangeHistoryRequest>(entityReferences.Count());

            foreach (EntityReference entityRef in entityReferences)
            {
                var request = new DeleteRecordChangeHistoryRequest()
                {
                    Target = entityRef
                };

                requests.Add(request);
            }

            return requests.AsBatches(batchSize, continueSettingDefault, returnSettingDefault);
        }
        public static IDictionary<string, ExecuteMultipleRequest> AsBatches<T>(this IEnumerable<T> requests, int batchSize, bool continueOnError, bool returnResponses)
        where T : OrganizationRequest
        {
            return requests.AsBatches(batchSize, new ExecuteMultipleSettings() { ContinueOnError = continueOnError, ReturnResponses = returnResponses });
        }

        /// <summary>
        /// Converts a collection of type <see cref="OrganizationRequest"/> to <see cref="ExecuteMultipleRequest"/> batches
        /// </summary>
        /// <typeparam name="T">The typeof<see cref="OrganizationRequest"/></typeparam>
        /// <param name="requests">The collection of requests to partition into batches</param>
        /// <param name="batchSize">The size of each batch</param>
        /// <param name="batchSettings">The desired settings</param>
        /// <returns>A keyed collection of type <see cref="ExecuteMultipleRequest"/> representing the request batches</returns>
        public static IDictionary<string, ExecuteMultipleRequest> AsBatches<T>(this IEnumerable<T> requests, int batchSize, ExecuteMultipleSettings batchSettings)
            where T : OrganizationRequest
        {
            if (batchSize <= 0)
                throw new ArgumentException("Batch size must be greater than 0", "batchSize");
            if (batchSize > maxBatchSize)
                throw new ArgumentException(String.Format("Batch size of {0} exceeds max batch size of 1000", batchSize), "batchSize");
            if (batchSettings == null)
                throw new ArgumentNullException("batchSettings");

            // Index each request
            var indexedRequests = requests.Select((r, i) => new { Index = i, Value = r });

            // Partition the indexed requests by batch size 
            var partitions = indexedRequests.GroupBy(ir => ir.Index / batchSize);

            // Convert each partition to an ExecuteMultilpleRequest batch
            IEnumerable<ExecuteMultipleRequest> batches = partitions.Select(p => p.Select(ir => ir.Value).AsBatch(batchSettings));

            // Index each batch
            var indexedBatches = batches.Select((b, i) => new { Index = i, Value = b });

            // Return indexed batches as dictionary
            return indexedBatches.ToDictionary(ib => ib.Index.ToString(), ib => ib.Value);
        }

        /// <summary>
        /// Converts a collection of type <see cref="OrganizationRequest"/> to a single <see cref="ExecuteMultipleRequest"/> instance  
        /// </summary>
        /// <typeparam name="T">The typeof<see cref="OrganizationRequest"/></typeparam>
        /// <param name="requests">The collection of requests representing the batch</param>
        /// <param name="batchSettings">The desired settings</param>
        /// <returns>A single <see cref="ExecuteMultipleRequest"/> instance</returns>
        public static ExecuteMultipleRequest AsBatch<T>(this IEnumerable<T> requests, ExecuteMultipleSettings batchSettings)
            where T : OrganizationRequest
        {
            var batch = new OrganizationRequestCollection();
            batch.AddRange(requests);

            return new ExecuteMultipleRequest()
            {
                Requests = batch,
                Settings = batchSettings
            };
        }


    }
}
