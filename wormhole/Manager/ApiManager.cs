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
                _routeTable.Routes.Add(
                    new Route
                    {
                        BackendPath = item.BackendPath,
                        HttpVerb = item.HttpVerb,
                        InboundPolicies = new List<IPolicy>(),
                        OutboundPolicies = new List<IPolicy>(),
                        PathExpression = item.PathStartsWith,
                    });
            }
        }

        public ProductProxyOptions CreateProductProxyOptions(HttpContext context)
        {
            var path = context.Request.Path.ToString();

            if (!path.EndsWith('/'))
            {
                path = path + '/';
            }

            //Note: We will improve performance in future
            foreach (var item in _routeTable.Routes)
            {
                if (path.StartsWith(item.PathExpression, StringComparison.OrdinalIgnoreCase))
                {
                    var newPath = path.Replace(item.PathExpression, "", StringComparison.OrdinalIgnoreCase);

                    return new ProductProxyOptions
                    {
                        BackendBaseUri = $"{item.BackendPath}/{newPath}",   //Note: We will improve performance in future
                        InboundPolicies = item.InboundPolicies,
                        OutboundPolicies = item.OutboundPolicies,
                    };
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
