using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed class FireUpdateMS : SystemModelGameAbs
    {
        internal FireUpdateMS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Run()
        {
            foreach (var cellE in eMG.AroundCellsE(eMG.WeatherE.CloudC.Center).CellsAround)
            {
                eMG.HaveFire(cellE) = false;
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
                            eMG.HpUnitC(cell_0).Health = HpValues.MAX;
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


                        eMG.HaveFire(cell_0) = false;


                        foreach (var cellE in eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            needForFireNext.Add(cellE);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (eMG.IsActiveParentSelf(cell_0))
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