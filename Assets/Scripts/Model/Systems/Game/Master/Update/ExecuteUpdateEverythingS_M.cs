using Chessy.Common;
using Chessy.Game.Enum;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Chessy.Game.Values.Cell.Unit.Stats;
using Photon.Pun;
using System.Linq;

namespace Chessy.Game.Model.System.Master
{
    sealed class ExecuteUpdateEverythingS_M : SystemModel
    {
        readonly FireUpdateS_M _fireUpdateS;
        readonly ExecuteAIBotLogicAfterUpdateS_M _aIBotS;
        readonly TryGivePeopleToEachPlayerS_M _tryGivePeopleToEachPlayerS_M;
        readonly TrySetCamelMS _trySetCamelMS;
        readonly TryExecuteTruceMS _tryExecuteTruceS;
        readonly TryFertilizeCellsAroundCloudMS _tryFertilizeCellsAroundCloudS;
        readonly TryChangeDirectionWindMS _tryChangeDirectionWindS;
        readonly TryExecuteFarmExtractMS _tryExecuteFarmExtractS;
        readonly TryExecuteWoodcutterExtractMS _tryExecuteWoodcutterExtractS;
        readonly TryDryWetCellsMS _tryDryWetCellsS;
        readonly GiveWaterAroundRiverMS _giveWaterAroundRiverS;
        readonly GiveHealthUnitsInRelaxMS _giveHealthUnitsInRelaxS;
        readonly TryShiftWolfMS _tryShiftWolfS;
        readonly TryExecuteHungryUnitsMS _tryExecuteHungryUnitsS;
        readonly ResetEnergyUnitsMS _resetEnergyUnitsS;
        readonly TryExecuteAbilitiesGodsMS _tryExecuteAbilitiesGodsS;
        readonly TryGiveHpToBotUnitsMS _tryGiveHpToBotUnitsS;
        readonly TryExecuteThirstyMS _tryExecuteThirstyS;

        internal ExecuteUpdateEverythingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
            _fireUpdateS = new FireUpdateS_M(sMG, eMG);
            _aIBotS = new ExecuteAIBotLogicAfterUpdateS_M(sMG, eMG);

            _tryGivePeopleToEachPlayerS_M = new TryGivePeopleToEachPlayerS_M(sMG, eMG);
            _trySetCamelMS = new TrySetCamelMS(sMG, eMG);
            _tryExecuteTruceS = new TryExecuteTruceMS(sMG, eMG);
            _tryFertilizeCellsAroundCloudS = new TryFertilizeCellsAroundCloudMS(sMG, eMG);
            _tryChangeDirectionWindS = new TryChangeDirectionWindMS(sMG, eMG);
            _tryExecuteFarmExtractS = new TryExecuteFarmExtractMS(sMG, eMG);
            _tryExecuteWoodcutterExtractS = new TryExecuteWoodcutterExtractMS(sMG, eMG);
            _tryDryWetCellsS = new TryDryWetCellsMS(sMG, eMG);
            _giveWaterAroundRiverS = new GiveWaterAroundRiverMS(sMG, eMG);
            _giveHealthUnitsInRelaxS = new GiveHealthUnitsInRelaxMS(sMG, eMG);
            _tryShiftWolfS = new TryShiftWolfMS(sMG, eMG);
            _tryExecuteHungryUnitsS = new TryExecuteHungryUnitsMS(sMG, eMG);
            _resetEnergyUnitsS = new ResetEnergyUnitsMS(sMG, eMG);
            _tryExecuteAbilitiesGodsS = new TryExecuteAbilitiesGodsMS(sMG, eMG);
            _tryExecuteThirstyS = new TryExecuteThirstyMS(sMG, eMG);
        }

        internal void Run()
        {
            _fireUpdateS.Run();

            eMG.MotionsC.Motions++;
            eMG.WeatherE.SunSideTC.ToggleNext();

            _tryGivePeopleToEachPlayerS_M.TryGive();




            for (var i = 0; i < eMG.WeatherE.WindC.Speed; i++)
            {
                var cell = eMG.WeatherE.CloudC.Center;
                var xy_next = eMG.AroundCellsE(cell).AroundCellE(eMG.WeatherE.WindC.DirectT).XyC.Xy;
                var idx_next = eMG.AroundCellsE(cell).IdxCell(eMG.WeatherE.WindC.DirectT);

                bool isBorder = false;

                for (var ii = 0; ii < 10; ii++)
                {
                    if (xy_next[0] > 3 && xy_next[0] < 12 && xy_next[1] > 1 && xy_next[1] < 9)
                    {
                        eMG.WeatherE.CloudC.Center = eMG.GetIdxCellByXy(xy_next);
                    }
                    else
                    {
                        var newDir = eMG.WeatherE.WindC.DirectT;

                        newDir = newDir.Invert();
                        var newDirInt = (int)newDir;
                        newDirInt += UnityEngine.Random.Range(-1, 2);

                        if (newDirInt <= 0) newDirInt = 1;
                        else if (newDirInt >= (int)DirectTypes.End) newDirInt = newDirInt = 1;
                        eMG.WeatherE.WindC.DirectT = (DirectTypes)newDirInt;

                        isBorder = true;

                        break;
                    }
                }

                if (isBorder) break;


                for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
                {
                    eMG.HealthTrail(idx_next).Health(dirT) = 0;
                }
            }




            _tryChangeDirectionWindS.TryChange();
            _tryFertilizeCellsAroundCloudS.TryFertilize();
            _tryDryWetCellsS.TryDry();
            _giveWaterAroundRiverS.GiveWater();
            _giveHealthUnitsInRelaxS.GiveHealth();
            sMG.MasterSs.TryGiveWaterToUnitsAroundRainyS_M.TryGive();
            _tryShiftWolfS.TryShift();
            _tryExecuteHungryUnitsS.TryExecute();
            _resetEnergyUnitsS.Reset();
            _tryGiveHpToBotUnitsS.TryGive();
            _tryExecuteThirstyS.TryExecute();

            _tryExecuteFarmExtractS.TryExecute();
            _tryExecuteWoodcutterExtractS.TryExecute();




            byte cellIdx0;

            for (cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitTC(cellIdx0).HaveUnit)
                {
                    if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                    {
                        if (eMG.LessonTC.HaveLesson)
                        {
                            if (eMG.UnitTC(cellIdx0).Is(UnitTypes.King, UnitTypes.Snowy))
                            {
                                eMG.WaterUnitC(cellIdx0).Water = WaterValues.MAX;
                            }

                            if (eMG.LessonT < LessonTypes.DrinkWaterHere)
                            {
                                if (eMG.UnitT(cellIdx0) == UnitTypes.Pawn)
                                {
                                    eMG.WaterUnitC(cellIdx0).Water = WaterValues.MAX;
                                }
                           }
                        }
                    }


                    if (eMG.HaveFire(cellIdx0))
                    {
                        eMG.UnitConditionTC(cellIdx0).Condition = ConditionUnitTypes.None;
                    }

                    else
                    {
                        if (eMG.UnitConditionTC(cellIdx0).Is(ConditionUnitTypes.Protected))
                        {
                            if (eMG.HpUnitC(cellIdx0).Health >= HpValues.MAX)
                            {
                                if (eMG.MainToolWeaponTC(cellIdx0).Is(ToolWeaponTypes.Staff))
                                {
                                    if (eMG.BuildingTC(cellIdx0).Is(BuildingTypes.Woodcutter) || !eMG.BuildingTC(cellIdx0).HaveBuilding)
                                    {
                                        if (eMG.Common.GameModeTC.Is(GameModeTypes.TrainingOffline))
                                        {
                                            if (eMG.UnitPlayerTC(cellIdx0).Is(PlayerTypes.First))
                                            {
                                                if (eMG.BuildingsInfo(eMG.UnitPlayerTC(cellIdx0).PlayerT, LevelTypes.First, BuildingTypes.City).IdxC.HaveAny)
                                                {
                                                    //Es.BuildE(idx_camp).BuildingE.Destroy(Es);
                                                }


                                                //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                            }
                                        }
                                        else
                                        {
                                            if (eMG.BuildingsInfo(eMG.UnitPlayerTC(cellIdx0).PlayerT, eMG.BuildingLevelTC(cellIdx0).LevelT, BuildingTypes.Camp).IdxC.HaveAny)
                                            {
                                                //Es.BuildingE(idx_camp).Destroy(Es);
                                            }

                                            //Es.BuildE(cell_0).BuildingE.SetNew(BuildingTypes.Camp, Es.UnitPlayerTC(cell_0).Player);
                                        }
                                    }
                                }
                            }
                        }

                        else if (eMG.UnitConditionTC(cellIdx0).Is(ConditionUnitTypes.Relaxed))
                        {

                        }

                        else
                        {
                            if (eMG.StepUnitC(cellIdx0).HaveAnySteps)
                            {
                                eMG.UnitConditionTC(cellIdx0).Condition = ConditionUnitTypes.Protected;
                            }
                        }
                    }
                }
                
                if (eMG.PawnExtractAdultForestC(cellIdx0).HaveAnyResources)
                {
                    var extract = eMG.PawnExtractAdultForestC(cellIdx0).Resources;

                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cellIdx0).PlayerT).ResourcesC(ResourceTypes.Wood).Resources += extract;
                    sMG.MasterSs.TryTakeAdultForestResourcesS.TryTake(extract, cellIdx0);

                    if (eMG.AdultForestC(cellIdx0).HaveAnyResources)
                    {
                        if (eMG.BuildingTC(cellIdx0).Is(BuildingTypes.Camp) || !eMG.BuildingTC(cellIdx0).HaveBuilding)
                        {
                            sMG.BuildingSs.BuildS.Build(BuildingTypes.Woodcutter, LevelTypes.First, eMG.UnitPlayerTC(cellIdx0).PlayerT, 1, cellIdx0);
                        }

                        else if (!eMG.BuildingTC(cellIdx0).Is(BuildingTypes.Woodcutter))
                        {
                            eMG.UnitConditionTC(cellIdx0).Condition = ConditionUnitTypes.Protected;
                        }
                    }
                    else
                    {
                        sMG.BuildingSs.ClearS.Clear(cellIdx0);

                        if (eMG.LessonTC.Is(LessonTypes.RelaxExtractPawn, LessonTypes.ShiftPawnHere))
                        {
                            if (cellIdx0 == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                            {
                                eMG.LessonT = LessonTypes.RelaxExtractPawn + 1;
                            }
                        }
                    }
                }

                else if (eMG.PawnExtractHillC(cellIdx0).HaveAnyResources)
                {
                    var extract = eMG.PawnExtractHillC(cellIdx0).Resources;

                    eMG.HillC(cellIdx0).Resources -= extract;
                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cellIdx0).PlayerT).ResourcesC(ResourceTypes.Ore).Resources += extract;

                    if (!eMG.HillC(cellIdx0).HaveAnyResources)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ExtractHillPawnHere))
                        {
                            eMG.LessonTC.SetNextLesson();

                            if (eMG.IsSelectedCity)
                            {
                                eMG.LessonTC.SetNextLesson();
                            }
                        }
                        else if (eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                        {
                            eMG.LessonTC.SetNextLesson();
                            eMG.LessonTC.SetNextLesson();
                        }
                    }
                }

                else if (eMG.UnitConditionTC(cellIdx0).Is(ConditionUnitTypes.Relaxed)
                    && eMG.HpUnitC(cellIdx0).Health >= HpValues.MAX)
                {
                    eMG.UnitConditionTC(cellIdx0).Condition = ConditionUnitTypes.Protected;
                }
            }



            if (!eMG.LessonTC.HaveLesson || eMG.LessonT >= LessonTypes.Build3Farms)
            {
                for (var playerT = PlayerTypes.First; playerT < PlayerTypes.End; playerT++)
                {
                    eMG.ResourcesC(playerT, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;
                }
            }

            if (!eMG.LessonTC.HaveLesson || eMG.LessonT >= LessonTypes.Build3Farms)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    var res = ResourceTypes.Food;

                    eMG.ResourcesC(player, ResourceTypes.Food).Resources += EconomyValues.ADDING_FOOD_AFTER_UPDATE;

                    if (eMG.PlayerInfoE(player).ResourcesC(res).Resources < 0)
                    {
                        eMG.PlayerInfoE(player).ResourcesC(res).Resources = 0;


                        for (cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
                        {
                            if (eMG.UnitTC(cellIdx0).Is(UnitTypes.Pawn) && eMG.UnitPlayerTC(cellIdx0).Is(player))
                            {
                                sMG.UnitSs.KillUnitS.Kill(eMG.UnitPlayerTC(cellIdx0).PlayerT.NextPlayer(), cellIdx0);

                                sMG.UnitSs.ClearUnit(cellIdx0);
                                break;
                            }
                        }
                    }
                }
            }



            _trySetCamelMS.TrySet();
            _tryExecuteAbilitiesGodsS.TryExecute();
            _tryExecuteTruceS.TryExecute();
        }
    }
}