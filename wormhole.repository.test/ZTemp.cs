using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using wormhole.models;
using wormhole.repository.Disk;
using Xunit;

namespace wormhole.repository.test
{
    public class ZTemp
    {
        [Fact(DisplayName = "ZTemp.create.repo")]
        public void CreatRepoInstantTest()
        {
            IWormholeRepository db = new WormholeDiskRepository();

            db.Config.Should().NotBeNull();
        }
    }
}
