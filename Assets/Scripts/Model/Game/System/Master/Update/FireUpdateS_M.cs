using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System.Collections.Generic;

namespace Chessy.Game.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModel
    {
        void FireUpdate()
        {
            foreach (var cellE in _eMG.AroundCellsE(_eMG.WeatherE.CloudC.Center).CellsAround)
            {
                _eMG.HaveFire(cellE) = false;
            }


            var needForFireNext = new List<byte>();

            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMG.HaveFire(cell_0))
                {
                    _sMG.TryTakeAdultForestResourcesM(EnvironmentValues.FIRE_ADULT_FOREST, cell_0);

                    if (_eMG.UnitTC(cell_0).HaveUnit)
                    {
                        if (_eMG.UnitTC(cell_0).Is(UnitTypes.Hell))
                        {
                            _eMG.HpUnitC(cell_0).Health = HpValues.MAX;
                        }
                        else
                        {
                            if (_eMG.UnitPlayerTC(cell_0).Is(PlayerTypes.None))
                            {
                                _sMG.UnitSs.Attack(HpValues.FIRE_DAMAGE, PlayerTypes.None, cell_0);
                            }
                            else
                            {
                                _sMG.UnitSs.Attack(HpValues.FIRE_DAMAGE, _eMG.UnitPlayerTC(cell_0).PlayerT.NextPlayer(), cell_0);
                            }
                        }
                    }

                    if (!_eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;


                        _eMG.HaveFire(cell_0) = false;


                        foreach (var cellE in _eMG.AroundCellsE(cell_0).CellsAround)
                        {
                            needForFireNext.Add(cellE);
                        }
                    }
                }
            }

            foreach (var cell_0 in needForFireNext)
            {
                if (!_eMG.IsBorder(cell_0))
                {
                    if (_eMG.AdultForestC(cell_0).HaveAnyResources)
                    {
                        _eMG.HaveFire(cell_0) = true;
                    }
                }
            }
        }
    }
}