using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.View.Game.General.Cell
{
    public sealed class SysViewGameGeneralCellManager : SystemAbstManager
    {
        internal SysViewGameGeneralCellManager(EcsWorld gameWorld) : base(gameWorld)
        {
            InitSystems
                .Add(new StartSpawnCellsViewSystem())
                .Add(new CellBlocksViewSystem())
                .Add(new CellUnitViewSystem())
                .Add(new CellSupVisBarsViewSystem())
                .Add(new CellSupViewSystem())
                .Add(new CellFireViewSystem())
                .Add(new CellBuildViewSystem())
                .Add(new CellEnvViewSystem())
                .Add(new CellViewSystem());
        }
    }
}
