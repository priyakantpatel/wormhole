using System;
using wormhole.repository;
using wormhole.models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WormholeServices.Swagger2;

namespace wormhole.services
{
    public class WormholeServices
    {
        IWormholeRepository _repo;
        public WormholeServices(IWormholeRepository repo)
        {
            _repo = repo;
        }

        public WormholeConfig GetConfig()
        {
            return _repo.Config;
        }

        public List<Api> GetApiCollection()
        {
            return _repo.GetApiCollection();
        }

        public Api GetApi(string Id)
        {
            return _repo.GetApi(Id);
        }

        public Api UpsertApi(Api api)
        {
            throw new Exception("Not implemented");
        }

        public List<Operation> GetApiOperations(Api api)
        {
            var operations = DiscoverOperations(api).Result;
            return operations;
            //throw new Exception("Not implemented");
        }


        public async Task RefreshApiOperations(Api api)
        {
            ResetOperation(api);
            List<Operation> operation = await DiscoverOperations(api);
            SetApiOperation(api, operation);
        }

        public async Task<List<Operation>> DiscoverOperations(Api api)
        {
            //Important-Note: We will discover every time for now. We will be caching in future!
            //Note: we will auto discover GroupType!.. May be?? need more thinking!
            if (api.GroupType == "SwaggerJson") //Swagger 2.0
            {
                var httpClient = new HttpClient(new HttpClientHandler());
                var jsonString = await httpClient.GetStringAsync(api.DiscoveryUrl);

                //Get operations for
                return Swagger2Helper.GetOperationsFromSwaggerJson(jsonString);

            }

            throw new Exception($"Invalid GroupType [{api.GroupType ?? ""}]");
        }

        /// <summary>
        /// This action will reset all operation to null
        /// </summary>
        /// <param name="api"></param>
        public void ResetOperation(Api api)
        {
            throw new Exception("Not implemented");
        }

        public void SetApiOperation(Api api, List<Operation> operation)
        {
            throw new Exception("Not implemented");
        }
    }
}
