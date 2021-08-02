using Leopotam.Ecs;

namespace Assets.Scripts.ECS.System.Data.Game.General.Cell
{
    public sealed class SysDataGameGeneralCellManager : SystemAbstManager
    {
        internal EcsSystems CellSystems { get; private set; }

        internal SysDataGameGeneralCellManager(EcsWorld gameWorld) : base(gameWorld)
        {
            CellSystems = new EcsSystems(gameWorld)
                .Add(new CellEnvrDataSystem())
                .Add(new CellFireDataSystem())
                .Add(new CellUnitsDataSystem())
                .Add(new CellBuildDataSystem());
        }

        internal override void ProcessInjects()
        {
            base.ProcessInjects();

            CellSystems.ProcessInjects();
        }

        internal override void Init()
        {
            base.Init();

            CellSystems.Init();
        }

        internal override void RunUpdate()
        {
            base.RunUpdate();

            CellSystems.Run();
        }
    }
}
