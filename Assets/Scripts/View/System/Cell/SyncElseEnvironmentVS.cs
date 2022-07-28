using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.Entity;

namespace Chessy.View.System
{
    sealed class SyncElseEnvironmentVS : SystemViewAbstract
    {
        readonly bool[,] _needActive = new bool[IndexCellsValues.CELLS, (byte)EnvironmentTypes.End];
        readonly bool[,] _wasActivated = new bool[IndexCellsValues.CELLS, (byte)EnvironmentTypes.End];
        readonly GameObjectVC[,] _gOCs = new GameObjectVC[IndexCellsValues.CELLS, (byte)EnvironmentTypes.End];

        readonly bool[] _needActiveHillOver = new bool[IndexCellsValues.CELLS];
        readonly bool[] _wasActivatedHillOver = new bool[IndexCellsValues.CELLS];
        readonly GameObjectVC[] _hillOverGOs = new GameObjectVC[IndexCellsValues.CELLS];

        internal SyncElseEnvironmentVS(in EnvironmentVE[] environmentVEs, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (var environmentT = (EnvironmentTypes)1; environmentT < EnvironmentTypes.End; environmentT++)
                {
                    _gOCs[cellIdxCurrent, (byte)environmentT] = new GameObjectVC(environmentVEs[cellIdxCurrent].EnvironmentE(environmentT).GO);
                }

                _hillOverGOs[cellIdxCurrent] = new GameObjectVC(environmentVEs[cellIdxCurrent].HillOverC.GO);
            }
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.AdultForest))
                {
                    if (_environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill))
                    {
                        _needActiveHillOver[cellIdxCurrent] = true;
                    }

                    _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Hill] = false;
                }
                else
                {
                    _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Hill] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Hill);
                    _needActiveHillOver[cellIdxCurrent] = false;
                }

                _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Fertilizer] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Fertilizer);
                _needActive[cellIdxCurrent, (byte)EnvironmentTypes.YoungForest] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.YoungForest);
                _needActive[cellIdxCurrent, (byte)EnvironmentTypes.Mountain] = _environmentCs[cellIdxCurrent].HaveEnvironment(EnvironmentTypes.Mountain);
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                for (byte envTByte = 1; envTByte < (byte)EnvironmentTypes.End; envTByte++)
                {
                    _gOCs[cellIdxCurrent, envTByte].TrySetActive2(_needActive[cellIdxCurrent, envTByte], ref _wasActivated[cellIdxCurrent, envTByte]);
                }

                _hillOverGOs[cellIdxCurrent].TrySetActive2(_needActiveHillOver[cellIdxCurrent], ref _wasActivatedHillOver[cellIdxCurrent]);
            }
        }
    }
}