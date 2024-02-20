using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncBarsEnvironmentVS : SystemViewAbstract
    {
        readonly EntitiesView _eV;

        readonly SpriteRenderer[] _foorSRs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObjectVC[] _foorGOs = new GameObjectVC[IndexCellsValues.CELLS];
        readonly bool[] _needActiveFood = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedFood = new bool[IndexCellsValues.CELLS];

        readonly SpriteRenderer[] _woodSRs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObjectVC[] _woodGOs = new GameObjectVC[IndexCellsValues.CELLS];
        readonly bool[] _needActiveWood = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedWood = new bool[IndexCellsValues.CELLS];

        readonly SpriteRenderer[] _oreSRs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObjectVC[] _oreGOs = new GameObjectVC[IndexCellsValues.CELLS];
        readonly bool[] _needActiveOre = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedOre = new bool[IndexCellsValues.CELLS];

        internal SyncBarsEnvironmentVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;

            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                var sR = _eV.CellEs(cellIdx_0).Bar(CellBarTypes.Food).SR;
                _foorSRs[cellIdx_0] = sR;
                _foorGOs[cellIdx_0] = new GameObjectVC(sR.gameObject);

                sR = _eV.CellEs(cellIdx_0).Bar(CellBarTypes.Wood).SR;
                _woodSRs[cellIdx_0] = sR;
                _woodGOs[cellIdx_0] = new GameObjectVC(sR.gameObject);

                sR = _eV.CellEs(cellIdx_0).Bar(CellBarTypes.Ore).SR;
                _oreSRs[cellIdx_0] = sR;
                _oreGOs[cellIdx_0] = new GameObjectVC(sR.gameObject);
            }

        }

        internal sealed override void Sync()
        {
            if (zonesInfoC.IsActiveEnvironment)
            {
                for (byte currentCellIdx_0 = 0; currentCellIdx_0 < IndexCellsValues.CELLS; currentCellIdx_0++)
                {
                    if (cellCs[currentCellIdx_0].IsBorder) continue;


                    var curEnvirC_0 = environmentCs[currentCellIdx_0];

                    if (curEnvirC_0.HaveEnvironment(EnvironmentTypes.Fertilizer))
                    {
                        _needActiveFood[currentCellIdx_0] = true;

                        var scale = new Vector3((float)curEnvirC_0.Resources(EnvironmentTypes.Fertilizer) / (float)ValuesChessy.MAX_RESOURCES_ENVIRONMENT, 0.15f, 1);

                        _eV.CellEs(currentCellIdx_0).Bar(CellBarTypes.Food).Transform.localScale = scale;
                    }
                    else
                    {
                        _needActiveFood[currentCellIdx_0] = false;
                    }

                    if (curEnvirC_0.HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        _needActiveWood[currentCellIdx_0] = true;

                        _eV.CellEs(currentCellIdx_0).Bar(CellBarTypes.Wood).Transform.localScale =
                            new Vector3((float)curEnvirC_0.Resources(EnvironmentTypes.AdultForest)
                            / (float)ValuesChessy.MAX_RESOURCES_ENVIRONMENT, 0.15f, 1);
                    }
                    else
                    {
                        _needActiveWood[currentCellIdx_0] = false;
                    }

                    if (curEnvirC_0.HaveEnvironment(EnvironmentTypes.Hill))
                    {
                        _needActiveOre[currentCellIdx_0] = true;

                        _eV.CellEs(currentCellIdx_0).Bar(CellBarTypes.Ore).Transform.localScale
                            = new Vector3((float)curEnvirC_0.Resources(EnvironmentTypes.Hill)
                            / (float)ValuesChessy.MAX_RESOURCES_ENVIRONMENT, 0.15f, 1);
                    }
                    else
                    {
                        _needActiveOre[currentCellIdx_0] = false;
                    }
                }
            }

            else
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {
                    if (cellCs[cellIdxCurrent].IsBorder) continue;

                    _needActiveFood[cellIdxCurrent] = false;
                    _needActiveWood[cellIdxCurrent] = false;
                    _needActiveOre[cellIdxCurrent] = false;
                }
            }


            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (cellCs[cellIdxCurrent].IsBorder) continue;

                _foorGOs[cellIdxCurrent].TrySetActive2(_needActiveFood[cellIdxCurrent], ref _wasActivatedFood[cellIdxCurrent]);
                _woodGOs[cellIdxCurrent].TrySetActive2(_needActiveWood[cellIdxCurrent], ref _wasActivatedWood[cellIdxCurrent]);
                _oreGOs[cellIdxCurrent].TrySetActive2(_needActiveOre[cellIdxCurrent], ref _wasActivatedOre[cellIdxCurrent]);
            }
        }
    }
}