using Game.Common;
using Photon.Pun;
using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityCellTrailPool;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct UpdatorMS : IEcsRunSystem
    {
        public void Run()
        {
            //InvResC.AddStandartValues();


            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Scout, player).Cooldown -= 1;
                EntityPool.ScoutHeroCooldown<CooldownC>(UnitTypes.Elfemale, player).Cooldown -= 1;
            }

            foreach (byte idx_0 in Idxs)
            {
                ref var cell_0 = ref Cell<InstanceIDC>(idx_0);

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerC>(idx_0);
                ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
                ref var hp_0 = ref Unit<HpC>(idx_0);
                ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var buil_0 = ref Build<BuildingC>(idx_0);
                ref var ownBuil_0 = ref Build<PlayerC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);
                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);
                foreach (var item in Unique) Unit<CooldownC>(item, idx_0).Take();
                Unit<NeedStepsForExitStunC>(idx_0).Steps -= 1;


                if (unit_0.Have)
                {
                    Unit<StepC>(condUnit_0.Condition, idx_0).Add();

                    if (!unit_0.Is(UnitTypes.King)) EntInventorResources.Resource<AmountC>(ResTypes.Food, ownUnit_0.Player).Amount -= 1;

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            unitE_0.SetMaxHp();
                        }
                    }


                    if (fire_0.Have)
                    {
                        if (condUnit_0.HaveCondition) condUnit_0.Reset();
                    }

                    else
                    {
                        if (condUnit_0.Is(ConditionUnitTypes.Protected))
                        {
                            if (unitE_0.HaveMax)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                //if (WhereBuildsC.IsSetted(BuildTypes.Camp, ownUnit_0.Player))
                                                //{
                                                //    var idxCamp = WhereBuildsC.Idx(BuildTypes.Camp, ownUnit_0.Player);

                                                //    Build<BuildCellEC>(idxCamp).Remove();
                                                //}



                                                buildCell_0.SetNew(BuildTypes.Camp, ownUnit_0.Player);
                                            }
                                        }
                                        else
                                        {
                                            //if (WhereBuildsC.IsSetted(BuildTypes.Camp, ownUnit_0.Player))
                                            //{
                                            //    var idxCamp = WhereBuildsC.Idx(BuildTypes.Camp, ownUnit_0.Player);

                                            //    Build<BuildCellEC>(idxCamp).Remove();
                                            //}


                                            buildCell_0.SetNew(BuildTypes.Camp, ownUnit_0.Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (stepUnit_0.HaveMin)
                            {
                                condUnit_0.Set(ConditionUnitTypes.Protected);
                            }
                        }
                    }

                    Unit<UnitCellEC>(idx_0).SetMaxSteps();
                }
            }

            var amountAdultForest = 0;
            foreach (var idx in Idxs)
            {
                if (EntWhereEnviroments.HaveEnv<HaveEnvC>(EnvTypes.AdultForest, idx).Have)
                    amountAdultForest += 1;
            }

            if (amountAdultForest <= 8)
            {
                EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Truce);
            }

            if (EntityPool.GameInfo<AmountMotionsC>().Amount % 3 == 0)
            {
                foreach (byte idx_0 in Idxs)
                {
                    ref var build_0 = ref Build<BuildingC>(idx_0);

                    if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have)
                    {
                        if (!build_0.Is(BuildTypes.Mine))
                        {
                            if (!Environment<EnvCellEC>(EnvTypes.Hill, idx_0).HaveMax())
                            {
                                Environment<AmountResourcesC>(EnvTypes.Hill, idx_0).Resources += 1;
                            }
                        }
                    }
                }
            }

            EntityPool.GameInfo<AmountMotionsC>().Amount += 1;
        }
    }
}