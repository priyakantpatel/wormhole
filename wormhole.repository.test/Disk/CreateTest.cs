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
            Directory.Exists(db.Config.DataDirectory).Should().BeTrue();
        }
    }
}
