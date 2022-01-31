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
            foreach (byte idx_0 in CellEs.Idxs)
            {
                var unit_0 = UnitEs.Main(idx_0).UnitTC;
                var ownUnit_0 = UnitEs.Main(idx_0).OwnerC;
                var hpUnit_0 = UnitEs.StatEs.Hp(idx_0).Health;
                var step_0 = UnitEs.StatEs.Step(idx_0).Steps;
                var water_0 = UnitEs.StatEs.Water(idx_0).Water;


                Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Disable();


                Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).Disable();


                if (UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_0).IsVisibleC.IsVisible)
                {
                    if (unit_0.Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Color = Color.red;

                        float xCordinate = (float)hpUnit_0.Amount / CellUnitHpValues.MAX_HP;
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).LocalScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                        Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).SetActive(UnitEs.StatEs.Water(idx_0).Water.Amount <= CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.4f);
                        Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).SetActive(UnitEs.StatEs.Step(idx_0).HaveMax(UnitEs.Main(idx_0)));



                        if (UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Protected))
                        {
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Enable();
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Color = Color.yellow;
                        }

                        else if (UnitEs.Main(idx_0).ConditionTC.Is(ConditionUnitTypes.Relaxed))
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
