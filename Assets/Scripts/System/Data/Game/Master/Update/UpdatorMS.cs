using Game.Common;
using Photon.Pun;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellEs;

namespace Game.Game
{
    struct UpdatorMS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                Entities.ScoutHeroCooldownE(UnitTypes.Scout, player).Cooldown.Amount -= 1;
                Entities.ScoutHeroCooldownE(UnitTypes.Elfemale, player).Cooldown.Amount -= 1;

                InventorResourcesE.Resource(ResourceTypes.Food, player) += EconomyValues.ADDING_FOOD_AFTER_MOVE;
            }

            foreach (byte idx_0 in Entities.CellEs.Idxs)
            {
                ref var cell_0 = ref Entities.CellEs.CellE(idx_0).InstanceIDC;

                ref var unit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).UnitC;
                ref var levUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).LevelC;
                ref var ownUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).OwnerC;
                ref var hp_0 = ref Entities.CellEs.UnitEs.Hp(idx_0).AmountC;
                ref var condUnit_0 = ref Entities.CellEs.UnitEs.Else(idx_0).ConditionC;

                ref var buil_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;
                ref var ownBuil_0 = ref Entities.CellEs.BuildEs.Build(idx_0).PlayerTC;
                ref var fire_0 = ref Entities.CellEs.FireEs.Fire(idx_0).Fire;

                foreach (var item in Entities.CellEs.TrailEs.Keys) Entities.CellEs.TrailEs.Trail(item, idx_0).Health.Take();
                foreach (var item in Entities.CellEs.UnitEs.CooldownKeys) Entities.CellEs.UnitEs.CooldownUnique(item, idx_0).Cooldown.Take();
                


                if (unit_0.Have && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    InventorResourcesE.Resource(ResourceTypes.Food, ownUnit_0.Player).Take(EconomyValues.CostFood(unit_0.Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            Entities.CellEs.UnitEs.Hp(idx_0).AmountC.Amount = UnitHpValues.MAX_HP;
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
                            if (Entities.CellEs.UnitEs.Hp(idx_0).HaveMax)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildingTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (Entities.WhereBuildingEs.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                                {
                                                    Entities.CellEs.BuildEs.Build(idx_camp).BuildTC.Reset();
                                                }


                                                Entities.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                                Entities.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                            }
                                        }
                                        else
                                        {
                                            if (Entities.WhereBuildingEs.IsSetted(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                Entities.CellEs.BuildEs.Build(idx_camp).BuildTC.Reset();
                                            }

                                            Entities.CellEs.BuildEs.Build(idx_0).SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                            Entities.WhereBuildingEs.HaveBuild(BuildingTypes.Camp, ownUnit_0.Player, idx_0).HaveBuilding.Have = true;
                                        }
                                    }
                                }
                            }
                        }

                        else if (!condUnit_0.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (Entities.CellEs.UnitEs.Step(idx_0).Steps.Have)
                            {
                                condUnit_0.Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    Entities.CellEs.UnitEs.Step(idx_0).SetMax(Entities.CellEs.UnitEs.Else(idx_0));
                }
            }

            var amountAdultForest = 0;
            foreach (var idx in Entities.CellEs.Idxs)
            {
                if (EntWhereEnviroments.HaveEnv(EnvironmentTypes.AdultForest, idx).Have)
                    amountAdultForest += 1;
            }

            if (amountAdultForest <= 8)
            {
                Entities.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.Truce);
                SystemDataMasterManager.InvokeRun(SystemDataMasterTypes.Truce);
            }

            if (Entities.Motion.AmountMotions.Amount % 3 == 0)
            {
                foreach (byte idx_0 in Entities.CellEs.Idxs)
                {
                    ref var build_0 = ref Entities.CellEs.BuildEs.Build(idx_0).BuildTC;

                    if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx_0).Resources.Have)
                    {
                        if (!build_0.Is(BuildingTypes.Mine))
                        {
                            if (Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx_0).Resources.Amount != CellEnvironmentValues.MaxResources(EnvironmentTypes.Hill))
                            {
                                Entities.CellEs.EnvironmentEs.Environment(EnvironmentTypes.Hill, idx_0).Resources.Amount += 1;
                            }
                        }
                    }
                }
            }

            Entities.Motion.AmountMotions.Amount += 1;



            Entities.SunSidesE.SunSideTC.ToggleNext();
        }
    }
}