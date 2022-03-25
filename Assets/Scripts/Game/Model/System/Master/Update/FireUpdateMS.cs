using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.System.Model
{
    static class FireUpdateMS
    {
        public static void Run(in SystemsModelGame sMM, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            foreach (var cellE in e.CellEs(e.WeatherE.CloudC.Center).AroundCellEs)
            {
                e.HaveFire(cellE.IdxC.Idx) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.HaveFire(cell_0))
                {
                    sMM.TakeAdultForestResourcesS.Take(EnvironmentValues.FIRE_ADULT_FOREST, cell_0);

                    if (e.UnitTC(cell_0).HaveUnit)
                    {
                        if (e.UnitTC(cell_0).Is(UnitTypes.Hell))
                        {
                            e.UnitHpC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (e.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                            {
                                sMM.UnitSystems.AttackUnitS.Attack(HpValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                sMM.UnitSystems.AttackUnitS.Attack(HpValues.FIRE_DAMAGE, e.NextPlayer(e.UnitPlayerTC(cell_0).Player).Player, cell_0);
                            }
                        }
                    }

                    if (!e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        e.BuildingTC(cell_0).Building = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            e.YoungForestC(cell_0).Resources -= EnvironmentValues.FIRE_ADULT_FOREST;
                        }


                        e.HaveFire(cell_0) = false;


                        foreach (var cellE in e.CellEs(cell_0).AroundCellEs)
                        {
                            needForFireNext.Add(cellE.IdxC.Idx);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (e.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        e.HaveFire(cell_0) = true;
                    }
                }
            }
        }
    }
}