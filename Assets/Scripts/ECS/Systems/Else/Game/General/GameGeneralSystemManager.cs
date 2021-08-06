using Assets.Scripts;
using Assets.Scripts.ECS.Entities.Game.General.Else.View.Containers;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers;
using Leopotam.Ecs;

public sealed class GameGeneralSystemManager : SystemAbstManager
{
    private EcsSystems _rpcSystems;
    internal static EcsSystems SyncCellVisionSystems { get; private set; }

    internal GameGeneralSystemManager(EcsWorld gameWorld, EcsWorld commonWorld) : base(gameWorld)
    {
        InitSystems
            .Add(new MainGameSystem(commonWorld))
            .Add(new PhotonSceneGameGeneralSystem());


        UpdateSystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(new SupportVisionSystem())
            .Add(new FliperAndRotatorUnitSystem())

            .Add(new SoundEventsSystem())

            .Add(new CellEnvrDataSystem())
            .Add(new CellFireDataSystem())
            .Add(new CellUnitsDataSystem())
            .Add(new CellBuildDataSystem())

            .Add(new StartSpawnCellsViewSystem())
            .Add(new CellBlocksViewSystem())
            .Add(new CellUnitViewSystem())
            .Add(new CellSupVisBarsViewSystem())
            .Add(new CellSupViewSystem())
            .Add(new CellFireViewSystem())
            .Add(new CellBuildViewSystem())
            .Add(new CellEnvViewSystem())
            .Add(new CellViewSystem());


        _rpcSystems = new EcsSystems(gameWorld)
            .Add(Main.Instance.gameObject.AddComponent<RPCGameSystem>());


        SyncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitVisSystem())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildingsVisSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem());


        new SoundGameGeneralViewWorker(new SoundElseViewContainer(gameWorld));

    }

    internal override void Init()
    {
        base.Init();

        _rpcSystems.Init();
        SyncCellVisionSystems.Init();
    }
}
