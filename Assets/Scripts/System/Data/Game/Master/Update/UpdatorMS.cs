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
                ref var cell_0 = ref CellEs(idx_0).CellE.InstanceIDC;

                var unit_0 = UnitEs(idx_0).TypeE.UnitTC;
                var ownUnit_0 = UnitEs(idx_0).OwnerE.OwnerC;

                var buil_0 = BuildEs(idx_0).BuildingE.BuildTC;

                foreach (var item in CellEs(idx_0).TrailEs.Keys) TrailEs(idx_0).Trail(item).TakeEveryUpdate();
                foreach (var item in UnitEs(idx_0).CooldownKeys) UnitEs(idx_0).Ability(item).TakeAfterUpdate();



                if (UnitEs(idx_0).TypeE.HaveUnit && !unit_0.IsAnimal)
                {
                    //CellUnitStepsInConditionEs.Steps(condUnit_0.Condition, idx_0)++;

                    Es.InventorResourcesEs.Resource(ResourceTypes.Food, ownUnit_0.Player).Take(ResourcesInInventorValues.CostFoodForFeedingThem(unit_0.Unit));

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (ownUnit_0.Is(PlayerTypes.Second))
                        {
                            UnitStatEs(idx_0).Hp.SetMax();
                        }
                    }


                    if (EffectEs(idx_0).FireE.HaveFireC.Have)
                    {
                        UnitEs(idx_0).ConditionE.Reset();
                    }

                    else
                    {
                        if (UnitEs(idx_0).ConditionE.ConditionTC.Is(ConditionUnitTypes.Protected))
                        {
                            if (UnitStatEs(idx_0).Hp.HaveMax)
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
                                                    BuildEs(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                            }
                                        }
                                        else
                                        {
                                            if (Es.WhereWorker.TryGetBuilding(BuildingTypes.Camp, ownUnit_0.Player, out var idx_camp))
                                            {
                                                Es.BuildE(idx_camp).Destroy(Es);
                                            }

                                            BuildEs(idx_0).BuildingE.SetNew(BuildingTypes.Camp, ownUnit_0.Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!UnitEs(idx_0).ConditionE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                        {
                            if (UnitStatEs(idx_0).StepE.HaveSteps)
                            {
                                UnitEs(idx_0).ConditionE.Set(ConditionUnitTypes.Protected);
                            }
                        }
                    }
                    UnitStatEs(idx_0).StepE.SetMax(UnitEs(idx_0).TypeE);
                }
            }

            Es.Motion.AddEveryUpdateMove();
            Es.SunSidesE.ToggleNext();
        }
    }
}