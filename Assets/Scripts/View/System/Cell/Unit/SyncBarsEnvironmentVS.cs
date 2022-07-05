using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncBarsEnvironmentVS : SystemViewAbstract
    {
        readonly EntitiesView _eV;

        internal SyncBarsEnvironmentVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.ZoneInfoC.IsActiveEnvironment)
                {
                    if (_e.WaterOnCellC(cellIdxCurrent).HaveAnyResources)
                    {
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Food).Enable();

                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Food).Transform.localScale
                            = new Vector3(_e.WaterOnCellC(cellIdxCurrent).Resources / (float)ValuesChessy.MAX_RESOURCES, 0.15f, 1);
                    }
                    else
                    {
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Food).Disable();
                    }

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Wood).Enable();
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Wood).Transform.localScale =
                            new Vector3(_e.AdultForestC(cellIdxCurrent).Resources
                            / (float)ValuesChessy.MAX_RESOURCES, 0.15f, 1);
                    }
                    else
                    {
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Wood).Disable();
                    }

                    if (_e.HillC(cellIdxCurrent).HaveAnyResources)
                    {
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Ore).Enable();
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Ore).Transform.localScale
                            = new Vector3(_e.HillC(cellIdxCurrent).Resources
                            / (float)ValuesChessy.MAX_RESOURCES, 0.15f, 1);
                    }
                    else
                    {
                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Ore).Disable();
                    }
                }
                else
                {
                    _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Food).Disable();
                    _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Wood).Disable();
                    _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Ore).Disable();
                }
            }
        }
    }
}