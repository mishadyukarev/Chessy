using Game.Common;
using Photon.Pun;
using static Game.Game.CellEs;
using static Game.Game.CellEnvironmentEs;

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
                ref var cell_0 = ref Cell(idx_0).InstanceIDC;

                ref var unit_0 = ref CellUnitEs.Else(idx_0).UnitC;
                ref var levUnit_0 = ref CellUnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref CellUnitEs.Else(idx_0).OwnerC;
                ref var hp_0 = ref CellUnitEs.Hp(idx_0).AmountC;
                ref var condUnit_0 = ref CellUnitEs.Else(idx_0).ConditionC;

                ref var buil_0 = ref CellBuildEs.Build(idx_0).BuildTC;
                ref var ownBuil_0 = ref CellBuildEs.Build(idx_0).PlayerTC;
                ref var fire_0 = ref CellFireEs.Fire(idx_0).Fire;

                foreach (var item in CellTrailEs.Keys) CellTrailEs.Trail(item, idx_0).Health.Take();
                foreach (var item in CellUnitEs.CooldownKeys) CellUnitEs.CooldownUnique(item, idx_0).Cooldown.Take();


                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    InventorResourcesE.Resource(ResourceTypes.Food, ownUnit_0.Player).Take(EconomyValues.CostFood(unit_0.Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            CellUnitEs.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;
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
                            if (CellUnitEs.Hp(idx_0).HaveMax)
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
                                                    CellBuildEs.Build(idx_camp).BuildTC.Reset();
                                                }


                                                CellBuildEs.SetNew(BuildingTypes.Camp, ownUnit_0.Player, idx_0);
                                            }
                                        }
                                        else
                                        {
                                            if (WhereBuildsE.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                CellBuildEs.Build(idx_camp).BuildTC.Reset();
                                            }

                                            CellBuildEs.SetNew(BuildingTypes.Camp, ownUnit_0.Player, idx_0);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (CellUnitEs.Step(idx_0).AmountC.Have)
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    CellUnitEs.Step(idx_0).AmountC.Amount = CellUnitEs.MaxAmountSteps(idx_0);
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
                    ref var build_0 = ref CellBuildEs.Build(idx_0).BuildTC;

                    if (Environment(EnvironmentTypes.Hill, idx_0).Resources.Have)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (Environment(EnvironmentTypes.Hill, idx_0).Resources.Amount != CellEnvironmentValues.MaxResources(EnvironmentTypes.Hill))
                            {
                                Environment(EnvironmentTypes.Hill, idx_0).Resources.Amount += 1;
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