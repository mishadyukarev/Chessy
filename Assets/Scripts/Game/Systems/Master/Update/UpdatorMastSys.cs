using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    internal sealed class UpdatorMastSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;

        private EcsFilter<CellFireDataComponent> _cellFireDataFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataC,  OwnerCom> _cellBuildFilt = default;

        public void Run()
        {
            var curXy = new byte[0];

            InventResC.AddAmountRes(PlayerTypes.First, ResTypes.Food, 3);
            InventResC.AddAmountRes(PlayerTypes.Second, ResTypes.Food, 3);

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.MasterClient);

            foreach (byte idx_0 in _xyCellFilter)
            {
                curXy = _xyCellFilter.GetXyCell(idx_0);

                ref var curCellDatC = ref _cellDataFilt.Get1(idx_0);

                ref var unitC_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levUnitC_0 = ref _cellUnitMainFilt.Get2(idx_0);
                ref var ownUnitC_0 = ref _cellUnitMainFilt.Get3(idx_0);
                ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var curStepUnitC = ref _cellUnitFilter.Get3(idx_0);
                ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
                ref var twUnitC = ref _cellUnitOthFilt.Get3(idx_0);
                ref var effUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);

                ref var builC_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuilC_0 = ref _cellBuildFilt.Get2(idx_0);

                ref var fireC_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var envrC_0 = ref _cellEnvDataFilter.Get1(idx_0);


                if (unitC_0.HaveUnit)
                {
                    if (!unitC_0.Is(UnitTypes.King)) InventResC.TakeAmountRes(ownUnitC_0.Owner, ResTypes.Food);

                    if (!unitC_0.Is(UnitTypes.Scout))
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (ownUnitC_0.Is(PlayerTypes.Second))
                            {
                                if (!hpUnit_0.HaveMaxHpUnit(effUnit_0, unitC_0.Unit))
                                {
                                    hpUnit_0.AddHp(100);

                                    if (hpUnit_0.HaveMaxHpUnit(effUnit_0, unitC_0.Unit))
                                    {
                                        hpUnit_0.SetMaxHp(effUnit_0, unitC_0.Unit);
                                    }
                                }
                            }
                        }




                        if (fireC_0.HaveFire)
                        {
                            condUnit_0.CondUnitType = CondUnitTypes.None;
                        }

                        else
                        {
                            if (condUnit_0.Is(CondUnitTypes.Protected))
                            {
                                if (hpUnit_0.HaveMaxHpUnit(effUnit_0, unitC_0.Unit))
                                {
                                    if (unitC_0.Is(UnitTypes.Pawn))
                                    {
                                        if (envrC_0.Have(EnvTypes.AdultForest))
                                        {
                                            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                            {
                                                if (ownUnitC_0.Is(PlayerTypes.First))
                                                {
                                                    if (builC_0.HaveBuild)
                                                    {
                                                        WhereBuildsC.Remove(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
                                                        builC_0.NoneBuild();
                                                    }

                                                    builC_0.SetBuild(BuildTypes.Camp);
                                                    ownBuilC_0.SetOwner(ownUnitC_0.Owner);
                                                    WhereBuildsC.Add(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
                                                }
                                            }
                                            else
                                            {
                                                if (builC_0.HaveBuild)
                                                {
                                                    WhereBuildsC.Remove(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
                                                    builC_0.NoneBuild();
                                                }

                                                builC_0.SetBuild(BuildTypes.Camp);
                                                ownBuilC_0.SetOwner(ownUnitC_0.Owner);
                                                WhereBuildsC.Add(ownUnitC_0.Owner, builC_0.BuildType, idx_0);
                                            }
                                        }
                                    }
                                }
                            }

                            else if(!condUnit_0.Is(CondUnitTypes.Relaxed))
                            {
                                if (curStepUnitC.HaveMinSteps)
                                {
                                    condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                }
                            }
                        }
                    }

                    curStepUnitC.SetMaxSteps(effUnit_0, unitC_0.Unit);
                }

                else
                {
                    if (builC_0.Is(BuildTypes.Camp))
                    {
                        WhereBuildsC.Remove(ownBuilC_0.Owner, builC_0.BuildType, idx_0);
                        builC_0.NoneBuild();
                    }
                }
            }


            if (WhereEnvC.Amount(EnvTypes.AdultForest) <= 6)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, SoundEffectTypes.Truce);
                GameMasSysDataM.TruceSystems.Run();
            }

            if (MotionsDataUIC.AmountMotions % 3 == 0)
            {
                foreach (byte curIdxCell in _xyCellFilter)
                {
                    ref var curEnvrDatCom = ref _cellEnvDataFilter.Get1(curIdxCell);
                    ref var curBuildDatCom = ref _cellBuildFilt.Get1(curIdxCell);

                    if (curEnvrDatCom.Have(EnvTypes.Hill))
                    {
                        if (!curBuildDatCom.Is(BuildTypes.Mine))
                        {
                            if (!curEnvrDatCom.HaveMaxRes(EnvTypes.Hill))
                            {
                                curEnvrDatCom.AddAmountRes(EnvTypes.Hill);
                            }
                        }
                    }
                }
            }

            MotionsDataUIC.AmountMotions += 1;
        }

        private void SpawnNewSeed(byte idxCellStart)
        {
            if (UnityEngine.Random.Range(0, 100) < 70)
            {
                ref var envDatCom = ref _cellEnvDataFilter.Get1(idxCellStart);

                envDatCom.SetNew(EnvTypes.YoungForest);
                WhereEnvC.Add(EnvTypes.YoungForest, idxCellStart);
            }
        }
    }
}