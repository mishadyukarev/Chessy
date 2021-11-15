﻿using Leopotam.Ecs;
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

        private EcsFilter<UnitC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionUnitC, MoveInCondC, UnitEffectsC, StunC> _effUnitF = default;
        private EcsFilter<CooldownUniqC> _unitUniqF = default;

        public void Run()
        {
            InvResC.AddAmountRes(PlayerTypes.First, ResTypes.Food, 3);
            InvResC.AddAmountRes(PlayerTypes.Second, ResTypes.Food, 3);


            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.First, UnitTypes.Scout);
            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.Second, UnitTypes.Scout);

            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.First, UnitTypes.Elfemale);
            ScoutHeroCooldownC.TakeCooldown(PlayerTypes.Second, UnitTypes.Elfemale);


            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var cell_0 = ref _cellDataFilt.Get1(idx_0);

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var own_0 = ref _unitF.Get2(idx_0);

                ref var hp_0 = ref _statUnitF.Get1(idx_0);
                ref var step_0 = ref _statUnitF.Get2(idx_0);

                ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
                ref var moveCond_0 = ref _effUnitF.Get2(idx_0);
                ref var effUnit_0 = ref _effUnitF.Get3(idx_0);
                ref var stun_0 = ref _effUnitF.Get4(idx_0);

                ref var cdUniq_0 = ref _unitUniqF.Get1(idx_0);
                


                ref var buil_0 = ref _cellBuildFilt.Get1(idx_0);
                ref var ownBuil_0 = ref _cellBuildFilt.Get2(idx_0);
                ref var fire_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);
                ref var trail_0 = ref _cellTrailFilt.Get1(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);
                foreach (var item in cdUniq_0.Cooldowns) cdUniq_0.TakeCooldown(item.Key);
                stun_0.Take();


                if (unit_0.HaveUnit)
                {
                    moveCond_0.AddMove(condUnit_0.Condition);

                    if (!unit_0.Is(UnitTypes.King)) InvResC.TakeAmountRes(own_0.Owner, ResTypes.Food);

                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                    {
                        if (own_0.Is(PlayerTypes.Second))
                        {
                            if (!hp_0.HaveMaxHp)
                            {
                                hp_0.AddHp(100);

                                if (hp_0.HaveMaxHp)
                                {
                                    hp_0.SetMaxHp();
                                }
                            }
                        }
                    }


                    if (fire_0.Have)
                    {
                        if (condUnit_0.HaveCondition) condUnit_0.Reset();
                    }

                    else
                    {
                        if (condUnit_0.Is(CondUnitTypes.Protected))
                        {
                            if (hp_0.HaveMaxHp)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (own_0.Is(PlayerTypes.First))
                                            {
                                                if (buil_0.Have)
                                                {
                                                    WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.Build, idx_0);
                                                    buil_0.Remove();
                                                }

                                                if (WhereBuildsC.IsSettedCamp(own_0.Owner))
                                                {
                                                    var idxCamp = WhereBuildsC.IdxCamp(own_0.Owner);

                                                    WhereBuildsC.Remove(own_0.Owner, BuildTypes.Camp, idxCamp);
                                                    _cellBuildFilt.Get1(idxCamp).Remove();
                                                }


                                                buil_0.SetNew(BuildTypes.Camp);
                                                ownBuil_0.SetOwner(own_0.Owner);
                                                WhereBuildsC.Add(ownBuil_0.Owner, buil_0.Build, idx_0);
                                            }
                                        }
                                        else
                                        {
                                            if (buil_0.Have)
                                            {
                                                WhereBuildsC.Remove(ownBuil_0.Owner, buil_0.Build, idx_0);
                                                buil_0.Remove();
                                            }

                                            if (WhereBuildsC.IsSettedCamp(own_0.Owner))
                                            {
                                                var idxCamp = WhereBuildsC.IdxCamp(own_0.Owner);

                                                WhereBuildsC.Remove(own_0.Owner, BuildTypes.Camp, idxCamp);
                                                _cellBuildFilt.Get1(idxCamp).Remove();
                                            }


                                            buil_0.SetNew(BuildTypes.Camp);
                                            ownBuil_0.SetOwner(own_0.Owner);
                                            WhereBuildsC.Add(own_0.Owner, buil_0.Build, idx_0);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(CondUnitTypes.Relaxed))
                        {
                            if (step_0.HaveMinSteps)
                            {
                                condUnit_0.Set(CondUnitTypes.Protected);
                            }
                        }
                    }

                    step_0.SetMaxSteps(unit_0.Unit, effUnit_0.Have(UnitStatTypes.Steps), UnitStepUpgC.UpgSteps(own_0.Owner, unit_0.Unit));
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
                RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                DataMastSC.InvokeRun(MastDataSysTypes.Truce);
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