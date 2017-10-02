using System;
using FluentAssertions;
using System.Linq;
using wormhole.repository.Disk;
using Xunit;

namespace wormhole.services.test
{
    public class WormholeServicesTests
    {
        WormholeServices GetWormholeServices()
        {
            var whDiskRepo = new WormholeDiskRepository();
            var whSrv = new WormholeServices(whDiskRepo);
            return whSrv;
        }

        [Fact(DisplayName = "WormholeServices.GetConfig")]
        public void GetConfigTest()
        {
            var whSrv = GetWormholeServices();

            var config = whSrv.GetConfig();

            config.Should().NotBeNull();

        }

        [Fact(DisplayName = "WormholeServices.GetApiCollection")]
        public void GetApiCollectionTest()
        {
            var whSrv = GetWormholeServices();

            var apis = whSrv.GetApiCollection();

            apis.Should().NotBeNull();
            apis.Count().Should().BeGreaterThan(0);

        }

        [Fact(DisplayName = "WormholeServices.GetApiOperations")]
        public void GetApiOperationsTest()
        {
            var whSrv = GetWormholeServices();

            var apis = whSrv.GetApiCollection();

            apis.Should().NotBeNull();
            apis.Count().Should().BeGreaterThan(0);

            var api = apis.First();
            var operations = whSrv.GetApiOperations(api); ;

            operations.Should().NotBeNull();
            operations.Count().Should().BeGreaterThan(0);
        }
    }
}
