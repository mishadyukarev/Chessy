using UnityEngine;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellVEs;
using static Game.Game.CellBlocksVEs;
using static Game.Game.CellBarsVEs;

namespace Game.Game
{
    struct UnitStatCellSyncS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Else(idx_0).UnitC;
                ref var level_0 = ref CellUnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;
                ref var hpUnit_0 = ref CellUnitEs.Hp(idx_0).AmountC;
                ref var step_0 = ref CellUnitEs.Step(idx_0).AmountC;
                ref var water_0 = ref CellUnitEs.Water(idx_0).AmountC;
                ref var condUnit_0 = ref CellUnitEs.Else(idx_0).ConditionC;


                Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Disable();


                Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).Disable();


                if (CellUnitEs.VisibleE(Entities.WhoseMoveE.CurPlayerI, idx_0).VisibleC.IsVisible)
                {
                    if (unit_0.Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Color = Color.red;

                        float xCordinate = (float)hpUnit_0.Amount / UnitHpValues.MAX_HP;
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).LocalScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                        Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).SetActive(CellUnitEs.Water(idx_0).AmountC.Amount <= CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.4f);
                        Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).SetActive(CellUnitEs.Step(idx_0).AmountC.Amount >= CellUnitEs.MaxAmountSteps(idx_0));

                        

                        if (condUnit_0.Is(ConditionUnitTypes.Protected))
                        {
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Enable();
                            Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Color = Color.yellow;
                        }

                        else if (condUnit_0.Is(ConditionUnitTypes.Relaxed))
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
