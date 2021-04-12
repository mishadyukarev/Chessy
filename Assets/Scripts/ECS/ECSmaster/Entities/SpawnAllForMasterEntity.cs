using Leopotam.Ecs;
using UnityEngine;

public class SpawnAllForMasterEntity
{
    public void SetEnvironment(EntitiesGeneralManager entitiesGeneralManager, StartValuesConfig startValues)
    {
        for (int x = 0; x < startValues.CellCountX; x++)
        {
            for (int y = 0; y < startValues.CellCountY; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= startValues.PercentTree) entitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= startValues.PercentHill) entitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= startValues.PercentMountain) entitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Mountain);
            }
        }
    }
}
