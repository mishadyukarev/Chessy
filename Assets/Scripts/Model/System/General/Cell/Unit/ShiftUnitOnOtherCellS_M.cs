using Chessy.Model.Enum;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void ShiftUnitOnOtherCellM(in byte fromCellIdx, in byte toCellIdx)
        {
            UnitSs.CopyUnitFromTo(fromCellIdx, toCellIdx);
            _e.SetUnitConditionT(toCellIdx, ConditionUnitTypes.None);

            _e.UnitE(fromCellIdx).ClearEverything();


            var direct = _e.AroundCellsE(fromCellIdx).Direct(toCellIdx);

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

                    //if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                    //{
                    //    if (_eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                    //    {
                    //        _eMG.LessonTC.SetNextLesson();
                    //    }
                    //}

                    if (toCellIdx == KeyIndexCellsForLesson.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        if (_e.LessonT == LessonTypes.StepAwayFromWoodcutter)
                        {
                             SetNextLesson();
                        }
                    }

                    //if (toCellIdx == StartValues.CELL_IDX_FOR_SHIFT_PAWN_TO_FIRE_ADULT_FOREST)
                    //{
                    //    if (_eMG.LessonT == LessonTypes.ShiftPawnForFireForestHere)
                    //    {
                    //        if(_eMG.MainToolWeaponTC(_eMG.CurrentCellIdx).Is(ToolWeaponTypes.Axe))
                    //        {
                    //            _eMG.LessonTC.SetNextLesson();
                    //        }
                    //    }
                    //}

                    if (_e.LessonT == LessonTypes.ComeToYourKing)
                    {
                        foreach (var cellIdx in _e.AroundCellsE(toCellIdx).CellsAround)
                        {
                            if (_e.UnitT(cellIdx) == UnitTypes.King && _e.UnitPlayerT(cellIdx) == _e.UnitPlayerT(toCellIdx))
                            {
                                SetNextLesson();
                                break;
                            }
                        }
                    }



                    //if (_eMG.ExtraToolWeaponTC(toCellIdx).Is(ToolWeaponTypes.Pick))
                    //{
                    //    if (_eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                    //    {
                    //        if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                    //        {
                    //            _eMG.LessonTC.SetNextLesson();
                    //        }
                    //    }
                    //}
                }





                if (_e.UnitT(toCellIdx).Is(UnitTypes.Snowy))
                {
                    if (_e.WaterUnitC(toCellIdx).HaveAnyWater())
                    {
                        _e.WaterOnCellC(toCellIdx).Resources = ValuesChessy.MAX_RESOURCES;
                        _e.HaveFire(toCellIdx) = false;
                        _e.WaterUnitC(toCellIdx).Water -= ValuesChessy.TAKING_WATER_AFTER_SHIFT_SNOWY;
                    }
                }

                if (_e.AdultForestC(fromCellIdx).HaveAnyResources)
                {
                    _e.HealthTrail(fromCellIdx).Health(direct) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }
                if (_e.AdultForestC(toCellIdx).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    _e.HealthTrail(toCellIdx).Health(dirTrail) = ValuesChessy.HEALTH_TRAIL_ANY_TRAIL;
                }

                if (_e.RiverT(toCellIdx).HaveRiverNear())
                {
                    _e.WaterUnitC(toCellIdx).Water = ValuesChessy.MAX_WATER_FOR_ANY_UNIT;
                }


                //if (_e.UnitT(toCellIdx).Is(UnitTypes.King))
                //{
                //    _e.PlayerInfoE(_e.UnitPlayerT(toCellIdx)).KingInfoE.CellKing = toCellIdx;
                //}

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
                            _e.YoungForestC(toCellIdx).Resources = ValuesChessy.MAX_RESOURCES;
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