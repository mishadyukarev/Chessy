using System;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public struct UnitTWCellC : ITWCellE
    {
        byte _idx;

        internal UnitTWCellC(byte idx) : this()
        {
            _idx = idx;
        }

        public void Set(byte idx)
        {
            UnitTW<ToolWeaponC>(_idx).ToolWeapon = UnitTW<ToolWeaponC>(idx).ToolWeapon;
            UnitTW<LevelC>(_idx).Level = UnitTW<LevelC>(idx).Level;

            UnitTW<ProtectionC>(_idx).Set(UnitTW<ProtectionC>(idx));
        }
        public void Reset()
        {
            UnitTW<ToolWeaponC>(_idx).Reset();
            UnitTW<LevelC>(_idx).Reset();

            UnitTW<ProtectionC>(_idx).Reset();
        }
        public void Sync(in TWTypes tw, in LevelTypes lev, in int shieldProt)
        {
            UnitTW<ToolWeaponC>(_idx).ToolWeapon = tw;
            UnitTW<LevelC>(_idx).Level = lev;
            UnitTW<ProtectionC>(_idx).Set(shieldProt);
        }

        public void SetNew(in TWTypes tw, in LevelTypes level)
        {
            UnitTW<ToolWeaponC>(_idx).ToolWeapon = tw;
            UnitTW<LevelC>(_idx).Level = level;

            if (tw == TWTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        UnitTW<ProtectionC>(_idx).Set(1);
                        break;

                    case LevelTypes.Second:
                        UnitTW<ProtectionC>(_idx).Set(3);
                        break;

                    default: throw new Exception();
                }
            }
        }
    }
}