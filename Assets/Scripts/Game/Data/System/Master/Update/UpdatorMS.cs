using Chessy.Common;
using Chessy.Game.System.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;

namespace Chessy.Game
{
    sealed class UpdatorMS : SystemAbstract, IEcsRunSystem
    {
        internal UpdatorMS(in EntitiesModel ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                //E.ResourcesC(player, ResourceTypes.Food).Resources -= E.PlayerE(player).PeopleInCity * Economy_VALUES.CostFoodForFeedingThem / 2;

                E.PlayerInfoE(player).HeroCooldownC.Cooldown--;

                E.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;
            }

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
                {
                    E.UnitEs(idx_0).CoolDownC(abilityT).Cooldown--;
                }

                if (E.UnitTC(idx_0).HaveUnit && !E.IsAnimal(E.UnitTC(idx_0).Unit))
                {
                    E.ResourcesC(E.UnitPlayerTC(idx_0).Player, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                    if (GameModeC.IsGameMode(GameModes.TrainingOff))
                    {
                        if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.Second))
                        {
                            E.UnitHpC(idx_0).Health = HpValues.MAX;
                        }
                    }


                    if (E.HaveFire(idx_0))
                    {
                        E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (E.UnitHpC(idx_0).Health >= HpValues.MAX)
                            {
                                if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (E.BuildingTC(idx_0).Is(BuildingTypes.Woodcutter) || !E.BuildingTC(idx_0).HaveBuilding)
                                    {
                                        if (GameModeC.IsGameMode(GameModes.TrainingOff))
                                        {
                                            if (E.UnitPlayerTC(idx_0).Is(PlayerTypes.First))
                                            {
                                                if (E.BuildingsInfo(E.UnitPlayerTC(idx_0).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (E.BuildingsInfo(E.UnitPlayerTC(idx_0).Player, E.BuildingLevelTC(idx_0).Level, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(idx_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(idx_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!E.UnitConditionTC(idx_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            if (E.UnitStepC(idx_0).HaveAnySteps)
                            {
                                E.UnitConditionTC(idx_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    E.UnitStepC(idx_0).Steps = StepValues.MAX;
                }
            }

            E.Motions++;
            E.SunSideTC.ToggleNext();
        }
    }
}