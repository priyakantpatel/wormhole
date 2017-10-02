using System;
using System.Collections.Generic;
using System.Text;

namespace wormhole.models
{
    public class WormholeConfig
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string DataDirectory { get; set; }
        public List<Api> ApiCollection { get; set; }
        //Other wormhole wide configuration
    }
}
