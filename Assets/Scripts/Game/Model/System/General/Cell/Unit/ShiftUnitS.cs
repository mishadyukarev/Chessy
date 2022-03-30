using Chessy.Game.Entity.Model;
using Chessy.Game.Enum;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.System.Model
{
    sealed class ShiftUnitS : SystemModelGameAbs
    {
        internal ShiftUnitS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Shift(in byte cell_from, in byte cell_to)
        {
            s.SetUnitS.Set(cell_from, cell_to);
            e.UnitConditionTC(cell_to).Condition = ConditionUnitTypes.None;

            s.ClearUnitS.Clear(cell_from);


            var direct = e.CellEs(cell_from).AroundCellsEs.Direct(cell_to);

            if (!e.UnitTC(cell_to).Is(UnitTypes.Undead))
            {
                if (e.UnitTC(cell_to).Is(UnitTypes.Pawn))
                {
                    if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_TO_FOREST_LESSON)
                    {
                        if (e.LessonTC.Is(LessonTypes.ShiftPawnHere))
                        {
                            e.LessonTC.SetNextLesson();
                        }
                    }

                    if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_FOR_DRINKING_LESSON)
                    {
                        if (e.LessonTC.Is(LessonTypes.DrinkWaterHere))
                        {
                            e.LessonTC.SetNextLesson();
                        }
                    }

                    if (e.UnitExtraTWTC(cell_to).Is(ToolWeaponTypes.Pick))
                    {
                        if (cell_to == StartValues.CELL_FOR_SHIFT_PAWN_FOR_EXTRACING_HILL_LESSON)
                        {
                            e.LessonTC.SetNextLesson();
                        }
                    }

                }





                if (e.UnitTC(cell_to).Is(UnitTypes.Snowy))
                {
                    if (e.UnitWaterC(cell_to).Water > 0)
                    {
                        e.FertilizeC(cell_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        e.HaveFire(cell_to) = false;
                        e.UnitWaterC(cell_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (e.AdultForestC(cell_from).HaveAnyResources)
                {
                    e.CellEs(cell_from).TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
                }
                if (e.AdultForestC(cell_to).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    e.CellEs(cell_to).TrailHealthC(dirTrail).Health = TrailValues.HEALTH_TRAIL;
                }

                if (e.RiverEs(cell_to).RiverTC.HaveRiverNear)
                {
                    e.UnitWaterC(cell_to).Water = WaterValues.MAX;
                }


                if (e.UnitTC(cell_to).Is(UnitTypes.King))
                {
                    e.PlayerInfoE(e.UnitPlayerTC(cell_to).Player).KingCell = cell_to;
                }

            }

            switch (e.UnitTC(cell_to).Unit)
            {
                case UnitTypes.Elfemale:
                    if (!e.AdultForestC(cell_to).HaveAnyResources && !e.HillC(cell_to).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= 0.25f)
                        {
                            e.YoungForestC(cell_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (e.AdultForestC(cell_to).HaveAnyResources)
                    {
                        e.EffectEs(cell_to).HaveFire = true;
                    }
                    break;
            }

            if (e.BuildingTC(cell_to).HaveBuilding && !e.BuildingTC(cell_to).Is(BuildingTypes.City))
            {
                if (!e.BuildingPlayerTC(cell_to).Is(e.UnitPlayerTC(cell_to).Player))
                {
                    e.BuildingTC(cell_to).Building = BuildingTypes.None;
                }
            }
        }
    }
}