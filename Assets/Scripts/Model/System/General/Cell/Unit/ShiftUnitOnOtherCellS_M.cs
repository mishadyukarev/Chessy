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



            _e.UnitE(toCellIdx).Clone(_e.UnitE(fromCellIdx));
            _e.UnitE(fromCellIdx).Dispose();



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

           

            var directT = _e.CellAroundC(fromCellIdx, toCellIdx).DirectT;

            if (!_e.UnitT(toCellIdx).Is(UnitTypes.Undead))
            {
                if (_e.UnitT(toCellIdx).Is(UnitTypes.Pawn))
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
                        foreach (var cellIdx in _e.IdxsCellsAround(toCellIdx))
                        {
                            if (_e.UnitT(cellIdx) == UnitTypes.King && _unitCs[cellIdx].PlayerT == _unitCs[toCellIdx].PlayerT)
                            {
                                SetNextLesson();
                                break;
                            }
                        }
                    }
                }





                if (_e.UnitT(toCellIdx).Is(UnitTypes.Snowy))
                {
                    if (_unitWaterCs[toCellIdx].HaveAnyWater())
                    {
                        _e.WaterOnCellC(toCellIdx).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                        _fireCs[toCellIdx].HaveFire = false;
                        _unitWaterCs[toCellIdx].Water -= ValuesChessy.TAKING_WATER_AFTER_SHIFT_SNOWY;
                    }
                }

                if (_e.AdultForestC(fromCellIdx).HaveAnyResources)
                {
                    _hpTrailCs[fromCellIdx].Health(directT) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }
                if (_e.AdultForestC(toCellIdx).HaveAnyResources)
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


            if (_e.UnitT(toCellIdx) == UnitTypes.Snowy)
            {
                RainyGiveWaterToUnitsAround(toCellIdx);
            }


            switch (_e.UnitT(toCellIdx))
            {
                case UnitTypes.Elfemale:
                    if (!_e.AdultForestC(toCellIdx).HaveAnyResources && !_e.HillC(toCellIdx).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= ValuesChessy.PERCENT_FOR_SEEDING_YOUNG_FOREST_AFTER_SHIFT_ELFEMALE)
                        {
                            _e.YoungForestC(toCellIdx).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (_e.AdultForestC(toCellIdx).HaveAnyResources)
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