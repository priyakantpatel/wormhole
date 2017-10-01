using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using wormhole.models;

namespace wormhole.repository.Disk
{
    public class WormholeDiskRepository : IWormholeRepository
    {
        public readonly string _configFilePath;
        public WormholeConfig _config { get; set; }

        #region Private members

        public WormholeDiskRepository()
        {
            _configFilePath = Environment.GetEnvironmentVariable(WormholeConstants.ENVIRONMENT_WORMHOLE_CONFIG);

            if (!File.Exists(_configFilePath))
            {
                throw new Exception($"Config file not found. Please set {WormholeConstants.ENVIRONMENT_WORMHOLE_CONFIG} Environment with valid config file.");
            }

            LoadConfig();
            ValidateConfig();
        }

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

            if (string.IsNullOrWhiteSpace(_config.DataDirectory))
            {
                throw new Exception($"Invalid DataDirectory {_config.DataDirectory ?? "[empty]"}");
            }

            //Validate DataDirectory
            _config.DataDirectory = Path.GetFullPath(_config.DataDirectory);
            Directory.CreateDirectory(_config.DataDirectory);
        }

        private void SaveApiConfig(string content)
        {
            SaveJsonStringToFile(_configFilePath, content);
        }

        string ReadJsonStringFromFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(_configFilePath);
            }
            return "{}";
        }

        void SaveJsonStringToFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        #endregion

        #region IWormholeRepository

        public WormholeConfig Config => _config;
        public List<Api> ApiCollection { get; set; }
        public Api GetApi(string Id)
        {
            return null;
        }

        #endregion
    }
}
