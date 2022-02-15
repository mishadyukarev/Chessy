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
                    Es.ScoutHeroCooldownE(unit, player).CooldownC.Amount--;
                }

                Es.InventorResourcesEs.Resource(ResourceTypes.Food, player).ResourceC.Add(ResourcesInInventorValues.ADDING_FOOD_AFTER_MOVE);
            }

            foreach (byte idx_0 in CellWorker.Idxs)
            {
                ref var cell_0 = ref Es.CellEs(idx_0).CellE.InstanceIDC;

                foreach (var item in Es.CellEs(idx_0).TrailEs.Keys) Es.TrailEs(idx_0).Trail(item).HealthC.Take(1);
                foreach (var item in Es.UnitEs(idx_0).CooldownKeys) Es.UnitEs(idx_0).Ability(item).Cooldown--;



                if (Es.UnitTC(idx_0).HaveUnit && !Es.UnitTC(idx_0).IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, Es.UnitPlayerTC(idx_0).Player).ResourceC.Take(ResourcesInInventorValues.CostFoodForFeedingThem(Es.UnitTC(idx_0).Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (Es.UnitPlayerTC(idx_0).Is(PlayerTypes.Second))
                        {
                            Es.UnitHpC(idx_0).Health = CellUnitStatHpValues.MAX_HP;
                        }
                    }


                    if (Es.EffectEs(idx_0).FireE.HaveFireC.Have)
                    {
                        Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (Es.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (Es.UnitHpC(idx_0).Health >= CellUnitStatHpValues.MAX_HP)
                            {
                                if (Es.UnitTC(idx_0).Is(UnitTypes.Scout))
                                {
                                    if (Es.BuildingE(idx_0).Is(BuildingTypes.Woodcutter) || !Es.BuildingE(idx_0).HaveBuilding)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (Es.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                                            {
                                                if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player, out var idx_camp))
                                                {
                                                    Es.BuildEs(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                Es.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player, out var idx_camp))
                                            {
                                                Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            Es.BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!Es.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            if (Es.UnitStepC(idx_0).HaveSteps)
                            {
                                Es.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    Es.UnitStepC(idx_0).Set(CellUnitStatStepValues.StandartStepsUnit(Es.UnitTC(idx_0).Unit));
                }
            }

            Es.MotionsC.Add(1);
            Es.SunSideTC.ToggleNext();
        }
    }
}