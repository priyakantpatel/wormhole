using FluentAssertions;
using System.IO;
using wormhole.repository.Disk;
using Xunit;

namespace wormhole.repository.test.Disk
{
    public class CreateTest
    {
        [Fact(DisplayName = "WormholeDiskRepository.Create")]
        public void CreatRepoInstantTest()
        {
            IWormholeRepository db = new WormholeDiskRepository();

            db.Config.Should().NotBeNull();
            db.Config.Version.Should().NotBeNullOrWhiteSpace();
            db.Config.DataDirectory.Should().NotBeNullOrWhiteSpace();
            db.Config.ApiCollection.Should().BeNull();
            Directory.Exists(db.Config.DataDirectory).Should().BeTrue();
            Directory.Exists(Path.Combine(db.Config.DataDirectory, WormholeDiskRepository.API_DISCOVERY_FILDER)).Should().BeTrue();
        }
    }
}
