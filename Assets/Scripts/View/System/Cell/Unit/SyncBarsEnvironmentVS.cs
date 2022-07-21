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

        readonly SpriteRenderer[] _foorSRs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObject[] _foorGOs = new GameObject[IndexCellsValues.CELLS];
        readonly bool[] _needActiveFood = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedFood = new bool[IndexCellsValues.CELLS];

        readonly SpriteRenderer[] _woodSRs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObject[] _woodGOs = new GameObject[IndexCellsValues.CELLS];
        readonly bool[] _needActiveWood = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedWood = new bool[IndexCellsValues.CELLS];

        readonly SpriteRenderer[] _oreSRs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObject[] _oreGOs = new GameObject[IndexCellsValues.CELLS];
        readonly bool[] _needActiveOre = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedOre = new bool[IndexCellsValues.CELLS];

        internal SyncBarsEnvironmentVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;

            for (byte cellIdx_0 = 0; cellIdx_0 < IndexCellsValues.CELLS; cellIdx_0++)
            {
                var sR = _eV.CellEs(cellIdx_0).Bar(CellBarTypes.Food).SR;
                _foorSRs[cellIdx_0] = sR;
                _foorGOs[cellIdx_0] = sR.gameObject;

                sR = _eV.CellEs(cellIdx_0).Bar(CellBarTypes.Wood).SR;
                _woodSRs[cellIdx_0] = sR;
                _woodGOs[cellIdx_0] = sR.gameObject;

                sR = _eV.CellEs(cellIdx_0).Bar(CellBarTypes.Ore).SR;
                _oreSRs[cellIdx_0] = sR;
                _oreGOs[cellIdx_0] = sR.gameObject;
            }

        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActiveFood[cellIdxCurrent] = false;
                _needActiveWood[cellIdxCurrent] = false;
                _needActiveOre[cellIdxCurrent] = false;
            }

            if (_zonesInfoC.IsActiveEnvironment)
            {
                for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
                {

                    if (_e.WaterOnCellC(cellIdxCurrent).HaveAnyResources)
                    {
                        _needActiveFood[cellIdxCurrent] = true;

                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Food).Transform.localScale
                            = new Vector3(_e.WaterOnCellC(cellIdxCurrent).ResourcesP / (float)ValuesChessy.MAX_RESOURCES_ENVIRONMENT, 0.15f, 1);
                    }

                    if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                    {
                        _needActiveWood[cellIdxCurrent] = true;

                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Wood).Transform.localScale =
                            new Vector3(_e.AdultForestC(cellIdxCurrent).ResourcesP
                            / (float)ValuesChessy.MAX_RESOURCES_ENVIRONMENT, 0.15f, 1);
                    }

                    if (_e.HillC(cellIdxCurrent).HaveAnyResources)
                    {
                        _needActiveOre[cellIdxCurrent] = true;

                        _eV.CellEs(cellIdxCurrent).Bar(CellBarTypes.Ore).Transform.localScale
                            = new Vector3(_e.HillC(cellIdxCurrent).ResourcesP
                            / (float)ValuesChessy.MAX_RESOURCES_ENVIRONMENT, 0.15f, 1);
                    }
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var needActive = _needActiveFood[cellIdxCurrent];
                ref var wasActivatedFood = ref _wasActivatedFood[cellIdxCurrent];

                if (needActive != wasActivatedFood) _foorGOs[cellIdxCurrent].SetActive(needActive);

                wasActivatedFood = needActive;


                needActive = _needActiveWood[cellIdxCurrent];
                ref var wasActivatedWood = ref _wasActivatedWood[cellIdxCurrent];

                if (needActive != wasActivatedWood) _woodGOs[cellIdxCurrent].SetActive(needActive);

                wasActivatedWood = needActive;


                needActive = _needActiveOre[cellIdxCurrent];
                ref var wasActivatedOre = ref _wasActivatedOre[cellIdxCurrent];

                if (needActive != wasActivatedOre) _oreGOs[cellIdxCurrent].SetActive(needActive);

                wasActivatedOre = needActive;
            }
        }
    }
}