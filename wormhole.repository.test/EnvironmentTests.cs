using FluentAssertions;
using System;
using wormhole.models;
using Xunit;

namespace wormhole.repository.test
{
    public class EnvironmentTests
    {
        [Fact(DisplayName = "Environment.WormholeConfigExists")]
        public void EnvironmentWormholeConfigExists()
        {
            var condifPath = Environment.GetEnvironmentVariable(WormholeConstants.ENVIRONMENT_WORMHOLE_CONFIG);
            condifPath.Should().NotBeNullOrWhiteSpace();
        }
    }
}
