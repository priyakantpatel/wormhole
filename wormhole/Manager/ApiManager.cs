using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wormhole.policy;
using wormhole.policy.aspnetcore;

namespace wormhole.Manager
{
    internal class ApiManager
    {
        public ProductProxyOptions CreateProductProxyOptions(HttpContext context)
        {
            return new ProductProxyOptions
            {
                Options = new ProxyOptions
                {
                    BackendBaseUri = "hello"
                }
            };
            //return null;
        }
    }

    internal class ProductProxyOptions
    {
        public ProxyOptions Options { get; set; }
        public List<IPolicy> InboundPolicies { get; set; }
        public List<IPolicy> OutboundPolicies { get; set; }
    }
}
