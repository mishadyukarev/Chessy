using Leopotam.Ecs;
using UnityEngine;

public class SpawnAllForMasterEntity
{
    public void SetEnvironment(EntitiesGeneralManager entitiesGeneralManager, NameValueManager nameValueManager)
    {
        for (int x = 0; x < nameValueManager.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < nameValueManager.CELL_COUNT_Y; y++)
            {
                int random;
                random = Random.Range(1, 100);
                if (random <= nameValueManager.PERCENT_TREE) entitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Tree);

                random = Random.Range(1, 100);
                if (random <= nameValueManager.PERCENT_HILL) entitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Hill);

                random = Random.Range(1, 100);
                if (random <= nameValueManager.PERCENT_MOUNTAIN) entitiesGeneralManager.GetCellComponents<CellComponent.EnvironmentComponent>(x, y).Unref().SetResetEnvironment(true, EnvironmentTypes.Mountain);
            }
        }
    }
}
