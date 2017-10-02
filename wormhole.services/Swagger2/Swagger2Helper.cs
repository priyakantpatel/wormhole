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

                //GET
                if (pathItem.Get != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Get, "GET");
                    returnValue.Add(operation);
                }
                //POST
                if (pathItem.Post != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Post, "POST");
                    returnValue.Add(operation);
                }
                //PUT
                if (pathItem.Put != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Put, "PUT");
                    returnValue.Add(operation);
                }
                //DELETE
                if (pathItem.Delete != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Delete, "DELETE");
                    returnValue.Add(operation);
                }
                //OPTIONS
                if (pathItem.Options != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Options, "OPTIONS");
                    returnValue.Add(operation);
                }
                //HEAD
                if (pathItem.Head != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Head, "HEAD");
                    returnValue.Add(operation);
                }
                //PATCH
                if (pathItem.Patch != null)
                {
                    var operation = GetOperation(GetPathFromBase(sDoc.BasePath, pathItemKey), pathItem.Patch, "PATCH");
                    returnValue.Add(operation);
                }

            }

            return returnValue;
        }

        static Operation GetOperation(string path, wormhole.services.Swagger2.Operation opration, string httpVerb)
        {
            return new Operation
            {
                HttpVerb = httpVerb,
                OperationId = opration.OperationId,
                Path = path,
                Description = "",
            };
        }

        static string GetPathFromBase(string basePath, string path)
        {
            return (basePath == "/") ? path : $"{basePath}{path}";
        }
    }
}
