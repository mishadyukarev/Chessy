using System.Collections.Generic;

namespace Game.Game
{
    public struct PlayerLevelBuildingInfoE
    {
        public readonly IdxsC IdxC;
        public bool HaveCenterUpgrade;

        internal PlayerLevelBuildingInfoE(in bool def) : this()
        {
            IdxC = new IdxsC(new HashSet<byte>());
        }
    }
}