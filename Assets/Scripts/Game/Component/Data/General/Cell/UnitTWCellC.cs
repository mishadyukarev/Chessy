using System;
using static Game.Game.EntityPool;

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
            UnitToolWeapon<ToolWeaponC>(_idx).ToolWeapon = UnitToolWeapon<ToolWeaponC>(idx).ToolWeapon;
            UnitToolWeapon<LevelC>(_idx).Level = UnitToolWeapon<LevelC>(idx).Level;

            UnitShield<ProtectionC>(_idx).Set(UnitShield<ProtectionC>(idx));
        }
        public void Reset()
        {
            UnitToolWeapon<ToolWeaponC>(_idx).Reset();
            UnitToolWeapon<LevelC>(_idx).Reset();

            UnitShield<ProtectionC>(_idx).Reset();
        }
        public void Sync(in TWTypes tw, in LevelTypes lev, in int shieldProt)
        {
            UnitToolWeapon<ToolWeaponC>(_idx).ToolWeapon = tw;
            UnitToolWeapon<LevelC>(_idx).Level = lev;
            UnitShield<ProtectionC>(_idx).Set(shieldProt);
        }

        public void SetNew(in TWTypes tw, in LevelTypes level)
        {
            UnitToolWeapon<ToolWeaponC>(_idx).ToolWeapon = tw;
            UnitToolWeapon<LevelC>(_idx).Level = level;

            if (tw == TWTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        UnitShield<ProtectionC>(_idx).Set(1);
                        break;

                    case LevelTypes.Second:
                        UnitShield<ProtectionC>(_idx).Set(3);
                        break;

                    default: throw new Exception();
                }
            }
        }
    }
}