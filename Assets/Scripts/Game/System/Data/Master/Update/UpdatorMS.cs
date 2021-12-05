using Leopotam.Ecs;
using Photon.Pun;
using Game.Common;
using System;
using UnityEngine;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class UpdatorMS : IEcsRunSystem
    {
        private EcsFilter<FireC> _cellFireDataFilter = default;
        private EcsFilter<EnvC, EnvResC> _cellEnvDataFilter = default;

        private EcsFilter<UnitC, LevelC, OwnerC> _unitF = default;
        private EcsFilter<HpC, StepC> _statUnitF = default;
        private EcsFilter<ConditionC, MoveInCondC, EffectsC, StunC> _effUnitF = default;
        private EcsFilter<CooldownUniqC> _unitUniqF = default;

        public void Run()
        {
            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                InvResC.Add(res, PlayerTypes.First, EconomyValues.Adding(res));
            }

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                ScoutHeroCooldownC.TakeCooldown(UnitTypes.Scout, player);
                ScoutHeroCooldownC.TakeCooldown(UnitTypes.Elfemale, player);
            }

            foreach (byte idx_0 in EntityPool.Idxs)
            {
                ref var cell_0 = ref EntityPool.Cell<CellC>(idx_0);

                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);

                ref var hpUnitCell_0 = ref Unit<HpUnitWC>(idx_0);
                ref var hp_0 = ref _statUnitF.Get1(idx_0);
                ref var stepUnit_0 = ref Unit<StepUnitWC>(idx_0);

                ref var condUnit_0 = ref _effUnitF.Get1(idx_0);
                ref var moveCond_0 = ref _effUnitF.Get2(idx_0);
                ref var effUnit_0 = ref _effUnitF.Get3(idx_0);
                ref var stun_0 = ref _effUnitF.Get4(idx_0);

                ref var cdUniq_0 = ref _unitUniqF.Get1(idx_0);


                ref var buildCell_0 = ref Build<BuildCellC>(idx_0);
                ref var buil_0 = ref EntityPool.Build<BuildC>(idx_0);
                ref var ownBuil_0 = ref EntityPool.Build<OwnerC>(idx_0);
                ref var fire_0 = ref _cellFireDataFilter.Get1(idx_0);
                ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);
                ref var trail_0 = ref EntityPool.Trail<TrailC>(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);
                foreach (var item in cdUniq_0.Cooldowns) cdUniq_0.TakeCooldown(item.Key);
                stun_0.Take();


                if (unit_0.Have)
                {
                    moveCond_0.AddMove(condUnit_0.Condition);

                    if (!unit_0.Is(UnitTypes.King)) InvResC.Take(ResTypes.Food, ownUnit_0.Owner);

                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            hpUnitCell_0.SetMax();
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
                            if (hpUnitCell_0.HaveMax)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModesCom.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (WhereBuildsC.IsSetted(BuildTypes.Camp, ownUnit_0.Owner))
                                                {
                                                    var idxCamp = WhereBuildsC.Idx(BuildTypes.Camp, ownUnit_0.Owner);

                                                    Build<BuildCellC>(idxCamp).Remove();
                                                }



                                                buildCell_0.SetNew(BuildTypes.Camp, ownUnit_0.Owner);
                                            }
                                        }
                                        else
                                        {
                                            if (WhereBuildsC.IsSetted(BuildTypes.Camp, ownUnit_0.Owner))
                                            {
                                                var idxCamp = WhereBuildsC.Idx(BuildTypes.Camp, ownUnit_0.Owner);

                                                Build<BuildCellC>(idxCamp).Remove();
                                            }


                                            buildCell_0.SetNew(BuildTypes.Camp, ownUnit_0.Owner);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(CondUnitTypes.Relaxed))
                        {
                            if (stepUnit_0.HaveMin)
                            {
                                condUnit_0.Set(CondUnitTypes.Protected);
                            }
                        }
                    }

                    Unit<StepUnitWC>(idx_0).SetMaxSteps();
                }
            }


            if (WhereEnvC.Amount(EnvTypes.AdultForest) <= 8)
            {
                RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                DataMastSC.InvokeRun(MastDataSysTypes.Truce);
            }

            if (MotionsC.AmountMotions % 3 == 0)
            {
                foreach (byte idx_0 in EntityPool.Idxs)
                {
                    ref var env_0 = ref _cellEnvDataFilter.Get1(idx_0);
                    ref var envRes_0 = ref _cellEnvDataFilter.Get2(idx_0);

                    ref var build_0 = ref EntityPool.Build<BuildC>(idx_0);

                    if (env_0.Have(EnvTypes.Hill))
                    {
                        if (!build_0.Is(BuildTypes.Mine))
                        {
                            if (!envRes_0.HaveMax(EnvTypes.Hill))
                            {
                                envRes_0.Add(EnvTypes.Hill);
                            }
                        }
                    }
                }
            }

            MotionsC.AmountMotions += 1;
        }
    }
}