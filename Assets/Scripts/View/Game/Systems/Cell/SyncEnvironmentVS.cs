using Chessy.Game.Entity.View.Cell;
using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncEnvironmentVS : SystemViewCellGameAbs
    {
        readonly EnvironmentVEs _environmentVEs;

        internal SyncEnvironmentVS(in EnvironmentVEs environmentVEs, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _environmentVEs = environmentVEs;
        }

        internal sealed override void Sync()
        {
            if (e.SelectedCell == _currentCell)
            {
                _environmentVEs.AnimationC.Play();
            }

            if (e.AdultForestC(_currentCell).HaveAnyResources)
            {
                _environmentVEs.EnvironmentE(EnvironmentTypes.AdultForest).GO.SetActive(true);

                _environmentVEs.HillUnderC.GO.SetActive(e.HillC(_currentCell).HaveAnyResources);

                _environmentVEs.EnvironmentE(EnvironmentTypes.Hill).GO.SetActive(false);
            }
            else
            {
                _environmentVEs.EnvironmentE(EnvironmentTypes.AdultForest).GO.SetActive(false);
                _environmentVEs.HillUnderC.GO.SetActive(false);
                _environmentVEs.EnvironmentE(EnvironmentTypes.Hill).GO.SetActive(e.HillC(_currentCell).HaveAnyResources);
            }

            _environmentVEs.EnvironmentE(EnvironmentTypes.Fertilizer).GO.SetActive(e.FertilizeC(_currentCell).HaveAnyResources);
            _environmentVEs.EnvironmentE(EnvironmentTypes.YoungForest).GO.SetActive(e.YoungForestC(_currentCell).HaveAnyResources);
            _environmentVEs.EnvironmentE(EnvironmentTypes.Mountain).GO.SetActive(e.MountainC(_currentCell).HaveAnyResources);
        }
    }
}