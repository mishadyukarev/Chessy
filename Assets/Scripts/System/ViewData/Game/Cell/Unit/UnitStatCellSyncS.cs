﻿using UnityEngine;
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
            foreach (byte idx_0 in Es.CellEs.Idxs)
            {
                ref var unit_0 = ref Es.CellEs.UnitEs.Main(idx_0).UnitC;
                ref var level_0 = ref Es.CellEs.UnitEs.Main(idx_0).LevelC;
                ref var ownUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).OwnerC;
                ref var hpUnit_0 = ref Es.CellEs.UnitEs.StatEs.Hp(idx_0).Health;
                ref var step_0 = ref Es.CellEs.UnitEs.StatEs.Step(idx_0).Steps;
                ref var water_0 = ref Es.CellEs.UnitEs.StatEs.Water(idx_0).Water;
                ref var condUnit_0 = ref Es.CellEs.UnitEs.Main(idx_0).ConditionC;


                Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Disable();


                Block<SpriteRendererVC>(CellBlockTypes.Condition, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).Disable();
                Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).Disable();


                if (Es.CellEs.UnitEs.VisibleE(Es.WhoseMove.CurPlayerI, idx_0).VisibleC.IsVisible)
                {
                    if (unit_0.Have)
                    {
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Enable();
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).Color = Color.red;

                        float xCordinate = (float)hpUnit_0.Amount / CellUnitHpValues.MAX_HP;
                        Bar<SpriteRendererVC>(CellBarTypes.Hp, idx_0).LocalScale = new Vector3(xCordinate * 0.67f, 0.13f, 1);

                        Block<SpriteRendererVC>(CellBlockTypes.NeedWater, idx_0).SetActive(Es.CellEs.UnitEs.StatEs.Water(idx_0).Water.Amount <= CellUnitWaterValues.MAX_WATER_WITHOUT_EFFECTS * 0.4f);
                        Block<SpriteRendererVC>(CellBlockTypes.MaxSteps, idx_0).SetActive(Es.CellEs.UnitEs.StatEs.Step(idx_0).HaveMax(Es.CellEs.UnitEs.Main(idx_0)));



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
