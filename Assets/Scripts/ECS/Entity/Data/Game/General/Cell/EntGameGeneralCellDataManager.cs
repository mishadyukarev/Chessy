using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entities.Game.General.Cell
{
    public sealed class EntGameGeneralCellDataManager
    {
        internal EntGameGeneralCellDataManager(EcsWorld gameWorld)
        {
            new CellFireDataContainer(gameWorld);
            new CellUnitsDataContainer(gameWorld);
            new CellBuildDataContainer(gameWorld);
            new CellEnvirDataContainer(gameWorld);
        }
    }
}
