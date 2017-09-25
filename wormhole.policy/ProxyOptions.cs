using System;

namespace wormhole.policy
{
    /// <summary>
    /// Proxy Options
    /// </summary>
    public class ProxyOptions
    {
        ///// <summary>
        ///// Destination uri scheme
        ///// </summary>
        //public string Scheme { get; set; }

        ///// <summary>
        ///// Destination uri host
        ///// </summary>
        //public string Host { get; set; }

        ///// <summary>
        ///// Destination uri path base to which current Path will be appended
        ///// </summary>
        //public string PathBase { get; set; }

        ///// <summary>
        ///// Query string parameters to append to each request
        ///// </summary>
        //public string AppendQuery { get; set; }

        public string BackendBaseUri { get; set; }
    }
}
