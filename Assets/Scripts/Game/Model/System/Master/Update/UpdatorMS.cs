using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class UpdatorMS
    {
        public void Run(in GameModeTC gameModeTC, in EntitiesModelGame e)
        {
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                //E.ResourcesC(player, ResourceTypes.Food).Resources -= E.PlayerE(player).PeopleInCity * Economy_VALUES.CostFoodForFeedingThem / 2;

                e.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;
            }

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.UnitTC(cell_0).HaveUnit && !e.UnitTC(cell_0).IsAnimal)
                {
                    if (e.UnitTC(cell_0).Is(UnitTypes.Pawn)) e.ResourcesC(e.UnitPlayerTC(cell_0).Player, ResourceTypes.Food).Resources -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                    if (gameModeTC.Is(GameModes.TrainingOff))
                    {
                        if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.Second))
                        {
                            e.UnitHpC(cell_0).Health = HpValues.MAX;
                        }
                    }


                    if (e.HaveFire(cell_0))
                    {
                        e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Protected))
                        {
                            if (e.UnitHpC(cell_0).Health >= HpValues.MAX)
                            {
                                if (e.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (e.BuildingTC(cell_0).Is(BuildingTypes.Woodcutter) || !e.BuildingTC(cell_0).HaveBuilding)
                                    {
                                        if (gameModeTC.Is(GameModes.TrainingOff))
                                        {
                                            if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                                            {
                                                if (e.BuildingsInfo(e.UnitPlayerTC(cell_0).Player, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (e.BuildingsInfo(e.UnitPlayerTC(cell_0).Player, e.BuildingLevelTC(cell_0).Level, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (!e.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                        {
                            if (e.UnitStepC(cell_0).HaveAnySteps)
                            {
                                e.UnitConditionTC(cell_0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                    e.UnitStepC(cell_0).Steps = StepValues.MAX;
                }
            }

            e.MotionsC.Motions++;
            e.WeatherE.SunC.ToggleNext();
        }
    }
}