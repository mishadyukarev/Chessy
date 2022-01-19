﻿using ECS;
using System;

namespace Game.Game
{
    public struct CellUnitWaterEs
    {
        static Entity[] _units;

        public static ref C Water<C>(in byte idx) where C : struct, ICellUnitWaterE => ref _units[idx].Get<C>();


        public static bool NeedWater(in byte idx) => Water<AmountC>(idx).Amount <= 100 * 0.4f;
        public static int MaxWater(in byte idx)
        {
            var unitT = CellUnitEs.Unit<UnitTC>(idx).Unit;
            var levelT = CellUnitEs.Unit<LevelTC>(idx).Level;
            var playerT = CellUnitEs.Unit<PlayerTC>(idx).Player;

            var maxWater = 100;

            if (UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Water, unitT, levelT, playerT, UpgradeTypes.PickCenter).Have)
            {
                return maxWater = (int)(maxWater * 0.5f);
            }

            return maxWater;
        }
        public static bool HaveMaxWater(in byte idx) => Water<AmountC>(idx).Amount >= MaxWater(idx);


        public CellUnitWaterEs(in EcsWorld gameW)
        {
            _units = new Entity[CellStartValues.ALL_CELLS_AMOUNT];
            for (var idx = 0; idx < _units.Length; idx++)
            {
                _units[idx] = gameW.NewEntity()
                    .Add(new AmountC());
            }
        }

        public static void SetMaxWater(in byte idx) => Water<AmountC>(idx).Amount = MaxWater(idx);
        public static void TakeWater(in byte idx) => Water<AmountC>(idx).Take((int)(100 * 0.15f));
    }

    public interface ICellUnitWaterE { }
}