using System;
using System.Collections.Generic;
using System.Text;
using wormhole.models;

namespace wormhole.repository
{
    public interface IBackendApiDiscovery
    {
        IBackendApiInfo GetServiceInfo();
    }
}
