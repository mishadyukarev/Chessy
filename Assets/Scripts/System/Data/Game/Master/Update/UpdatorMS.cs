using Game.Common;
using Photon.Pun;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;

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

                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var levUnit_0 = ref Unit<LevelTC>(idx_0);
                ref var ownUnit_0 = ref Unit<PlayerTC>(idx_0);
                ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
                ref var hp_0 = ref CellUnitHpEs.Hp<AmountC>(idx_0);
                ref var stepUnit_0 = ref Unit<UnitCellEC>(idx_0);
                ref var condUnit_0 = ref Unit<ConditionUnitC>(idx_0);

                ref var buildCell_0 = ref Build<BuildCellEC>(idx_0);
                ref var buil_0 = ref Build<BuildingTC>(idx_0);
                ref var ownBuil_0 = ref Build<PlayerTC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);
                ref var trail_0 = ref Trail<TrailCellEC>(idx_0);


                foreach (var item in trail_0.DictTrail) trail_0.TakeHealth(item.Key);
                foreach (var item in KeysUnique) Unit<CooldownC>(item, idx_0).Take();
                Unit<NeedStepsForExitStunC>(idx_0).Steps -= 1;


                if (unit_0.Have)
                {
                    AmountStepsInCondition<AmountC>(condUnit_0.Condition, idx_0).Add();

                    if (!unit_0.Is(UnitTypes.King)) InventorResourcesE.Resource<AmountC>(ResTypes.Food, ownUnit_0.Player).Amount -= 1;

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
                            if (CellUnitHpEs.HaveMax(idx_0))
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildingTypes.Woodcutter) || !buil_0.Have)
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


                                                CellBuildE.SetNew(BuildingTypes.Camp, ownUnit_0.Player, idx_0);
                                            }
                                        }
                                        else
                                        {
                                            //if (WhereBuildsC.IsSetted(BuildTypes.Camp, ownUnit_0.Player))
                                            //{
                                            //    var idxCamp = WhereBuildsC.Idx(BuildTypes.Camp, ownUnit_0.Player);

                                            //    Build<BuildCellEC>(idxCamp).Remove();
                                            //}

                                            CellBuildE.SetNew(BuildingTypes.Camp, ownUnit_0.Player, idx_0);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (CellUnitStepEs.HaveMin(idx_0))
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }

                    CellUnitStepEs.SetMaxSteps(idx_0);
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
                    ref var build_0 = ref Build<BuildingTC>(idx_0);

                    if (Environment<HaveEnvironmentC>(EnvTypes.Hill, idx_0).Have)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (Environment<AmountC>(EnvTypes.Hill, idx_0).Amount != Max(EnvTypes.Hill))
                            {
                                Environment<AmountC>(EnvTypes.Hill, idx_0).Amount += 1;
                            }
                        }
                    }
                }
            }

            EntityPool.GameInfo<AmountMotionsC>().Amount += 1;
        }
    }
}