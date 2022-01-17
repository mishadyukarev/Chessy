using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitWaterEs
    {
        static Entity[] _units;

        public static ref C Water<C>(in byte idx) where C : struct, ICellUnitWaterE => ref _units[idx].Get<C>();


        //float UpgadeWaterPercent => UnitUpgC.UpgWaterPercent(Unit, Level, Owner);

        public static bool NeedWater(in byte idx) => Water<AmountC>(idx).Amount <= 100 * 0.4f;
        public static int MaxWater => (int)(100 + 100 /** UpgadeWaterPercent*/);
        public static bool HaveMaxWater(in byte idx) => Water<AmountC>(idx).Amount >= MaxWater;


        public CellUnitWaterEs(in EcsWorld gameW)
        {
            _units = new Entity[CellValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _units.Length; idx++)
            {
                _units[idx] = gameW.NewEntity()
                    .Add(new AmountC(idx));
            }
        }

        public static void SetMaxWater(in byte idx) => Water<AmountC>(idx).Amount = MaxWater;
        public static void ExecuteThirsty(in byte idx)
        {
            float percent = 0;
            switch (CellUnitEs.Unit<UnitTC>(idx).Unit)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: percent = 0.4f; break;
                case UnitTypes.Pawn: percent = 0.5f; break;
                case UnitTypes.Archer: percent = 0.5f; break;
                case UnitTypes.Scout: percent = 0.5f; break;
                case UnitTypes.Elfemale: percent = 0.5f; break;
                default: throw new Exception();
            }

            CellUnitHpEs.Hp<AmountC>(idx).Take((int)(100 * percent));
        }
        public static void TakeWater(in byte idx) => Water<AmountC>(idx).Take((int)(100 * 0.15f));
    }

    public interface ICellUnitWaterE { }
}