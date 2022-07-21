using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncElseEnvironmentVS : SystemViewAbstract
    {
        readonly bool[,] _needActive = new bool[IndexCellsValues.CELLS, (byte)EnvironmentTypes.End];
        readonly bool[,] _wasActivated = new bool[IndexCellsValues.CELLS, (byte)EnvironmentTypes.End];
        readonly GameObject[,] _gOs = new GameObject[IndexCellsValues.CELLS, (byte)EnvironmentTypes.End];

        readonly bool[] _needActiveHillOver = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedHillOver = new bool[IndexCellsValues.CELLS];
        readonly GameObject[] _hillOverGOs = new GameObject[IndexCellsValues.CELLS];

        internal SyncElseEnvironmentVS(in EnvironmentVE[] environmentVEs, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                {
                    _gOs[cellIdxCurrent, (byte)environmentT] = environmentVEs[cellIdxCurrent].EnvironmentE(environmentT).GO;
                }

                _hillOverGOs[cellIdxCurrent] = environmentVEs[cellIdxCurrent].HillOverC.GO;
            }
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                {
                    _needActive[cellIdxCurrent, (byte)environmentT] = false;
                }

                _needActiveHillOver[cellIdxCurrent] = false;
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    _needActiveHillOver[cellIdxCurrent] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill);
                }
                else
                {
                    _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Hill] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill);
                }

                _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Fertilizer] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Fertilizer);
                _needActive[cellIdxCurrent, (byte)EnvironmentTypes.YoungForest] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.YoungForest);
                _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Mountain] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Mountain);
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                {
                    var envTByte = (byte)environmentT;

                    var needActive = _needActive[cellIdxCurrent, envTByte];
                    ref var wasActivated = ref _wasActivated[cellIdxCurrent, envTByte];

                    if (wasActivated != needActive) _gOs[cellIdxCurrent, envTByte].SetActive(needActive);
                }

                var needActiveOver = _needActiveHillOver[cellIdxCurrent];
                ref var wasActivatedOver = ref _wasActivatedHillOver[cellIdxCurrent];

                if (needActiveOver != wasActivatedOver) _hillOverGOs[cellIdxCurrent].SetActive(needActiveOver);
            }
        }
    }
}