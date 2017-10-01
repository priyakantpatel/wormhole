using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using wormhole.models;

namespace WormholeServices.Swagger2
{
    public class Swagger2Helper
    {
        static public List<Operation> GetOperationsFromSwaggerJson(string jsonString)
        {
            var returnValue = new List<Operation>();
            var sDoc = Newtonsoft.Json.JsonConvert.DeserializeObject<wormhole.services.Swagger2.SwaggerDocument>(jsonString);

            foreach (var pathItemKv in sDoc.Paths)
            {
                var pathItem = pathItemKv.Value;
                var pathItemKey = pathItemKv.Key;

                var operation = new Operation();

                if (pathItem.Get != null)
                {
                    operation.HttpVerb = "GET";
                    operation.OperationId = pathItem.Get.OperationId;
                    operation.Path = pathItemKey;
                    operation.Description = "Kool";
                }

                if (pathItem.Post != null)
                {
                    operation.HttpVerb = "POST";
                    operation.OperationId = pathItem.Post.OperationId;
                    operation.Path = pathItemKey;
                    operation.Description = "Kool";
                }

                returnValue.Add(operation);
            }

            return returnValue;
        }
    }
}
