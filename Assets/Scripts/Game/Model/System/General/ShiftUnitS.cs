using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.System.Model
{
    public struct ShiftUnitS
    {
        public ShiftUnitS(in byte idx_from, in byte idx_to, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            e.UnitEs(idx_to).Set(e.UnitEs(idx_from));
            e.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

            e.UnitTC(idx_from).Unit = UnitTypes.None;


            var direct = e.CellEs(idx_from).Direct(idx_to);

            if (!e.UnitTC(idx_to).Is(UnitTypes.Undead))
            {
                if (e.UnitTC(idx_to).Is(UnitTypes.Snowy))
                {
                    if (e.UnitWaterC(idx_to).Water > 0)
                    {
                        e.FertilizeC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        e.HaveFire(idx_to) = false;
                        e.UnitWaterC(idx_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (e.AdultForestC(idx_from).HaveAnyResources)
                {
                    e.CellEs(idx_from).TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
                }
                if (e.AdultForestC(idx_to).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    e.CellEs(idx_to).TrailHealthC(dirTrail).Health = TrailValues.HEALTH_TRAIL;
                }

                if (e.RiverEs(idx_to).RiverTC.HaveRiverNear)
                {
                    e.UnitWaterC(idx_to).Water = WaterValues.MAX;
                }
            }

            switch (e.UnitTC(idx_to).Unit)
            {
                case UnitTypes.Elfemale:
                    if (!e.AdultForestC(idx_to).HaveAnyResources && !e.HillC(idx_to).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= 0.25f)
                        {
                            e.YoungForestC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (e.AdultForestC(idx_to).HaveAnyResources)
                    {
                        e.EffectEs(idx_to).HaveFire = true;
                    }
                    break;
            }

            if (e.BuildingTC(idx_to).HaveBuilding && !e.BuildingTC(idx_to).Is(BuildingTypes.City))
            {
                if (!e.BuildingPlayerTC(idx_to).Is(e.UnitPlayerTC(idx_to).Player))
                {
                    e.BuildingTC(idx_to).Building = BuildingTypes.None;
                }
            }
        }
    }
}