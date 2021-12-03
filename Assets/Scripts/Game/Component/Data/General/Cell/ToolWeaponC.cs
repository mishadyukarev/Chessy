using System;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public struct ToolWeaponC : ITWCellE
    {
        TWTypes _toolWeapon;
        byte _idx;

        public TWTypes ToolWeapon => _toolWeapon;
        public bool Is(TWTypes tW) => ToolWeapon == tW;
        public bool HaveTW => ToolWeapon != default;


        public ToolWeaponC(byte idx) : this()
        {
            _idx = idx;
        }


        public void Set(byte idx)
        {
            _toolWeapon = UnitToolWeapon<ToolWeaponC>(idx).ToolWeapon;
            UnitToolWeapon<LevelC>(_idx).Level = UnitToolWeapon<LevelC>(idx).Level;
            UnitToolWeapon<ShieldProtectionC>(_idx).Set(UnitToolWeapon<ShieldProtectionC>(idx));
        }
        public void Reset()
        {
            _toolWeapon = TWTypes.None;
            UnitToolWeapon<LevelC>(_idx).Reset();
            UnitToolWeapon<ShieldProtectionC>(_idx).Reset();
        }
        public void Sync(in TWTypes tw, in LevelTypes lev, in int shieldProt)
        {
            _toolWeapon = tw;
            UnitToolWeapon<LevelC>(_idx).Level = lev;
            UnitToolWeapon<ShieldProtectionC>(_idx).Set(shieldProt);
        }

        public void SetNew(in TWTypes tw, in LevelTypes level)
        {
            _toolWeapon = tw;
            UnitToolWeapon<LevelC>(_idx).Level = level;

            if (tw == TWTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        UnitToolWeapon<ShieldProtectionC>(_idx).Set(1);
                        break;

                    case LevelTypes.Second:
                        UnitToolWeapon<ShieldProtectionC>(_idx).Set(3);
                        break;

                    default: throw new Exception();
                }
            }
        }
    }
}