using Game.Common;
using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    struct UpdatorMS : IEcsRunSystem
    {
        public void Run()
        {
            InvResC.AddStandartValues();


            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                ScoutHeroCooldownC.TakeCooldown(UnitTypes.Scout, player);
                ScoutHeroCooldownC.TakeCooldown(UnitTypes.Elfemale, player);
            }

            foreach (byte idx_0 in Idxs)
            {
                ref var cell_0 = ref Cell<CellC>(idx_0);

                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelC>(idx_0);
                ref var ownUnit_0 = ref Unit<OwnerC>(idx_0);
                ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
                ref var hp_0 = ref Unit<HpC>(idx_0);
                ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionC>(idx_0);
                ref var moveCond_0 = ref Unit<MoveInCondC>(idx_0);
                ref var stun_0 = ref Unit<StunC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var buil_0 = ref Build<BuildC>(idx_0);
                ref var ownBuil_0 = ref Build<OwnerC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);
                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);
                foreach (var item in Unique) Unit<CooldownC>(item, idx_0).Take();
                stun_0.Take();


                if (unit_0.Have)
                {
                    moveCond_0.AddMove(condUnit_0.Condition);

                    if (!unit_0.Is(UnitTypes.King)) InvResC.Take(ResTypes.Food, ownUnit_0.Owner);

                    if (GameModesCom.IsGameMode(GameModes.TrainingOff))
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
                        if (condUnit_0.Is(CondUnitTypes.Protected))
                        {
                            if (unitE_0.HaveMax)
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

                                                    Build<BuildCellEC>(idxCamp).Remove();
                                                }



                                                buildCell_0.SetNew(BuildTypes.Camp, ownUnit_0.Owner);
                                            }
                                        }
                                        else
                                        {
                                            if (WhereBuildsC.IsSetted(BuildTypes.Camp, ownUnit_0.Owner))
                                            {
                                                var idxCamp = WhereBuildsC.Idx(BuildTypes.Camp, ownUnit_0.Owner);

                                                Build<BuildCellEC>(idxCamp).Remove();
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

                    Unit<UnitCellEC>(idx_0).SetMaxSteps();
                }
            }


            if (WhereEnvC.Amount(EnvTypes.AdultForest) <= 8)
            {
                EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Truce);
            }

            if (MotionsC.AmountMotions % 3 == 0)
            {
                foreach (byte idx_0 in Idxs)
                {
                    ref var build_0 = ref Build<BuildC>(idx_0);

                    if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have)
                    {
                        if (!build_0.Is(BuildTypes.Mine))
                        {
                            if (!Environment<EnvCellEC>(EnvTypes.Hill, idx_0).HaveMax())
                            {
                                Environment<ResourcesC>(EnvTypes.Hill, idx_0).Resources += 1;
                            }
                        }
                    }
                }
            }

            MotionsC.AmountMotions += 1;
        }
    }
}