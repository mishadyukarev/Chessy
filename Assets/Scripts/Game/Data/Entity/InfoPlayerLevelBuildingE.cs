using System.Collections.Generic;

namespace Game.Game
{
    public struct InfoPlayerLevelBuildingE
    {
        public readonly IdxsC IdxC;
        bool[] _haveUpgrades;

        public bool HaveCenterUpgrade;

        public ref bool HaveUpgrade(in UpgradeTypes upgT) => ref _haveUpgrades[(byte)upgT - 1];

        internal InfoPlayerLevelBuildingE(in bool def)
        {
            _haveUpgrades = new bool[(byte)UpgradeTypes.End - 1];
            HaveCenterUpgrade = true;

            IdxC = new IdxsC(new HashSet<byte>());
        }
    }
}