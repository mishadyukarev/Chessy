using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void ShiftUnitOnOtherCellM(in byte fromCellIdx, in byte toCellIdx)
        {
            UnitSs.CopyUnitFromTo(fromCellIdx, toCellIdx);
            _eMG.UnitConditionTC(toCellIdx).Condition = ConditionUnitTypes.None;

            UnitSs.ClearUnit(fromCellIdx);


            var direct = _eMG.AroundCellsE(fromCellIdx).Direct(toCellIdx);

            if (!_eMG.UnitTC(toCellIdx).Is(UnitTypes.Undead))
            {
                if (_eMG.UnitTC(toCellIdx).Is(UnitTypes.Pawn))
                {
                    if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (_eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                        {
                            _eMG.LessonTC.SetNextLesson();
                        }
                    }

                    //if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                    //{
                    //    if (_eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                    //    {
                    //        _eMG.LessonTC.SetNextLesson();
                    //    }
                    //}

                    if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_StepAwayFromWoodcutter)
                    {
                        if (_eMG.LessonT == LessonTypes.StepAwayFromWoodcutter)
                        {
                            _eMG.LessonTC.SetNextLesson();
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

                    if (_eMG.LessonT == LessonTypes.ComeToYourKing)
                    {
                        foreach (var cellIdx in _eMG.AroundCellsE(toCellIdx).CellsAround)
                        {
                            if (_eMG.UnitT(cellIdx) == UnitTypes.King && _eMG.UnitPlayerT(cellIdx) == _eMG.UnitPlayerT(toCellIdx))
                            {
                                _eMG.LessonTC.SetNextLesson();
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





                if (_eMG.UnitTC(toCellIdx).Is(UnitTypes.Snowy))
                {
                    if (_eMG.WaterUnitC(toCellIdx).HaveAnyWater)
                    {
                        _eMG.FertilizeC(toCellIdx).Resources = EnvironmentValues.MAX_RESOURCES;
                        _eMG.HaveFire(toCellIdx) = false;
                        _eMG.WaterUnitC(toCellIdx).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (_eMG.AdultForestC(fromCellIdx).HaveAnyResources)
                {
                    _eMG.HealthTrail(fromCellIdx).Health(direct) = TrailValues.HEALTH_TRAIL;
                }
                if (_eMG.AdultForestC(toCellIdx).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    _eMG.HealthTrail(toCellIdx).Health(dirTrail) = TrailValues.HEALTH_TRAIL;
                }

                if (_eMG.RiverTC(toCellIdx).HaveRiverNear)
                {
                    _eMG.WaterUnitC(toCellIdx).Water = WaterValues.MAX;
                }


                if (_eMG.UnitTC(toCellIdx).Is(UnitTypes.King))
                {
                    _eMG.PlayerInfoE(_eMG.UnitPlayerTC(toCellIdx).PlayerT).KingInfoE.CellKing = toCellIdx;
                }

            }


            if (_eMG.UnitT(toCellIdx) == UnitTypes.Snowy)
            {
                ExecuteUpdateEverythingMS.GiveWaterToUnitsAroundRainy(toCellIdx);
            }


            switch (_eMG.UnitT(toCellIdx))
            {
                case UnitTypes.Elfemale:
                    if (!_eMG.AdultForestC(toCellIdx).HaveAnyResources && !_eMG.HillC(toCellIdx).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= Values.Values.PERCENT_FOR_SEEDING_YOUNG_FOREST_AFTER_SHIFT_ELFEMALE)
                        {
                            _eMG.YoungForestC(toCellIdx).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (_eMG.AdultForestC(toCellIdx).HaveAnyResources)
                    {
                        _eMG.HaveFire(toCellIdx) = true;
                    }
                    break;
            }

            if (_eMG.BuildingTC(toCellIdx).HaveBuilding && !_eMG.BuildingTC(toCellIdx).Is(BuildingTypes.City))
            {
                if (!_eMG.BuildingPlayerTC(toCellIdx).Is(_eMG.UnitPlayerTC(toCellIdx).PlayerT))
                {
                    _eMG.BuildingTC(toCellIdx).BuildingT = BuildingTypes.None;
                }
            }
        }
    }
}