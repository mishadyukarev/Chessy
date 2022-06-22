﻿using Chessy.Game.Extensions;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModel
    {
        void FireUpdate()
        {
            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_e.HaveFire(cell_0))
                {
                    if (_e.UnitT(cell_0).HaveUnit())
                    {
                        if (_e.UnitT(cell_0).Is(UnitTypes.Hell))
                        {
                            _e.HpUnitC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (_e.UnitPlayerT(cell_0).Is(PlayerTypes.None))
                            {
                                _s.UnitSs.Attack(HpValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                _s.UnitSs.Attack(HpValues.FIRE_DAMAGE, _e.UnitPlayerT(cell_0).NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!_e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _e.SetBuildingOnCellT(cell_0, BuildingTypes.None);


                        _e.HaveFire(cell_0) = false;


                        foreach (var cellE in _e.AroundCellsE(cell_0).CellsAround)
                        {
                            needForFireNext.Add(cellE);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (!_e.IsBorder(cell_0))
                {
                    if (_e.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _e.HaveFire(cell_0) = true;
                    }
                }
            }
        }

        void TryPutOutFireWithClouds()
        {
            foreach (var cellE in _e.AroundCellsE(_e.WeatherE.CloudC.Center).CellsAround)
            {
                _e.HaveFire(cellE) = false;
            }
        }

        void BurnAdultForest()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_e.HaveFire(cell_0))
                {
                    _s.TryTakeAdultForestResourcesM(EnvironmentValues.FIRE_ADULT_FOREST, cell_0);
                }
            }
        }
    }
}