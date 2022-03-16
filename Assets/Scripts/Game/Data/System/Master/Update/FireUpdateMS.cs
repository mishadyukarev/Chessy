using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    static class FireUpdateMS
    {
        public static void Run(in EntitiesModel e)
        {
            foreach (var cellE in e.CellEs(e.CenterCloudIdxC.Idx).AroundCellEs)
            {
                e.HaveFire(cellE.IdxC.Idx) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                if (e.HaveFire(idx_0))
                {
                    TakeAdultForestResourcesS.TakeAdultForestResources(EnvironmentValues.FIRE_ADULT_FOREST, idx_0, e);

                    if (e.UnitTC(idx_0).HaveUnit)
                    {
                        if (e.UnitTC(idx_0).Is(UnitTypes.Hell))
                        {
                            e.UnitHpC(idx_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            AttackUnitS.AttackUnit(HpValues.FIRE_DAMAGE, e.NextPlayer(e.UnitPlayerTC(idx_0).Player).Player, idx_0, e);
                        }
                    }

                    if (!e.AdultForestC(idx_0).HaveAnyResources)
                    {
                        e.BuildingTC(idx_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            e.YoungForestC(idx_0).Resources -= EnvironmentValues.FIRE_ADULT_FOREST;
                        }


                        e.HaveFire(idx_0) = false;


                        foreach (var cellE in e.CellEs(idx_0).AroundCellEs)
                        {
                            needForFireNext.Add(cellE.IdxC.Idx);
                        }
                    }
                }
            }

            foreach (var idx_0 in needForFireNext)
            {
                if (e.CellEs(idx_0).IsActiveParentSelf)
                {
                    if (e.AdultForestC(idx_0).HaveAnyResources)
                    {
                        e.HaveFire(idx_0) = true;
                    }
                }
            }
        }
    }
}