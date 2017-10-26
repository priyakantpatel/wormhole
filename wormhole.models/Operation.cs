using System;
using System.Collections.Generic;
using System.Text;

namespace wormhole.models
{
    public class Operation
    {
        /// <summary>
        /// get, put, post, delete, ...
        /// </summary>
        public string HttpVerb { get; set; }
        /// <summary>
        /// Example get album by id => /album/{id}?query
        /// </summary>
        public string Path { get; set; }
        public string OperationId { get; set; }    // Can be null
        public string Description { get; set; }
    }


    /* ** Need to verify - start ** */
    public class RouteTable
    {
        public List<RouteRule> Rules { get; set; }
    }

    public class RouteRule
    {
        /// <summary>
        /// *, get, put, post, delete, ...
        /// </summary>
        public string HttpVerb { get; set; }
        /// <summary>
        /// Example get album by id => /album/*
        /// </summary>
        public string PathExpression { get; set; }
        public string BackendPath { get; set; }
        public List<string> InboundPolicies { get; set; }
        public List<string> OutboundPolicies { get; set; }
    }
    /* ** Need to verify - end ** */
}
