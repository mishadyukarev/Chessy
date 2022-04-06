using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class ShiftUnitS : SystemModel
    {
        internal ShiftUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Shift(in byte cell_from, in byte cell_to)
        {
            sMG.UnitSs.SetUnitS.Set(cell_from, cell_to);
            eMG.UnitConditionTC(cell_to).Condition = ConditionUnitTypes.None;

            sMG.UnitSs.ClearUnitS.Clear(cell_from);


            var direct = eMG.AroundCellsE(cell_from).Direct(cell_to);

            if (!eMG.UnitTC(cell_to).Is(UnitTypes.Undead))
            {
                if (eMG.UnitTC(cell_to).Is(UnitTypes.Pawn))
                {
                    if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ShiftPawnHere))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                    if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.DrinkWaterHere))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                    if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_FOR_SEEDING_LESSON)
                    {
                        if (eMG.LessonTC.Is(LessonTypes.ShiftPawnForSeedingHere))
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }



                    if (eMG.ExtraToolWeaponTC(cell_to).Is(ToolWeaponTypes.Pick))
                    {
                        if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                }





                if (eMG.UnitTC(cell_to).Is(UnitTypes.Snowy))
                {
                    if (eMG.WaterUnitC(cell_to).Water > 0)
                    {
                        eMG.FertilizeC(cell_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        eMG.HaveFire(cell_to) = false;
                        eMG.WaterUnitC(cell_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (eMG.AdultForestC(cell_from).HaveAnyResources)
                {
                    eMG.HealthTrail(cell_from).Health(direct) = TrailValues.HEALTH_TRAIL;
                }
                if (eMG.AdultForestC(cell_to).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    eMG.HealthTrail(cell_to).Health(dirTrail) = TrailValues.HEALTH_TRAIL;
                }

                if (eMG.RiverTC(cell_to).HaveRiverNear)
                {
                    eMG.WaterUnitC(cell_to).Water = WaterValues.MAX;
                }


                if (eMG.UnitTC(cell_to).Is(UnitTypes.King))
                {
                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_to).PlayerT).KingInfoE.CellKing = cell_to;
                }

            }

            switch (eMG.UnitTC(cell_to).UnitT)
            {
                case UnitTypes.Elfemale:
                    if (!eMG.AdultForestC(cell_to).HaveAnyResources && !eMG.HillC(cell_to).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= 0.25f)
                        {
                            eMG.YoungForestC(cell_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (eMG.AdultForestC(cell_to).HaveAnyResources)
                    {
                        eMG.HaveFire(cell_to) = true;
                    }
                    break;
            }

            if (eMG.BuildingTC(cell_to).HaveBuilding && !eMG.BuildingTC(cell_to).Is(BuildingTypes.City))
            {
                if (!eMG.BuildingPlayerTC(cell_to).Is(eMG.UnitPlayerTC(cell_to).PlayerT))
                {
                    eMG.BuildingTC(cell_to).BuildingT = BuildingTypes.None;
                }
            }
        }
    }
}