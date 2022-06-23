﻿using Chessy.Model.Extensions;
using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Model.Model.System
{
    static partial class ExecuteUpdateEverythingMS
    {
        static void FireUpdate(this EntitiesModel e, SystemsModel s)
        {
            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.HaveFire(cell_0))
                {
                    if (e.UnitT(cell_0).HaveUnit())
                    {
                        if (e.UnitT(cell_0).Is(UnitTypes.Hell))
                        {
                            e.HpUnitC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (e.UnitPlayerT(cell_0).Is(PlayerTypes.None))
                            {
                                s.UnitSs.Attack(HpValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                s.UnitSs.Attack(HpValues.FIRE_DAMAGE, e.UnitPlayerT(cell_0).NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        e.SetBuildingOnCellT(cell_0, BuildingTypes.None);


                        e.HaveFire(cell_0) = false;


                        foreach (var cellE in e.AroundCellsE(cell_0).CellsAround)
                        {
                            needForFireNext.Add(cellE);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (!e.IsBorder(cell_0))
                {
                    if (e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        e.HaveFire(cell_0) = true;
                    }
                }
            }
        }

        static void TryPutOutFireWithClouds(this EntitiesModel e)
        {
            foreach (var cellE in e.AroundCellsE(e.WeatherE.CellIdxCenterCloud).CellsAround)
            {
                e.HaveFire(cellE) = false;
            }
        }

        static void BurnAdultForest(this EntitiesModel e, in SystemsModel s)
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.HaveFire(cell_0))
                {
                    s.TryTakeAdultForestResourcesM(EnvironmentValues.FIRE_ADULT_FOREST, cell_0);
                }
            }
        }
    }
}