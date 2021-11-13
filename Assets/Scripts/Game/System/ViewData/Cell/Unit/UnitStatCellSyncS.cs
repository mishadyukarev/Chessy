﻿using Leopotam.Ecs;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class UnitStatCellSyncS : IEcsRunSystem
    {
        private EcsFilter<UnitC, HpC, StepC, ConditionUnitC, OwnerC, VisibleC> _cellUnitFilter = default;
        private EcsFilter<UnitC, ConditionUnitC, UnitEffectsC, WaterUnitC, OwnerC> _cellUnitOthFilt = default;
        private EcsFilter<UnitC, VisibleC> _cellUnitViewFilt = default;
        private EcsFilter<BarsVC> _cellBarsFilter = default;
        private EcsFilter<BlocksVC> _cellBlocksFilter = default;

        public void Run()
        {
            foreach (byte idx in _cellUnitFilter)
            {
                ref var unit_0 = ref _cellUnitFilter.Get1(idx);
                ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx);
                ref var curStepUnitC = ref _cellUnitFilter.Get3(idx);
                ref var condUnitC = ref _cellUnitOthFilt.Get2(idx);
                ref var effUnit_0 = ref _cellUnitOthFilt.Get3(idx);
                ref var thirUnitC_0 = ref _cellUnitOthFilt.Get4(idx);
                ref var ownUnit_0 = ref _cellUnitOthFilt.Get5(idx);
                ref var visUnit_0 = ref _cellUnitViewFilt.Get2(idx);

                ref var barsViewCom = ref _cellBarsFilter.Get1(idx);
                ref var blocksViewCom = ref _cellBlocksFilter.Get1(idx);


                barsViewCom.DisableSR(CellBarTypes.Hp);

                blocksViewCom.DisableBlockSR(CellBlockTypes.Condition);
                blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);


                if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                {
                    if (unit_0.HaveUnit)
                    {

                            barsViewCom.EnableSR(CellBarTypes.Hp);
                            barsViewCom.SetColorHp(Color.red);

                            float maxHpUnit_0 = HpC.MAX_HP;

                            float xCordinate = (float)hpUnit_0.Hp / maxHpUnit_0;
                            barsViewCom.SetScale(CellBarTypes.Hp, new Vector3(xCordinate * 0.67f, 0.13f, 1));
                        

                        if (thirUnitC_0.NeedWater)
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.NeedWater);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.NeedWater);
                        }

                        if (curStepUnitC.HaveMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit)))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.MaxSteps);
                        }
                        else
                        {
                            blocksViewCom.DisableBlockSR(CellBlockTypes.MaxSteps);
                        }

                        if (condUnitC.Is(CondUnitTypes.Protected))
                        {
                            blocksViewCom.EnableBlockSR(CellBlockTypes.Condition);
                            blocksViewCom.SetColor(CellBlockTypes.Condition, Color.yellow);
                        }

                        else if (condUnitC.Is(CondUnitTypes.Relaxed))
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
