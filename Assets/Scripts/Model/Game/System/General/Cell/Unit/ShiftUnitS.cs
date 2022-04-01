﻿using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;
using Chessy.Common.Entity;
using Chessy.Common.Model.System;

namespace Chessy.Game.Model.System
{
    sealed class ShiftUnitS : SystemModelGameAbs
    {
        internal ShiftUnitS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Shift(in byte cell_from, in byte cell_to)
        {
            sMG.UnitSs.SetUnitS.Set(cell_from, cell_to);
            eMG.UnitConditionTC(cell_to).Condition = ConditionUnitTypes.None;

            sMG.UnitSs.ClearUnitS.Clear(cell_from);


            var direct = eMG.CellEs(cell_from).AroundCellsEs.Direct(cell_to);

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



                    if (eMG.UnitExtraTWTC(cell_to).Is(ToolWeaponTypes.Pick))
                    {
                        if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                        {
                            eMG.LessonTC.SetNextLesson();
                        }
                    }

                }





                if (eMG.UnitTC(cell_to).Is(UnitTypes.Snowy))
                {
                    if (eMG.UnitWaterC(cell_to).Water > 0)
                    {
                        eMG.FertilizeC(cell_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        eMG.HaveFire(cell_to) = false;
                        eMG.UnitWaterC(cell_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (eMG.AdultForestC(cell_from).HaveAnyResources)
                {
                    eMG.CellEs(cell_from).TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
                }
                if (eMG.AdultForestC(cell_to).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    eMG.CellEs(cell_to).TrailHealthC(dirTrail).Health = TrailValues.HEALTH_TRAIL;
                }

                if (eMG.RiverEs(cell_to).RiverTC.HaveRiverNear)
                {
                    eMG.UnitWaterC(cell_to).Water = WaterValues.MAX;
                }


                if (eMG.UnitTC(cell_to).Is(UnitTypes.King))
                {
                    eMG.PlayerInfoE(eMG.UnitPlayerTC(cell_to).PlayerT).KingCell = cell_to;
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
                        eMG.EffectEs(cell_to).HaveFire = true;
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