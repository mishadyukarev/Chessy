using System.Collections.Generic;

namespace Chessy.Game
{
    public struct PlayerLevelBuildingInfoE
    {
        public readonly IdxsCellsC IdxC;

        internal PlayerLevelBuildingInfoE(in bool def) : this()
        {
            IdxC = new IdxsCellsC(new HashSet<byte>());
        }
    }
}