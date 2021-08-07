using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Leopotam.Ecs;

public sealed class GameGeneralSystemManager : SystemAbstManager
{
    private EcsSystems _rpcSystems;
    internal static EcsSystems SyncCellVisionSystems { get; private set; }

    internal GameGeneralSystemManager(EcsWorld gameWorld) : base(gameWorld)
    {
        InitSystems
            .Add(new MainGameSystem())
            .Add(new PhotonSceneGameGeneralSystem());


        UpdateSystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(new SupportVisionSystem())
            .Add(new FliperAndRotatorUnitSystem())

            .Add(new SoundEventsSystem());


        _rpcSystems = new EcsSystems(gameWorld)
            .Add(Main.Instance.gameObject.AddComponent<RPCGameSystem>());


        SyncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitVisSystem())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildingsVisSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem());
    }

    internal override void Init()
    {
        base.Init();

        _rpcSystems.Init();
        SyncCellVisionSystems.Init();
    }
}
