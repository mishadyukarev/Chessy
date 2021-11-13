using Leopotam.Ecs;
using Photon.Pun;
using Chessy.Common;
using System;

namespace Chessy.Game
{
    public sealed class UpdatorMastSys : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<CellC> _cellDataFilt = default;
        private EcsFilter<FireC> _cellFireDataFilter = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvDataFilter = default;
        private EcsFilter<BuildC, OwnerC> _cellBuildFilt = default;
        private EcsFilter<TrailC> _cellTrailFilt = default;

        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitMainFilt = default;
        private EcsFilter<UnitC, HpC, StepC> _cellUnitFilter = default;
        private EcsFilter<UnitC, ConditionUnitC, MoveInCondC> _cellUnitCondFilt = default;
        private EcsFilter<UnitC, UniqAbilC> _unitUniqFilt = default;
        private EcsFilter<UnitC, UnitEffectsC, StunC> _unitEffFilt = default;

        public void Run()
        {
            InventResC.AddAmountRes(PlayerTypes.First, ResTypes.Food, 3);
            InventResC.AddAmountRes(PlayerTypes.Second, ResTypes.Food, 3);


            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.First, UnitTypes.Scout);
            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.Second, UnitTypes.Scout);

            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.First, UnitTypes.Elfemale);
            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.Second, UnitTypes.Elfemale);


            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var cell_0 = ref _cellDataFilt.Get1(idx_0);

                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var ownUnit_0 = ref _cellUnitMainFilt.Get3(idx_0);
                ref var hpUnit_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var stepUnit_0 = ref _cellUnitFilter.Get3(idx_0);
                ref var condUnit_0 = ref _cellUnitCondFilt.Get2(idx_0);
                ref var moveCond_0 = ref _cellUnitCondFilt.Get3(idx_0);
                ref var uniq_0 = ref _unitUniqFilt.Get2(idx_0);
                ref var effUnit_0 = ref _unitEffFilt.Get2(idx_0);
                ref var stun_0 = ref _unitEffFilt.Get3(idx_0);


                ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuil_0 = ref _cellBuildFilt.Get2(idx_0);
                ref var fire_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);
                ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);
                foreach (var item in uniq_0.Cooldowns) uniq_0.TakeCooldown(item.Key);
                stun_0.Take();


                if (unit_0.HaveUnit)
                {
                    moveCond_0.AddMove(condUnit_0.Condition);

                    if (!unit_0.Is(UnitTypes.King)) InventResC.TakeAmountRes(ownUnit_0.Owner, ResTypes.Food);

                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            if (!hpUnit_0.HaveMaxHp)
                            {
                                hpUnit_0.AddHp(100);

                                if (hpUnit_0.HaveMaxHp)
                                {
                                    hpUnit_0.SetMaxHp();
                                }
                            }
                        }
                    }


                    if (fire_0.HaveFire)
                    {
                        if (condUnit_0.HaveCondition) condUnit_0.Def();
                    }

                    else
                    {
                        if (condUnit_0.Is(CondUnitTypes.Protected))
                        {
                            if (hpUnit_0.HaveMaxHp)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildTypes.Woodcutter) || !buil_0.HaveBuild)
                                    {
                                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (buil_0.HaveBuild)
                                                {
                                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.Build, idx_0);
                                                    buil_0.Reset();
                                                }

                                                if (WhereBuildsC.IsSettedCamp(ownUnit_0.Owner))
                                                {
                                                    var idxCamp = WhereBuildsC.IdxCamp(ownUnit_0.Owner);

                                                    WhereBuildsC.Remove(ownUnit_0.Owner, BuildTypes.Camp, idxCamp);
                                                    _cellBuildFilt.Get1(idxCamp).Reset();
                                                }


                                                buil_0.Build = BuildTypes.Camp;
                                                ownBuil_0.SetOwner(ownUnit_0.Owner);
                                                WhereBuildsC.Add(ownBuil_0.Owner, buil_0.Build, idx_0);
                                            }
                                        }
                                        else
                                        {
                                            if (buil_0.HaveBuild)
                                            {
                                                WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.Build, idx_0);
                                                buil_0.Reset();
                                            }

                                            if (WhereBuildsC.IsSettedCamp(ownUnit_0.Owner))
                                            {
                                                var idxCamp = WhereBuildsC.IdxCamp(ownUnit_0.Owner);

                                                WhereBuildsC.Remove(ownUnit_0.Owner, BuildTypes.Camp, idxCamp);
                                                _cellBuildFilt.Get1(idxCamp).Reset();
                                            }


                                            buil_0.Build = BuildTypes.Camp;
                                            ownBuil_0.SetOwner(ownUnit_0.Owner);
                                            WhereBuildsC.Add(ownUnit_0.Owner, buil_0.Build, idx_0);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(CondUnitTypes.Relaxed))
                        {
                            if (stepUnit_0.HaveMinSteps)
                            {
                                condUnit_0.Set(CondUnitTypes.Protected);
                            }
                        }
                    }

                    stepUnit_0.SetMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(ownUnit_0.Owner, unit_0.Unit));
                }

                //else
                //{
                //    if (buil_0.Is(BuildTypes.Camp))
                //    {
                //        WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.Build, idx_0);
                //        buil_0.Reset();
                //    }
                //}
            }


            if (WhereEnvC.Amount(EnvTypes.AdultForest) <= 6)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipGameTypes.Truce);
                DataMastC.InvokeRun(MastDataSysTypes.Truce);
            }

            if (MotionsC.AmountMotions % 3 == 0)
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

            MotionsC.AmountMotions += 1;
        }
    }
}