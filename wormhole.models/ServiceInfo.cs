using System;
using System.Collections.Generic;
using System.Text;

namespace wormhole.models
{
    public class IBackendApiInfo
    {
        string Version { get; set; }    // Got idea From Swagger
        string Title { get; set; }    // Got idea From Swagger
        List<Operation> Operations { get; set; }
    }
}
