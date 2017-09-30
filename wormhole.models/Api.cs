using System;
using System.Collections.Generic;

namespace wormhole.models
{
    public class Api
    {
        string Id { get; set; }

        /// <summary>
        /// "swagger", Custom
        /// </summary>
        string GroupType { get; set; }

        /// <summary>
        /// if value of UrlSuffix is "album"
        /// Base URL => http(s)://localhost/
        /// Base Api Url will be => http(s)://localhost/album
        /// </summary>
        string UrlSuffix { get; set; }

        /// <summary>
        /// Example: Swagger endpoint or other discovery mechanism 
        /// </summary>
        string DiscoveryUrl { get; set; }

        List<Operation> Operations { get; set; }
    }
}
