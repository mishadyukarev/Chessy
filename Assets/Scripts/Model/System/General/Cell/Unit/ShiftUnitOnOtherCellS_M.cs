using Chessy.Model.Enum;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void ShiftUnitOnOtherCellM(in byte fromCellIdx, in byte toCellIdx)
        {
            var dataFromIdxCell = _e.SkinInfoUnitC(fromCellIdx).DataIdxCell;
            var possitionFrom = _e.UnitPossitionOnCellC(fromCellIdx).Position;

            var dataToIdxCell = _e.SkinInfoUnitC(toCellIdx).DataIdxCell;
            var possitionTo = _e.UnitPossitionOnCellC(toCellIdx).Position;



            _e.UnitE(toCellIdx) = _e.UnitE(fromCellIdx).Clone();
            _e.UnitE(fromCellIdx).Dispose();



            _e.SkinInfoUnitC(fromCellIdx).DataIdxCell = dataFromIdxCell;
            _e.SkinInfoUnitC(toCellIdx).DataIdxCell = dataToIdxCell;  

            _e.UnitPossitionOnCellC(fromCellIdx).Position = possitionFrom;
            _e.UnitPossitionOnCellC(toCellIdx).Position = possitionTo;



            

            if(!_e.SkinInfoUnitC(toCellIdx).HaveDataReference)
            {
                _e.UnitPossitionOnCellC(_e.SkinInfoUnitC(toCellIdx).ViewIdxCell).Position = _e.CellE(toCellIdx).PositionC.Position;
            }
            _e.SkinInfoUnitC(_e.SkinInfoUnitC(toCellIdx).ViewIdxCell).DataIdxCell = toCellIdx;


            _e.SetUnitConditionT(toCellIdx, ConditionUnitTypes.None);

            _e.UnitMainC(toCellIdx).HowManySecondUnitWasHereInThisCondition = 0;

           

            var directT = _e.DirectionAround(fromCellIdx, toCellIdx);

            if (!_e.UnitT(toCellIdx).Is(UnitTypes.Undead))
            {
                if (_e.UnitT(toCellIdx).Is(UnitTypes.Pawn))
                {
                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (_e.LessonT.Is(LessonTypes.ShiftPawnHere))
                        {
                             SetNextLesson();
                        }
                    }

                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        if (_e.LessonT == LessonTypes.StepAwayFromWoodcutter)
                        {
                             SetNextLesson();
                        }
                    }

                    if (_e.LessonT == LessonTypes.ComeToYourKing)
                    {
                        foreach (var cellIdx in _e.IdxsCellsAround(toCellIdx, DistanceFromCellTypes.First))
                        {
                            if (_e.UnitT(cellIdx) == UnitTypes.King && _e.UnitPlayerT(cellIdx) == _e.UnitPlayerT(toCellIdx))
                            {
                                SetNextLesson();
                                break;
                            }
                        }
                    }
                }





                if (_e.UnitT(toCellIdx).Is(UnitTypes.Snowy))
                {
                    if (_e.WaterUnitC(toCellIdx).HaveAnyWater())
                    {
                        _e.WaterOnCellC(toCellIdx).Resources = ValuesChessy.MAX_RESOURCES_ENVIRONMENT;
                        _e.HaveFire(toCellIdx) = false;
                        _e.WaterUnitC(toCellIdx).Water -= ValuesChessy.TAKING_WATER_AFTER_SHIFT_SNOWY;
                    }
                }

                if (_e.AdultForestC(fromCellIdx).HaveAnyResources)
                {
                    _e.HealthTrail(fromCellIdx).Health(directT) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }
                if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                {
                    var dirTrail = directT.Invert();

                    _e.HealthTrail(toCellIdx).Health(dirTrail) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }

                if (_e.RiverT(toCellIdx).HaveRiverNear())
                {
                    TryExecuteAddingUnitAnimationM(toCellIdx);

                    _e.WaterUnitC(toCellIdx).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;

                    if (_e.LessonT == LessonTypes.Install1WarriorsNextToTheRiver) SetNextLesson();
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
                        _e.HaveFire(toCellIdx) = true;
                    }
                    break;
            }

            if (_e.HaveBuildingOnCell(toCellIdx) && !_e.BuildingOnCellT(toCellIdx).Is(BuildingTypes.City))
            {
                if (!_e.BuildingPlayerT(toCellIdx).Is(_e.UnitPlayerT(toCellIdx)))
                {
                    _e.SetBuildingOnCellT(toCellIdx, BuildingTypes.None);
                }
            }
        }
    }
}