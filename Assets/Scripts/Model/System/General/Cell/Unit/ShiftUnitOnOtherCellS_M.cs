using Chessy.Model.Enum;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void ShiftUnitOnOtherCellM(in byte fromCellIdx, in byte toCellIdx)
        {
            var dataFromIdxCell = unitWhereViewDataCs[fromCellIdx].DataIdxCell;
            //var possitionFrom = _e.UnitPossitionOnCellC(fromCellIdx).Position;

            var dataToIdxCell = unitWhereViewDataCs[toCellIdx].DataIdxCell;
            //var possitionTo = _e.UnitPossitionOnCellC(toCellIdx).Position;



            _unitEs[toCellIdx].Clone(_unitEs[fromCellIdx]);
            _unitEs[fromCellIdx].Dispose();



            unitWhereViewDataCs[fromCellIdx].DataIdxCell = dataFromIdxCell;
            unitWhereViewDataCs[toCellIdx].DataIdxCell = dataToIdxCell;

            //_e.UnitPossitionOnCellC(fromCellIdx).Position = possitionFrom;
            //_e.UnitPossitionOnCellC(toCellIdx).Position = possitionTo;





            if (!unitWhereViewDataCs[toCellIdx].HaveDataReference)
            {
                //_e.UnitPossitionOnCellC(_unitWhereViewDataCs[toCellIdx).ViewIdxCell).Position = _e.CellE(toCellIdx).PositionC.Position;
            }
            unitWhereViewDataCs[unitWhereViewDataCs[toCellIdx].ViewIdxCell].DataIdxCell = toCellIdx;


            unitCs[toCellIdx].ConditionT = ConditionUnitTypes.None;

            unitCs[toCellIdx].HowManySecondUnitWasHereInThisCondition = 0;



            var directT = cellAroundCs[fromCellIdx, toCellIdx].DirectT;

            if (unitCs[toCellIdx].UnitT != UnitTypes.Undead)
            {
                if (unitCs[toCellIdx].UnitT == UnitTypes.Pawn)
                {
                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (aboutGameC.LessonT.Is(LessonTypes.ShiftPawnHere))
                        {
                            SetNextLesson();
                        }
                    }

                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        if (aboutGameC.LessonT == LessonTypes.StepAwayFromWoodcutter)
                        {
                            SetNextLesson();
                        }
                    }

                    if (aboutGameC.LessonT == LessonTypes.ComeToYourKing)
                    {
                        foreach (var cellIdx in idxsAroundCellCs[toCellIdx].IdxCellsAroundArray)
                        {
                            if (unitCs[cellIdx].UnitT == UnitTypes.King && unitCs[cellIdx].PlayerT == unitCs[toCellIdx].PlayerT)
                            {
                                SetNextLesson();
                                break;
                            }
                        }
                    }
                }





                if (unitCs[toCellIdx].UnitT == UnitTypes.Snowy)
                {
                    if (unitWaterCs[toCellIdx].HaveAnyWater())
                    {
                        environmentCs[toCellIdx].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        fireCs[toCellIdx].HaveFire = false;
                        unitWaterCs[toCellIdx].Water -= ValuesChessy.TAKING_WATER_AFTER_SHIFT_SNOWY;
                    }
                }

                if (environmentCs[fromCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    hpTrailCs[fromCellIdx].Health(directT) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }
                if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    var dirTrail = directT.Invert();

                    hpTrailCs[toCellIdx].Health(dirTrail) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }

                if (riverCs[toCellIdx].HaveRiverNear)
                {
                    TryExecuteAddingUnitAnimationM(toCellIdx);

                    unitWaterCs[toCellIdx].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;

                    if (aboutGameC.LessonT == LessonTypes.Install1WarriorsNextToTheRiver) SetNextLesson();
                }
            }


            if (unitCs[toCellIdx].UnitT == UnitTypes.Snowy)
            {
                RainyGiveWaterToUnitsAround(toCellIdx);
            }


            switch (unitCs[toCellIdx].UnitT)
            {
                case UnitTypes.Elfemale:
                    if (!environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest) && !environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.Hill))
                    {
                        if (Random.Range(0, 1f) <= ValuesChessy.PERCENT_FOR_SEEDING_YOUNG_FOREST_AFTER_SHIFT_ELFEMALE)
                        {
                            environmentCs[toCellIdx].Set(EnvironmentTypes.YoungForest, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        fireCs[toCellIdx].HaveFire = true;
                    }
                    break;
            }

            if (buildingCs[toCellIdx].HaveBuilding)
            {
                if (!buildingCs[toCellIdx].PlayerT.Is(unitCs[toCellIdx].PlayerT))
                {
                    buildingCs[toCellIdx].BuildingT = BuildingTypes.None;
                }
            }
        }
    }
}