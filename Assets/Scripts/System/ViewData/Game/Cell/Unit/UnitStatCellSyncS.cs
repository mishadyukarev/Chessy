using UnityEngine;
using static Game.Game.CellBarsVEs;
using static Game.Game.CellBlocksVEs;

namespace Game.Game
{
    sealed class UnitStatCellSyncS : SystemViewAbstract, IEcsRunSystem
    {
        public UnitStatCellSyncS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var ownUnit_0 = UnitEs(idx_0).MainE.OwnerC;
                var hpUnit_0 = UnitStatEs(idx_0).Hp.Health;

                Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Disable();


                Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).Disable();


                if (UnitEs(idx_0).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                {
                    if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Color = Color.red;

                        float xCordinate = (float)hpUnit_0.Amount / CellUnitStatHpE.MAX_HP;
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).LocalScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                        Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).SetActive(UnitStatEs(idx_0).WaterE.Water.Amount <= CellUnitStatWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.4f);
                        Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).SetActive(UnitStatEs(idx_0).StepE.HaveMax(UnitEs(idx_0).MainE));



                        if (UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Protected))
                        {
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Enable();
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Color = Color.yellow;
                        }

                        else if (UnitEs(idx_0).MainE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                        {
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Enable();
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Color = Color.green;
                        }

                        else
                        {
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Disable();
                        }

                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Color = Color.blue;
                            Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).Color = Color.blue;
                        }
                        else
                        {
                            Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Color = Color.red;
                            Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).Color = Color.red;
                        }
                    }
                }
            }
        }
    }
}
