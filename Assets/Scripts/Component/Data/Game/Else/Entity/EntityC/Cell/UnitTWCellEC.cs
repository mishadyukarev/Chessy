using System;
using static Game.Game.CellUnitTWE;

namespace Game.Game
{
    public struct UnitTWCellEC : ITWCellE
    {
        readonly byte _idx;

        internal UnitTWCellEC(in byte idx)
        {
            _idx = idx;
        }

        public void Set(byte idx)
        {
            CellUnitTWE.UnitTW<ToolWeaponC>(_idx).ToolWeapon = CellUnitTWE.UnitTW<ToolWeaponC>(idx).ToolWeapon;
            CellUnitTWE.UnitTW<LevelTC>(_idx).Level = CellUnitTWE.UnitTW<LevelTC>(idx).Level;

            CellUnitTWE.UnitTW<ProtectionC>(_idx).Set(CellUnitTWE.UnitTW<ProtectionC>(idx));
        }
        public void Reset()
        {
            UnitTW<ToolWeaponC>(_idx).Reset();
            UnitTW<LevelTC>(_idx).Reset();

            UnitTW<ProtectionC>(_idx).Reset();
        }
        public void Sync(in TWTypes tw, in LevelTypes lev, in int shieldProt)
        {
            UnitTW<ToolWeaponC>(_idx).ToolWeapon = tw;
            UnitTW<LevelTC>(_idx).Level = lev;
            UnitTW<ProtectionC>(_idx).Protection = shieldProt;
        }

        public void SetNew(in TWTypes tw, in LevelTypes level)
        {
            UnitTW<ToolWeaponC>(_idx).ToolWeapon = tw;
            UnitTW<LevelTC>(_idx).Level = level;

            if (tw == TWTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        UnitTW<ProtectionC>(_idx).Protection = 1;
                        break;

                    case LevelTypes.Second:
                        UnitTW<ProtectionC>(_idx).Protection = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
    }
}