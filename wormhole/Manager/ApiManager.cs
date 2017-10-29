using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using wormhole.policy;
using wormhole.policy.aspnetcore;
using wormhole.repository;
using wormhole.repository.Disk;

namespace wormhole.Manager
{
    internal class ApiManager
    {
        IWormholeRepository _db = null;
        RouteTable _routeTable = null;
        public ApiManager(IWormholeRepository db)
        {
            _db = db;

            InitializeRouteTable();
        }

        private void InitializeRouteTable()
        {
            _routeTable = new RouteTable();

            var routes = _db.GetRouteDefinitions();

            foreach (var item in routes)
            {
                var rd = new Route
                {
                    BackendPath = item.BackendPath,
                    HttpVerb = item.HttpVerb,
                    InboundPolicies = new List<IPolicy>(),
                    OutboundPolicies = new List<IPolicy>(),
                    PathExpression = item.PathStartsWith,
                };

                if(item.InboundPolicies != null)
                {
                    item.InboundPolicies.ForEach(x => {
                        if(x["Name"] == "OpenIdConnectJwtPolicyConfig")
                        {
                            rd.InboundPolicies.Add(
                                new OpenIdConnectJwtPolicy(x)
                                );
                        }
                    });
                }

                _routeTable.Routes.Add(rd);
            }
        }

        public ProductProxyOptions CreateProductProxyOptions(HttpContext context)
        {
            var path = context.Request.Path.ToString();
            var endWithSlash = true;

            if (!path.EndsWith('/'))
            {
                path = path + '/';
                endWithSlash = false;
            }

            //Note: We will improve performance in future
            foreach (var item in _routeTable.Routes)
            {
                if (path.StartsWith(item.PathExpression, StringComparison.OrdinalIgnoreCase))
                {
                    var newPath = path.Replace(item.PathExpression, "", StringComparison.OrdinalIgnoreCase);

                    var rv = new ProductProxyOptions
                    {
                        BackendBaseUri = $"{item.BackendPath}/{newPath}",   //Note: We will improve performance in future
                        InboundPolicies = item.InboundPolicies,
                        OutboundPolicies = item.OutboundPolicies,
                    };
                    
                    if (!endWithSlash)  //Note: We will improve performance in future
                    {
                        rv.BackendBaseUri = rv.BackendBaseUri.TrimEnd('/');
                    }
                    return rv;
                }
            }

            return null;
        }
    }

    internal class ProductProxyOptions : ProxyOptions
    {
        public List<IPolicy> InboundPolicies { get; set; }
        public List<IPolicy> OutboundPolicies { get; set; }
    }

    public class RouteTable
    {
        public List<Route> Routes = null;
        public RouteTable()
        {
            Routes = new List<Route>();
        }
    }

    public class Route
    {
        /// <summary>
        /// *, get, put, post, delete, ...
        /// "*" => will match any verbe
        /// </summary>
        public string HttpVerb { get; set; }
        /// <summary>
        /// Example get album by id Regex => "/album/.*"
        /// </summary>
        public string PathExpression { get; set; }
        public List<IPolicy> InboundPolicies { get; set; }
        public List<IPolicy> OutboundPolicies { get; set; }

        /// <summary>
        /// BackendPath map path
        /// </summary>
        public string BackendPath { get; set; }
    }
}
