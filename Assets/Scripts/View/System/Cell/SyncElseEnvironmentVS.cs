using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Entity;

namespace Chessy.View.System
{
    sealed class SyncElseEnvironmentVS : SystemViewAbstract
    {
        readonly EnvironmentVE[] _environmentVEs;

        internal SyncElseEnvironmentVS(in EnvironmentVE[] environmentVEs, in EntitiesModel eM) : base(eM)
        {
            _environmentVEs = environmentVEs;
        }

        internal sealed override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    _environmentVEs[cellIdxCurrent].HillUnderC.GO.SetActive(_e.HillC(cellIdxCurrent).HaveAnyResources);

                    _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Hill).GO.SetActive(false);
                }
                else
                {
                    _environmentVEs[cellIdxCurrent].HillUnderC.GO.SetActive(false);
                    _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Hill).GO.SetActive(_e.HillC(cellIdxCurrent).HaveAnyResources);
                }

                _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Fertilizer).GO.SetActive(_e.WaterOnCellC(cellIdxCurrent).HaveAnyResources);
                _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.YoungForest).GO.SetActive(_e.YoungForestC(cellIdxCurrent).HaveAnyResources);
                _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Mountain).GO.SetActive(_e.MountainC(cellIdxCurrent).HaveAnyResources);
            }
        }
    }
}