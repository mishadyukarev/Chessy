using Game.Common;
using Photon.Pun;

namespace Game.Game
{
    sealed class UpdatorMS : SystemAbstract, IEcsRunSystem
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

                foreach (var item in Es.CellEs(idx_0).TrailEs.Keys) Es.TrailEs(idx_0).Trail(item).TakeEveryUpdate();
                foreach (var item in Es.UnitEs(idx_0).CooldownKeys) Es.UnitEs(idx_0).Ability(item).Cooldown--;



                if (Es.UnitEs(idx_0).UnitE.HaveUnit && !Es.UnitE(idx_0).IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, Es.UnitE(idx_0).Owner).Take(ResourcesInInventorValues.CostFoodForFeedingThem(Es.UnitE(idx_0).Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitE(idx_0).Is(PlayerTypes.Second))
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
                                if (Es.UnitE(idx_0).Is(UnitTypes.Scout))
                                {
                                    if (Es.BuildingE(idx_0).Is(BuildingTypes.Woodcutter) || !Es.BuildingE(idx_0).HaveBuilding)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (Es.UnitE(idx_0).Is(PlayerTypes.First))
                                            {
                                                if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, Es.UnitE(idx_0).Owner, out var idx_camp))
                                                {
                                                    Es.BuildingEs(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                Es.BuildingEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitE(idx_0).Owner);
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, Es.UnitE(idx_0).Owner, out var idx_camp))
                                            {
                                                Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            Es.BuildingEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitE(idx_0).Owner);
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