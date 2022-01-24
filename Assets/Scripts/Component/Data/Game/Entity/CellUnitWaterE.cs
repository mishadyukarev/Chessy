using ECS;
using System;
using UnityEngine;

namespace Game.Game
{
    public sealed class CellUnitWaterE : CellAbstE
    {
        public ref AmountC Water => ref Ent.Get<AmountC>();


        public int MaxWater
        {
            get
            {
                var unitT = CellUnitEs.Unit(Idx).Unit;
                var levelT = EntitiesPool.UnitElse.Level(Idx).Level;
                var playerT = EntitiesPool.UnitElse.Owner(Idx).Player;

                var maxWater = CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS;

                if (!CellUnitEs.Unit(Idx).IsAnimal)
                {
                    if (UnitStatUpgradesEs.HaveUpgrade<HaveUpgradeC>(UnitStatTypes.Water, unitT, levelT, playerT, UpgradeTypes.PickCenter).Have)
                    {
                        return maxWater += (int)(maxWater * 0.5f);
                    }
                }

                return maxWater;

            }
        }
        public bool HaveMaxWater => Water.Amount >= MaxWater;


        public CellUnitWaterE(in EcsWorld gameW, in byte idx) : base(gameW, idx) { }

        public void SetMaxWater() => Water.Amount = MaxWater;
    }
}