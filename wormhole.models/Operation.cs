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
        string HttpVerb { get; set; }
        /// <summary>
        /// Example get album by id => /album/{id}?query
        /// </summary>
        string Path { get; set; }
        string OperationId { get; set; }    // Can be null
        string Description { get; set; }
    }
}
