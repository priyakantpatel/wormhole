using System;
using System.Collections.Generic;
using System.Text;

namespace wormhole.models
{
    public class WormholeConfig
    {
        public virtual string Name { get; set; }
        public virtual string Version { get; set; }
        public virtual string Data { get; set; }
        public virtual List<Api> ApiCollection { get; set; }
        //Other wormhole wide configuration
    }
}
