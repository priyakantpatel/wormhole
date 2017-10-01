using System;
using System.Collections.Generic;

namespace wormhole.models
{
    public class Api
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        /// <summary>
        /// "SwaggerJson", Custom
        /// </summary>
        public string GroupType { get; set; }

        /// <summary>
        /// if value of UrlSuffix is "album"
        /// Base URL => http(s)://localhost/
        /// Base Api Url will be => http(s)://localhost/album
        /// </summary>
        public string UrlSuffix { get; set; }

        /// <summary>
        /// Example: Swagger endpoint or other discovery mechanism 
        /// </summary>
        public string DiscoveryUrl { get; set; }

        public List<Operation> Operations { get; set; }
    }
}
