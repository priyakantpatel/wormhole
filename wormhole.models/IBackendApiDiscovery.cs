using System;
using System.Collections.Generic;
using System.Text;

namespace wormhole.models
{
    public interface IBackendApiDiscovery
    {
        IBackendApiInfo GetServiceInfo();
    }
}
