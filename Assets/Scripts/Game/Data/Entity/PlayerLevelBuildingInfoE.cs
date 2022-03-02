using System.Collections.Generic;

namespace Chessy.Game
{
    public struct PlayerLevelBuildingInfoE
    {
        public readonly IdxsC IdxC;

        internal PlayerLevelBuildingInfoE(in bool def) : this()
        {
            IdxC = new IdxsC(new HashSet<byte>());
        }
    }
}