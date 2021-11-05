using Leopotam.Ecs;
using Photon.Pun;
using Scripts.Common;
using System;

namespace Scripts.Game
{
    public sealed class UpdatorMastSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellDataC> _cellDataFilt = default;
        private EcsFilter<CellFireDataC> _cellFireDataFilter = default;
        private EcsFilter<CellEnvDataC, CellEnvResC> _cellEnvDataFilter = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuildFilt = default;
        private EcsFilter<CellTrailDataC> _cellTrailFilt = default;

        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitMainFilt = default;
        private EcsFilter<CellUnitDataCom, HpUnitC, StepComponent> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, ConditionUnitC, ToolWeaponC, UnitEffectsC> _cellUnitOthFilt = default;

        public void Run()
        {
            InventResC.AddAmountRes(PlayerTypes.First, ResTypes.Food, 3);
            InventResC.AddAmountRes(PlayerTypes.Second, ResTypes.Food, 3);

            RpcSys.ActiveAmountMotionUIToGeneral(RpcTarget.MasterClient);

            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var cell_0 = ref _cellDataFilt.Get1(idx_0);

                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
                ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var stepUnit_0 = ref _cellUnitFilter.Get3(idx_0);
                ref var condUnit_0 = ref _cellUnitOthFilt.Get2(idx_0);
                ref var effUnit_0 = ref _cellUnitOthFilt.Get4(idx_0);

                ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuil_0 = ref _cellBuildFilt.Get2(idx_0);

                ref var fire_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);

                ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);


                if (unit_0.HaveUnit)
                {
                    if (!unit_0.Is(UnitTypes.King)) InventResC.TakeAmountRes(ownUnit_0.Owner, ResTypes.Food);

                    if (!unit_0.Is(UnitTypes.Scout))
                    {
                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                        {
                            if (ownUnit_0.Is(PlayerTypes.Second))
                            {
                                if (!hpUnit_0.HaveMaxHpUnit)
                                {
                                    hpUnit_0.AddHp(100);

                                    if (hpUnit_0.HaveMaxHpUnit)
                                    {
                                        hpUnit_0.SetMaxHp();
                                    }
                                }
                            }
                        }




                        if (fire_0.HaveFire)
                        {
                            condUnit_0.CondUnitType = CondUnitTypes.None;
                        }

                        else
                        {
                            if (condUnit_0.Is(CondUnitTypes.Protected))
                            {
                                if (hpUnit_0.HaveMaxHpUnit)
                                {
                                    if (unit_0.Is(UnitTypes.Pawn))
                                    {
                                        if(buil_0.Is(BuildTypes.Woodcutter) || !buil_0.HaveBuild)
                                        {
                                            if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                            {
                                                if (ownUnit_0.Is(PlayerTypes.First))
                                                {
                                                    if (buil_0.HaveBuild)
                                                    {
                                                        WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                                        buil_0.Reset();
                                                    }

                                                    buil_0.SetBuild(BuildTypes.Camp);
                                                    ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                    WhereBuildsC.Add(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                                }
                                            }
                                            else
                                            {
                                                if (buil_0.HaveBuild)
                                                {
                                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                                                    buil_0.Reset();
                                                }

                                                buil_0.SetBuild(BuildTypes.Camp);
                                                ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                WhereBuildsC.Add(ownUnit_0.Owner, buil_0.BuildType, idx_0);
                                            }
                                        }
                                    }
                                }
                            }

                            else if(!condUnit_0.Is(CondUnitTypes.Relaxed))
                            {
                                if (stepUnit_0.HaveMinSteps)
                                {
                                    condUnit_0.CondUnitType = CondUnitTypes.Protected;
                                }
                            }
                        }
                    }

                    stepUnit_0.SetMaxSteps(effUnit_0, unit_0.Unit, UnitsUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                }

                else
                {
                    if (buil_0.Is(BuildTypes.Camp))
                    {
                        WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.BuildType, idx_0);
                        buil_0.Reset();
                    }
                }
            }


            if (WhereEnvC.Amount(EnvTypes.AdultForest) <= 6)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.Truce);
                MastSysDataC.InvokeRun(MastDataSysTypes.Truce);
            }

            if (MotionsDataUIC.AmountMotions % 3 == 0)
            {
                foreach (byte idx_0 in _xyCellFilter)
                {
                    ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);
                    ref var envRes_0 = ref _cellEnvDataFilter.Get2(idx_0);

                    ref var build_0 = ref _cellBuildFilt.Get1(idx_0);

                    if (env_0.Have(EnvTypes.Hill))
                    {
                        if (!build_0.Is(BuildTypes.Mine))
                        {
                            if (!envRes_0.HaveMaxRes(EnvTypes.Hill))
                            {
                                envRes_0.AddAmountRes(EnvTypes.Hill);
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

                envDatCom.Set(EnvTypes.YoungForest);
                WhereEnvC.Add(EnvTypes.YoungForest, idxCellStart);
            }
        }
    }
}