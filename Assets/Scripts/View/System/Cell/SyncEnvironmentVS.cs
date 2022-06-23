using Chessy.Model.Entity.View.Cell;
using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    sealed class SyncEnvironmentVS : SystemViewCellGameAbs
    {
        readonly EnvironmentVEs _environmentVEs;

        internal SyncEnvironmentVS(in EnvironmentVEs environmentVEs, in byte currentCell, in EntitiesModel eMG) : base(currentCell, eMG)
        {
            _environmentVEs = environmentVEs;
        }

        internal sealed override void Sync()
        {
            if (_e.AdultForestC(_currentCell).HaveAnyResources)
            {
                _environmentVEs.EnvironmentE(EnvironmentTypes.AdultForest).GO.SetActive(true);

                _environmentVEs.HillUnderC.GO.SetActive(_e.HillC(_currentCell).HaveAnyResources);

                _environmentVEs.EnvironmentE(EnvironmentTypes.Hill).GO.SetActive(false);
            }
            else
            {
                _environmentVEs.EnvironmentE(EnvironmentTypes.AdultForest).GO.SetActive(false);
                _environmentVEs.HillUnderC.GO.SetActive(false);
                _environmentVEs.EnvironmentE(EnvironmentTypes.Hill).GO.SetActive(_e.HillC(_currentCell).HaveAnyResources);
            }

            _environmentVEs.EnvironmentE(EnvironmentTypes.Fertilizer).GO.SetActive(_e.FertilizeC(_currentCell).HaveAnyResources);
            _environmentVEs.EnvironmentE(EnvironmentTypes.YoungForest).GO.SetActive(_e.YoungForestC(_currentCell).HaveAnyResources);
            _environmentVEs.EnvironmentE(EnvironmentTypes.Mountain).GO.SetActive(_e.MountainC(_currentCell).HaveAnyResources);
        }
    }
}