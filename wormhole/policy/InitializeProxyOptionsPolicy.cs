using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wormhole.policy.aspnetcore;

namespace wormhole.policy
{
    /// <summary>
    /// Special internal policy to Initialize api management
    /// </summary>
    internal class InitializeProxyOptionsPolicy : IPolicy
    {
        public bool Run(HttpContext context, ProxyOptions options)
        {
            return true;
        }
    }
}
