using System.Collections.Generic;
using wormhole.models;

namespace wormhole.repository
{
    public interface IWormholeRepository
    {
        #region WormholeDatabase

        WormholeConfig Config { get; }

        #endregion

        #region Api Collections

        List<Api> GetApiCollection();
        Api GetApi(string Id);

        #endregion
    }
}
