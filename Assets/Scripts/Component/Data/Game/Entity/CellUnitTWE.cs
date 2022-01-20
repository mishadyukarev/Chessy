using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitTWE
    {
        static Entity[] _unitTWs;

        public static ref T UnitTW<T>(in byte idx) where T : struct, ITWCellE => ref _unitTWs[idx].Get<T>();

        public CellUnitTWE(in EcsWorld gameW)
        {
            _unitTWs = new Entity[CellStartValues.ALL_CELLS_AMOUNT];

            for (byte idx = 0; idx < CellStartValues.ALL_CELLS_AMOUNT; idx++)
            {
                _unitTWs[idx] = gameW.NewEntity()
                    .Add(new ToolWeaponC())
                    .Add(new LevelTC())
                    .Add(new ProtectionC());
            }
        }

        public static void Take(in byte _idx, in int taking = 1)
        {
            ref var tw = ref UnitTW<ToolWeaponC>(_idx);
            ref var prot = ref UnitTW<ProtectionC>(_idx);

            if (!tw.IsShield) throw new Exception();
            if (!prot.Have) throw new Exception();

            prot.Take(taking);

            if (!prot.Have) tw.Reset();
        }

        public static void Set(byte idx_from, in byte idx_to)
        {
            UnitTW<ToolWeaponC>(idx_to).ToolWeapon = UnitTW<ToolWeaponC>(idx_from).ToolWeapon;
            UnitTW<LevelTC>(idx_to).Level = UnitTW<LevelTC>(idx_from).Level;

            UnitTW<ProtectionC>(idx_to) = UnitTW<ProtectionC>(idx_from);
        }
        public static void Reset(in byte idx)
        {
            UnitTW<ToolWeaponC>(idx).Reset();
            UnitTW<LevelTC>(idx).Reset();

            UnitTW<ProtectionC>(idx).Reset();
        }

        public static void SetNew(in byte idx, in ToolWeaponTypes tw, in LevelTypes level)
        {
            UnitTW<ToolWeaponC>(idx).ToolWeapon = tw;
            UnitTW<LevelTC>(idx).Level = level;

            if (tw == ToolWeaponTypes.Shield)
            {
                switch (level)
                {
                    case LevelTypes.First:
                        UnitTW<ProtectionC>(idx).Protection = 1;
                        break;

                    case LevelTypes.Second:
                        UnitTW<ProtectionC>(idx).Protection = 3;
                        break;

                    default: throw new Exception();
                }
            }
        }
    }
}