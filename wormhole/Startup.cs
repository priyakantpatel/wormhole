using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using wormhole.policy;
using wormhole.Manager;

namespace wormhole
{
    public class Startup
    {
        ApiManager _apiManager;  //Special internal policy

        public void ConfigureServices(IServiceCollection services)
        {
            _apiManager = new ApiManager(new repository.Disk.WormholeDiskRepository());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //http://product-api-app.azurewebsites.net/swagger/

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(RequestDelegate);
        }

        private async Task RequestDelegate(HttpContext context)
        {
            try
            {
                var options = _apiManager.CreateProductProxyOptions(context);

                if (options == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync("NotFound");
                }
                else
                {
                    if (!RunInboundPolicy(context, options))
                    {
                        //Policy must set "HttpStatusCode" and-or content
                        return;
                    }

                    //await TempBackendCall(context, options);
                    await proxy.WormholeProxy.HandleHttpRequestX(context, options.BackendBaseUri);

                    RunOutboundPolicy(context, options);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ex.ToString());
            }
        }

        private bool RunInboundPolicy(HttpContext context, ProductProxyOptions productOptions)
        {
            if (productOptions.InboundPolicies != null)
            {
                foreach (var item in productOptions.InboundPolicies)
                {
                        return false;
                }
            }
            return true;
        }

        private bool RunOutboundPolicy(HttpContext context, ProductProxyOptions productOptions)
        {
            if (productOptions.OutboundPolicies != null)
            {
                foreach (var item in productOptions.OutboundPolicies)
                {
                    if (item.Run(context, productOptions))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Task TempBackendCall(HttpContext context, ProxyOptions options)
        {
            return context.Response.WriteAsync("Hello world");
        }
    }
}
