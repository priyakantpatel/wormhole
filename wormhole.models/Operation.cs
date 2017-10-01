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
}
