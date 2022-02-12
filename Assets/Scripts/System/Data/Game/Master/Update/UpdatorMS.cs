using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    sealed class UpdatorMS : SystemCellAbstract, IEcsRunSystem
    {
        internal UpdatorMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                for (var unit = UnitTypes.Scout; unit < UnitTypes.Camel; unit++)
                {
                    Es.ScoutHeroCooldownE(unit, player).UpdateCooldown();
                }

                Es.InventorResourcesEs.Resource(ResourceTypes.Food, player).Add(ResourcesInInventorValues.ADDING_FOOD_AFTER_MOVE);
            }

            foreach (byte idx_0 in CellWorker.Idxs)
            {
                ref var cell_0 = ref Es.CellEs(idx_0).CellE.InstanceIDC;

                var unit_0 = Es.UnitEs(idx_0).UnitE.UnitTC;
                var ownUnit_0 = Es.UnitE(idx_0).OwnerC;

                var buil_0 = Es.BuildEs(idx_0).BuildingE.BuildTC;

                foreach (var item in Es.CellEs(idx_0).TrailEs.Keys) Es.TrailEs(idx_0).Trail(item).TakeEveryUpdate();
                foreach (var item in Es.UnitEs(idx_0).CooldownKeys) Es.UnitEs(idx_0).Ability(item).TakeAfterUpdate();



                if (Es.UnitEs(idx_0).UnitE.HaveUnit && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, ownUnit_0.Player).Take(ResourcesInInventorValues.CostFoodForFeedingThem(unit_0.Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            Es.UnitE(idx_0).SetMaxHp();
                        }
                    }


                    if (Es.EffectEs(idx_0).FireE.HaveFireC.Have)
                    {
                        Es.UnitE(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (Es.UnitE(idx_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (Es.UnitE(idx_0).HaveMaxHp)
                            {
                                if (unit_0.Is(UnitTypes.Scout))
                                {
                                    if (buil_0.Is(BuildingTypes.Woodcutter) || !buil_0.Have)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (ownUnit_0.Is(PlayerTypes.First))
                                            {
                                                if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                                {
                                                    Es.BuildEs(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                Es.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                Es.BuildE(idx_camp).Destroy(Es);
                                            }

                                            Es.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!Es.UnitE(idx_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            if (Es.UnitE(idx_0).HaveSteps)
                            {
                                Es.UnitE(idx_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    Es.UnitE(idx_0).SetMaxSteps();
                }
            }

            Es.Motion.AddEveryUpdateMove();
            Es.SunSidesE.ToggleNext();
        }
    }
}