using Game.Common;
using Photon.Pun;
using static Game.Game.CellEs;
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
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                EntityPool.ScoutHeroCooldown(UnitTypes.Scout, player).Amount -= 1;
                EntityPool.ScoutHeroCooldown(UnitTypes.Elfemale, player).Amount -= 1;

                InventorResourcesE.Resource(ResourceTypes.Food, player) += EconomyValues.ADDING_FOOD_AFTER_MOVE;
            }

            foreach (byte idx_0 in Idxs)
            {
                ref var cell_0 = ref Cell<InstanceIDC>(idx_0);

                ref var unit_0 = ref CellUnitEntities.Else(idx_0).UnitC;
                ref var levUnit_0 = ref CellUnitEntities.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref CellUnitEntities.Else(idx_0).OwnerC;
                ref var hp_0 = ref CellUnitEntities.Hp(idx_0).AmountC;
                ref var condUnit_0 = ref CellUnitEntities.Else(idx_0).ConditionC;

                ref var buil_0 = ref Build<BuildingTC>(idx_0);
                ref var ownBuil_0 = ref Build<PlayerTC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

                foreach (var item in CellTrailEs.Keys) CellTrailEs.Health(item, idx_0).Take();
                foreach (var item in CellUnitEntities.CooldownKeys) CellUnitEntities.CooldownUnique(item, idx_0).Cooldown.Take();


                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    InventorResourcesE.Resource(ResourceTypes.Food, ownUnit_0.Player).Take(EconomyValues.CostFood(unit_0.Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            CellUnitEntities.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;
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
                            if (CellUnitEntities.Hp(idx_0).HaveMax)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildingTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (WhereBuildsE.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                                {
                                                    Build<BuildingTC>(idx_camp).Reset();
                                                }


                                                CellBuildE.SetNew(BuildingTypes.Camp, ownUnit_0.Player, idx_0);
                                            }
                                        }
                                        else
                                        {
                                            if (WhereBuildsE.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                Build<BuildingTC>(idx_camp).Reset();
                                            }

                                            CellBuildE.SetNew(BuildingTypes.Camp, ownUnit_0.Player, idx_0);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (CellUnitEntities.Step(idx_0).AmountC.Have)
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    CellUnitEntities.Step(idx_0).AmountC.Amount = CellUnitEntities.MaxAmountSteps(idx_0);
                }
            }

            var amountAdultForest = 0;
            foreach (var idx in Idxs)
            {
                if (EntWhereEnviroments.HaveEnv(EnvironmentTypes.AdultForest, idx).Have)
                    amountAdultForest += 1;
            }

            if (amountAdultForest <= 8)
            {
                EntityPool.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Truce);
            }

            if (EntityPool.GameInfo<AmountMotionsC>().Amount % 3 == 0)
            {
                foreach (byte idx_0 in Idxs)
                {
                    ref var build_0 = ref Build<BuildingTC>(idx_0);

                    if (Resources(EnvironmentTypes.Hill, idx_0).Have)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (Resources(EnvironmentTypes.Hill, idx_0).Amount != CellEnvironmentValues.MaxResources(EnvironmentTypes.Hill))
                            {
                                Resources(EnvironmentTypes.Hill, idx_0).Amount += 1;
                            }
                        }
                    }
                }
            }

            EntityPool.GameInfo<AmountMotionsC>().Amount += 1;



            SunSidesE.SunSideTC.ToggleNext();
        }
    }
}