using Assets.Scripts.ECS.Game.General.Entities;
using Assets.Scripts.ECS.Game.General.Entities.Containers;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Entities.Game.General.Cell
{
    public sealed class EntGameGeneralCellDataManager
    {
        private CellUnitsDataContainerEnts _cellUnitsDataContainerEnts;
        private CellFireDataContainerEnts _cellFireDataContainerEnts;
        private CellBuildDataContainerEnts _cellBuildDataContainerEnts;
        private CellEnvirDataContainerEnts _cellEnvirDataContainerEnts;


        internal EntGameGeneralCellDataManager(EcsWorld gameWorld)
        {
            _cellFireDataContainerEnts = new CellFireDataContainerEnts(gameWorld);
            new CellFireDataWorker(_cellFireDataContainerEnts);

            _cellUnitsDataContainerEnts = new CellUnitsDataContainerEnts(gameWorld);
            new CellUnitsDataWorker(_cellUnitsDataContainerEnts);

            _cellBuildDataContainerEnts = new CellBuildDataContainerEnts(gameWorld);
            new CellBuildingsDataWorker(_cellBuildDataContainerEnts);

            _cellEnvirDataContainerEnts = new CellEnvirDataContainerEnts(gameWorld);
            new CellEnvirDataWorker(_cellEnvirDataContainerEnts);
        }
    }
}
