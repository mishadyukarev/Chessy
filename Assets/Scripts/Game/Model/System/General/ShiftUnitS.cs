using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.System.Model
{
    public sealed class ShiftUnitS : SystemModelGameAbs
    {
        public ShiftUnitS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Shift(in byte idx_from, in byte idx_to)
        {
            eMGame.UnitEs(idx_to).Set(eMGame.UnitEs(idx_from));
            eMGame.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

            eMGame.UnitTC(idx_from).Unit = UnitTypes.None;


            var direct = eMGame.CellEs(idx_from).Direct(idx_to);

            if (!eMGame.UnitTC(idx_to).Is(UnitTypes.Undead))
            {
                if (eMGame.UnitTC(idx_to).Is(UnitTypes.Snowy))
                {
                    if (eMGame.UnitWaterC(idx_to).Water > 0)
                    {
                        eMGame.FertilizeC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        eMGame.HaveFire(idx_to) = false;
                        eMGame.UnitWaterC(idx_to).Water -= WaterValues.AFTER_SHIFT_SNOWY;
                    }
                }

                if (eMGame.AdultForestC(idx_from).HaveAnyResources)
                {
                    eMGame.CellEs(idx_from).TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
                }
                if (eMGame.AdultForestC(idx_to).HaveAnyResources)
                {
                    var dirTrail = direct.Invert();

                    eMGame.CellEs(idx_to).TrailHealthC(dirTrail).Health = TrailValues.HEALTH_TRAIL;
                }

                if (eMGame.RiverEs(idx_to).RiverTC.HaveRiverNear)
                {
                    eMGame.UnitWaterC(idx_to).Water = WaterValues.MAX;
                }
            }

            switch (eMGame.UnitTC(idx_to).Unit)
            {
                case UnitTypes.Elfemale:
                    if (!eMGame.AdultForestC(idx_to).HaveAnyResources && !eMGame.HillC(idx_to).HaveAnyResources)
                    {
                        if (Random.Range(0, 1f) <= 0.25f)
                        {
                            eMGame.YoungForestC(idx_to).Resources = EnvironmentValues.MAX_RESOURCES;
                        }
                    }
                    break;

                case UnitTypes.Hell:
                    if (eMGame.AdultForestC(idx_to).HaveAnyResources)
                    {
                        eMGame.EffectEs(idx_to).HaveFire = true;
                    }
                    break;
            }

            if (eMGame.BuildingTC(idx_to).HaveBuilding && !eMGame.BuildingTC(idx_to).Is(BuildingTypes.City))
            {
                if (!eMGame.BuildingPlayerTC(idx_to).Is(eMGame.UnitPlayerTC(idx_to).Player))
                {
                    eMGame.BuildingTC(idx_to).Building = BuildingTypes.None;
                }
            }
        }
    }
}