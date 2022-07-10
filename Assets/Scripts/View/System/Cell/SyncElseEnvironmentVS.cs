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
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_e.AdultForestC(cellIdxCurrent).HaveAnyResources)
                {
                    _environmentVEs[cellIdxCurrent].HillUnderC.SetActiveGO(_e.HillC(cellIdxCurrent).HaveAnyResources);

                    _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Hill).SetActiveGO(false);
                }
                else
                {
                    _environmentVEs[cellIdxCurrent].HillUnderC.SetActiveGO(false);
                    _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Hill).SetActiveGO(_e.HillC(cellIdxCurrent).HaveAnyResources);
                }

                _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Fertilizer).SetActiveGO(_e.WaterOnCellC(cellIdxCurrent).HaveAnyResources);
                _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.YoungForest).SetActiveGO(_e.YoungForestC(cellIdxCurrent).HaveAnyResources);
                _environmentVEs[cellIdxCurrent].EnvironmentE(EnvironmentTypes.Mountain).SetActiveGO(_e.MountainC(cellIdxCurrent).HaveAnyResources);
            }
        }
    }
}