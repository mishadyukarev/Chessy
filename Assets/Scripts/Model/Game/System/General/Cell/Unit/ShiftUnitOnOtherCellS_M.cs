using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class ShiftUnitOnOtherCellS_M : SystemModel
    {
        internal ShiftUnitOnOtherCellS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Shift(in byte fromCellIdx, in byte toCellIdx)
        {
            sMG.UnitSs.CopyUnitFromToS.Copy(fromCellIdx, toCellIdx);
            eMG.UnitConditionTC(toCellIdx).Condition = ConditionUnitTypes.None;

            sMG.UnitSs.ClearUnit(fromCellIdx);


            var direct = eMG.AroundCellsE(fromCellIdx).Direct(toCellIdx);

            if (!eMG.UnitTC(toCellIdx).Is(UnitTypes.Undead))
            {
                if (eMG.UnitTC(toCellIdx).Is(UnitTypes.Pawn))
                {
                    if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                    if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                    if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ShiftPawnForSeedingHere))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }



                    if (eMG.ExtraToolWeaponTC(toCellIdx).Is(ToolWeaponTypes.Pick))
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ShiftHereWithPick))
                        {
                            if (toCellIdx == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                            {
                                eMG.LessonTC.SetNextLesson();
                            }
                        }
                    }

                }





                if (eMG.UnitTC(toCellIdx).Is(UnitTypes.Snowy))
                {
                    if (eMG.WaterUnitC(toCellIdx).HaveAnyWater)
                    {
                        eMG.FertilizeC(toCellIdx).Resources = EnvironmentValues.MAX_RESOURCES;
                        eMG.HaveFire(toCellIdx) = false;
                        eMG.WaterUnitC(toCellIdx).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (eMG.AdultForestC(fromCellIdx).HaveAnyResources)
                {
                    eMG.HealthTrail(fromCellIdx).Health(direct) = TrailValues.HEALTH_TRAIL;
                }
                if (eMG.AdultForestC(toCellIdx).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    eMG.HealthTrail(toCellIdx).Health(dirTrail) = TrailValues.HEALTH_TRAIL;
                }

                if (eMG.RiverTC(toCellIdx).HaveRiverNear)
                {
                    eMG.WaterUnitC(toCellIdx).Water = WaterValues.MAX;
                }


                if (eMG.UnitTC(toCellIdx).Is(UnitTypes.King))
                {
                    eMG.PlayerInfoE(eMG.UnitPlayerTC(toCellIdx).PlayerT).KingInfoE.CellKing = toCellIdx;
                }

            }


            if (eMG.UnitT(toCellIdx) == UnitTypes.Snowy)
            {
                sMG.MasterSs.RainyGiveWaterToUnitsAroundS_M.TryGive(toCellIdx);
            }


            switch (eMG.UnitT(toCellIdx))
            {
                case UnitTypes.Elfemale:
                    if (!eMG.AdultForestC(toCellIdx).HaveAnyResources && !eMG.HillC(toCellIdx).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= Values.Values.PERCENT_FOR_SEEDING_YOUNG_FOREST_AFTER_SHIFT_ELFEMALE)
                        {
                            eMG.YoungForestC(toCellIdx).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (eMG.AdultForestC(toCellIdx).HaveAnyResources)
                    {
                        eMG.HaveFire(toCellIdx) = true;
                    }
                    break;
            }

            if (eMG.BuildingTC(toCellIdx).HaveBuilding && !eMG.BuildingTC(toCellIdx).Is(BuildingTypes.City))
            {
                if (!eMG.BuildingPlayerTC(toCellIdx).Is(eMG.UnitPlayerTC(toCellIdx).PlayerT))
                {
                    eMG.BuildingTC(toCellIdx).BuildingT = BuildingTypes.None;
                }
            }
        }
    }
}