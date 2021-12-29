using Leopotam.Ecs;
using UnityEngine;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class UnitStatCellSyncS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _unitF = default;
        private EcsFilter<HpC, StepC, WaterC> _statUnitF = default;
        private EcsFilter<ConditionC, EffectsC> _effUnitF = default;

        private EcsFilter<BarsVC> _cellBarsFilter = default;
        private EcsFilter<BlocksVC> _cellBlocksFilter = default;

        public void Run()
        {
            foreach (byte idx_0 in _statUnitF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var level_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);
                ref var visUnit_0 = ref _unitF.Get4(idx_0);

                ref var hpUnit_0 = ref _statUnitF.Get1(idx_0);
                ref var step_0 = ref _statUnitF.Get2(idx_0);
                ref var water_0 = ref _statUnitF.Get3(idx_0);

                ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
                ref var effUnit_0 = ref _effUnitF.Get2(idx_0);
                      
                

                ref var barsViewCom = ref _cellBarsFilter.Get1(idx_0);
                ref var blocksViewCom = ref _cellBlocksFilter.Get1(idx_0);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);


                if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.Have)
                    {

                            barsViewCom.EnableSR(CellBarTypes.Hp);
                            barsViewCom.SetColorHp(Color.red);

                            float maxHpUnit_0 = HpUnitWC.MAX;

                            float xCordinate = (float)hpUnit_0.Hp / maxHpUnit_0;
                            barsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));
                        

                        if (Unit<WaterUnitC>(idx_0).NeedWater)
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.NeedWater);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);
                        }

                        if (Unit<StepUnitWC>(idx_0).HaveMaxSteps)
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (condUnit_0.Is(CondUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (condUnit_0.Is(CondUnitTypes.Relaxed))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.green);
                        }

                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                        }

                        if (ownUnit_0.Is(PlayerTypes.First))
                        {
                            barsViewCom.SetColorHp(Color.blue);
                            blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.blue);
                        }
                        else
                        {
                            barsViewCom.SetColorHp(Color.red);
                            blocksViewCom.SetColor(CellBlockTypes.MaxSteps, Color.red);
                        }
                    }
                }
            }
        }
    }
}
