using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncAdultForestOnCellsVS : SystemViewAbstract
    {
        readonly bool[] _wasActivated = new bool[IndexCellsValues.CELLS];
        readonly bool[] _needActive = new bool[IndexCellsValues.CELLS];
        readonly SpriteRenderer[] _srs = new SpriteRenderer[IndexCellsValues.CELLS];
        readonly GameObject[] _gos = new GameObject[IndexCellsValues.CELLS];

        internal SyncAdultForestOnCellsVS(in SpriteRendererVC[] adultForestVCs, in EntitiesModel eM) : base(eM)
        {
            for (var i = 0; i < adultForestVCs.Length; i++)
            {
                var sr = adultForestVCs[i].SR;

                _srs[i] = adultForestVCs[i].SR;
                _gos[i] = sr.gameObject;
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                _needActive[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _needActive[cellIdxCurrent] = true;
                }
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var needActive = _needActive[cellIdxCurrent];
                ref var wasActivated = ref _wasActivated[cellIdxCurrent];

                if (needActive != wasActivated) _gos[cellIdxCurrent].SetActive(needActive);

                wasActivated = needActive;
            }
        }
    }
}