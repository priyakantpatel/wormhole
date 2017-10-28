using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using wormhole.models;

namespace wormhole.repository.Disk
{
    public class WormholeDiskRepository : IWormholeRepository
    {
        public readonly string _configFilePath;
        public WormholeConfig _config { get; set; }

        public WormholeDiskRepository()
        {
            _configFilePath = Path.GetFullPath(Environment.GetEnvironmentVariable(WormholeConstants.ENVIRONMENT_WORMHOLE_CONFIG));
            Console.WriteLine($"ConfigFilePath [{_configFilePath}]");

            if (!File.Exists(_configFilePath))
            {
                throw new Exception($"Config file not found. Please set {WormholeConstants.ENVIRONMENT_WORMHOLE_CONFIG} Environment with valid config file.");
            }

            LoadConfig();
            ValidateConfig();
        }

        #region Load Config

        private void LoadConfig()
        {
            string jsonString = ReadJsonStringFromFile(_configFilePath);
            _config = Newtonsoft.Json.JsonConvert.DeserializeObject<WormholeConfig>(jsonString);
        }

        private void ValidateConfig()
        {
            if (_config == null)
            {
                throw new Exception("Invalid config file");
            }

            //Validate DataDirectory
            if (string.IsNullOrWhiteSpace(_config.DataDirectory))
            {
                throw new Exception($"Invalid DataDirectory {_config.DataDirectory ?? "[empty]"}");
            }

            if (!Path.IsPathRooted(_config.DataDirectory))
            {
                _config.DataDirectory = Path.Combine(Path.GetDirectoryName(_configFilePath), _config.DataDirectory);
            }
            _config.DataDirectory = Path.GetFullPath(_config.DataDirectory);

            Console.WriteLine($"DataDirectory [{_config.DataDirectory}]");

            Directory.CreateDirectory(_config.DataDirectory);
        }

        public WormholeConfig UpsertConfig(WormholeConfig config)
        {
            throw new Exception("Not implemented");
        }

        private void SaveApiConfig(string content)
        {
            SaveJsonStringToFile(_configFilePath, content);
        }

        #endregion

        string ReadJsonStringFromFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            return "{}";
        }

        void SaveJsonStringToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        #region Load Api

        public const string API_FOLDER = "apis";
        public List<Api> GetApiCollection()
        {
            var apiCollection = new List<Api>();
            var apifolderPath = Path.Combine(_config.DataDirectory, API_FOLDER);

            Directory.CreateDirectory(apifolderPath);   // Create folder if not exists
            var apiFiles = Directory.GetFiles(apifolderPath);

            apiCollection = new List<Api>();

            foreach (var apifile in apiFiles)
            {
                var api = GetApiForFile(apifile);
                apiCollection.Add(api);
            }

            return apiCollection;
        }

        Api GetApiForFile(string apifile)
        {
            Console.WriteLine($"Load Api [{apifile}]");

            string jsonString = ReadJsonStringFromFile(apifile);
            var api = Newtonsoft.Json.JsonConvert.DeserializeObject<Api>(jsonString);

            return api;
        }

        //public const string API_DISCOVERY_FILDER = "discovery";
        //async void DiscoverApi()
        //{
        //    var discoveryPath = Path.Combine(this.Config.DataDirectory, API_DISCOVERY_FILDER);
        //    Directory.CreateDirectory(discoveryPath);   // Create folder if not exists

        //    foreach (var api in this.ApiCollection)
        //    {
        //        if(api.GroupType == "SwaggerJson")
        //        {
        //            var discoveryFilePath = Path.Combine(discoveryPath, $"{api.Id}.json");

        //            string jsonString;
        //            if (!File.Exists(discoveryFilePath))
        //            {
        //                Console.WriteLine($"Discover api [{api.Id}]");

        //                if (string.IsNullOrWhiteSpace(api.DiscoveryUrl))
        //                {
        //                    Console.WriteLine("Skip api discovery. Application is not fully configured yet!");
        //                    continue;
        //                }

        //                var httpClient = new HttpClient(new HttpClientHandler());
        //                jsonString = await httpClient.GetStringAsync(api.DiscoveryUrl);
        //                SaveJsonStringToFile(discoveryFilePath, jsonString);
        //            }
        //            else
        //            {
        //                jsonString = ReadJsonStringFromFile(discoveryFilePath);
        //            }

        //            Console.WriteLine(jsonString.Length);
        //            //load operations from "jsonString" : Priyakant Is Here
        //        }
        //        else
        //        {
        //            throw new Exception($"Invalid GroupType [{api.GroupType ?? ""}]");
        //        }
        //    }
        //}

        #endregion

        #region IWormholeRepository

        public WormholeConfig Config => _config;
        public Api GetApi(string Id)
        {
            return null;
        }

        #endregion

        #region Routes
        public List<RouteDefinition> GetRouteDefinitions()
        {
            var returnValue = new List<RouteDefinition>();

            returnValue.Add(new RouteDefinition
            {
                BackendPath= "http://product-api-app.azurewebsites.net/Products",
                HttpVerb="*",
                InboundPolicies = new List<string>(),
                OutboundPolicies = new List<string>(),
                PathStartsWith = "/product-api/",
                ProductId = "x-p1",
            });

            returnValue.Add(new RouteDefinition
            {
                BackendPath = "http://product-api-app.azurewebsites.net/api/Values",
                HttpVerb = "*",
                InboundPolicies = new List<string>(),
                OutboundPolicies = new List<string>(),
                PathStartsWith = "/value-api/",
                ProductId = "x-p2",
            });
            return returnValue;
        }

        #endregion
    }
}
