using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Environment;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class FireUpdateMS : SystemModelGameAbs
    {
        internal FireUpdateMS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Run()
        {
            foreach (var cellE in eMG.CellEs(eMG.WeatherE.CloudC.Center).AroundCellsEs.AroundCellEs)
            {
                eMG.HaveFire(cellE.IdxC.Idx) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (eMG.HaveFire(cell_0))
                {
                    sMG.TakeAdultForestResourcesS.Take(EnvironmentValues.FIRE_ADULT_FOREST, cell_0);

                    if (eMG.UnitTC(cell_0).HaveUnit)
                    {
                        if (eMG.UnitTC(cell_0).Is(UnitTypes.Hell))
                        {
                            eMG.UnitHpC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                            {
                                sMG.UnitSs.AttackUnitS.Attack(HpValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                sMG.UnitSs.AttackUnitS.Attack(HpValues.FIRE_DAMAGE, eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;

                        if (UnityEngine.Random.Range(0f, 1f) < EnvironmentValues.PERCENT_SPAWN_FOR_YOUNG_FOREST_AFTER_FIRE)
                        {
                            eMG.YoungForestC(cell_0).Resources -= EnvironmentValues.FIRE_ADULT_FOREST;
                        }


                        eMG.HaveFire(cell_0) = false;


                        foreach (var cellE in eMG.CellEs(cell_0).AroundCellsEs.AroundCellEs)
                        {
                            needForFireNext.Add(cellE.IdxC.Idx);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (eMG.CellEs(cell_0).IsActiveParentSelf)
                {
                    if (eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        eMG.HaveFire(cell_0) = true;
                    }
                }
            }
        }
    }
}