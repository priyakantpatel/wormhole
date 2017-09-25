using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace wormhole.policy.aspnetcore
{
    public interface IPolicy
    {
        bool Run(HttpContext context, ProxyOptions options);
    }
}
