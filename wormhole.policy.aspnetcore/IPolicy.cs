using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace wormhole.policy.aspnetcore
{
    public enum ServiceTypeEnum
    {
        Scoped = 1,
        Singleton = 2,
        Transient = 3,
    }

    public interface IPolicy
    {
        ServiceTypeEnum ServiceType { get; set; }
        bool Run(HttpContext context, ProxyOptions options);
    }
}
