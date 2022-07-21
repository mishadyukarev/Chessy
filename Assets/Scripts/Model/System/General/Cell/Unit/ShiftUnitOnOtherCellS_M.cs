using Chessy.Model.Enum;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void ShiftUnitOnOtherCellM(in byte fromCellIdx, in byte toCellIdx)
        {
            var dataFromIdxCell = _unitWhereViewDataCs[fromCellIdx].DataIdxCell;
            //var possitionFrom = _e.UnitPossitionOnCellC(fromCellIdx).Position;

            var dataToIdxCell = _unitWhereViewDataCs[toCellIdx].DataIdxCell;
            //var possitionTo = _e.UnitPossitionOnCellC(toCellIdx).Position;



            _unitEs[toCellIdx].Clone(_unitEs[fromCellIdx]);
            _unitEs[fromCellIdx].Dispose();



            _unitWhereViewDataCs[fromCellIdx].DataIdxCell = dataFromIdxCell;
            _unitWhereViewDataCs[toCellIdx].DataIdxCell = dataToIdxCell;  

            //_e.UnitPossitionOnCellC(fromCellIdx).Position = possitionFrom;
            //_e.UnitPossitionOnCellC(toCellIdx).Position = possitionTo;



            

            if(!_unitWhereViewDataCs[toCellIdx].HaveDataReference)
            {
                //_e.UnitPossitionOnCellC(_unitWhereViewDataCs[toCellIdx).ViewIdxCell).Position = _e.CellE(toCellIdx).PositionC.Position;
            }
            _unitWhereViewDataCs[_unitWhereViewDataCs[toCellIdx].ViewIdxCell].DataIdxCell = toCellIdx;


            _unitCs[toCellIdx].ConditionT = ConditionUnitTypes.None;

            _unitCs[toCellIdx].HowManySecondUnitWasHereInThisCondition = 0;

           

            var directT = _cellAroundCs[fromCellIdx, toCellIdx].DirectT;

            if (_unitCs[toCellIdx].UnitT != UnitTypes.Undead)
            {
                if (_unitCs[toCellIdx].UnitT == UnitTypes.Pawn)
                {
                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (_aboutGameC.LessonT.Is(LessonTypes.ShiftPawnHere))
                        {
                             SetNextLesson();
                        }
                    }

                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        if (_aboutGameC.LessonT == LessonTypes.StepAwayFromWoodcutter)
                        {
                             SetNextLesson();
                        }
                    }

                    if (_aboutGameC.LessonT == LessonTypes.ComeToYourKing)
                    {
                        foreach (var cellIdx in _idxsAroundCellCs[toCellIdx].IdxCellsAroundArray)
                        {
                            if (_unitCs[cellIdx].UnitT == UnitTypes.King && _unitCs[cellIdx].PlayerT == _unitCs[toCellIdx].PlayerT)
                            {
                                SetNextLesson();
                                break;
                            }
                        }
                    }
                }





                if (_unitCs[toCellIdx].UnitT == UnitTypes.Snowy)
                {
                    if (_unitWaterCs[toCellIdx].HaveAnyWater())
                    {
                        _environmentCs[toCellIdx].Set(EnvironmentTypes.Fertilizer, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        _fireCs[toCellIdx].HaveFire = false;
                        _unitWaterCs[toCellIdx].Water -= ValuesChessy.TAKING_WATER_AFTER_SHIFT_SNOWY;
                    }
                }

                if (_environmentCs[fromCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _hpTrailCs[fromCellIdx].Health(directT) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }
                if (_environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    var dirTrail = directT.Invert();

                    _hpTrailCs[toCellIdx].Health(dirTrail) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }

                if (_riverCs[toCellIdx].HaveRiverNear)
                {
                    TryExecuteAddingUnitAnimationM(toCellIdx);

                    _unitWaterCs[toCellIdx].Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;

                    if (_aboutGameC.LessonT == LessonTypes.Install1WarriorsNextToTheRiver) SetNextLesson();
                }
            }


            if (_unitCs[toCellIdx].UnitT == UnitTypes.Snowy)
            {
                RainyGiveWaterToUnitsAround(toCellIdx);
            }


            switch (_unitCs[toCellIdx].UnitT)
            {
                case UnitTypes.Elfemale:
                    if (!_environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest) && !_environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.Hill))
                    {
                        if (Random.Range(0, 1f) <= ValuesChessy.PERCENT_FOR_SEEDING_YOUNG_FOREST_AFTER_SHIFT_ELFEMALE)
                        {
                            _environmentCs[toCellIdx].Set(EnvironmentTypes.YoungForest, ValuesChessy.MAX_RESOURCES_ENVIRONMENT);
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (_environmentCs[toCellIdx].HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        _fireCs[toCellIdx].HaveFire = true;
                    }
                    break;
            }

            if (_buildingCs[toCellIdx].HaveBuilding)
            {
                if (!_buildingCs[toCellIdx].PlayerT.Is(_unitCs[toCellIdx].PlayerT))
                {
                    _buildingCs[toCellIdx].BuildingT = BuildingTypes.None;
                }
            }
        }
    }
}