using Chessy.Game.Entity.Model;
using Chessy.Game.Values.Cell;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using UnityEngine;

namespace Chessy.Game.System.Model
{
    public sealed class ShiftUnitS : SystemModelGameAbs
    {
        readonly SystemsModelGame _systems;
        readonly CellEs _cellEs;

        public ShiftUnitS(in SystemsModelGame systems, in CellEs cellEs, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _systems = systems;
            _cellEs = cellEs;
        }

        public void Shift(in byte idx_to)
        {
            _systems.CellSs(idx_to).SetUnitS.Set(_cellEs.UnitEs);
            eMGame.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

            _cellEs.UnitMainE.UnitTC.Unit = UnitTypes.None;


            var direct = _cellEs.AroundCellsEs.Direct(idx_to);

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

                if (_cellEs.EnvironmentEs.AdultForestC.HaveAnyResources)
                {
                    _cellEs.TrailHealthC(direct).Health = TrailValues.HEALTH_TRAIL;
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