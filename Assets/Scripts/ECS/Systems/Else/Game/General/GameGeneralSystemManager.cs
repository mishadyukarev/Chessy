using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.Else.Game.General.Cell;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone;
using Leopotam.Ecs;

public sealed class GameGeneralSystemManager : SystemAbstManager
{
    private PhotonSceneGameGeneralSystem _photonSceneGameGeneralSystem;
    private RPCGameSystem _rPCGameSystem;

    internal static EcsSystems GetUnitWaySystems { get; private set; }

    internal GameGeneralSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld)
    {
        var spawnerAndCreatorEntSystems = new EcsSystems(gameWorld)
            .Add(new InitGameGeneralSystem());


        _photonSceneGameGeneralSystem = Main.Instance.gameObject.AddComponent<PhotonSceneGameGeneralSystem>();
        _rPCGameSystem = Main.Instance.gameObject.AddComponent<RPCGameSystem>();


        var syncCellVisionSystems = new EcsSystems(gameWorld)
            .Add(new SyncCellUnitViewSystem())
            .Add(new SyncCellUnitSupVisSystem())
            .Add(new SyncCellBuildViewSystem())
            .Add(new SyncCellEnvirsVisSystem())
            .Add(new SyncCellEffectsVisSystem())
            .Add(new SyncSupportViewSystem())
            .Add(new FliperAndRotatorUnitSystem());


        var syncCanvasSystems = new EcsSystems(gameWorld)
           //left
           .Add(new LeftBuildingUISystem())
           .Add(new EnvironmentUISystem())

            //right
            .Add(new RightZoneUISystem())
            .Add(new StatsUISystem())
            .Add(new ConditionAbilitiesUISystem())
            .Add(new UniqueAbilitiesUISystem())
            .Add(new BuildRighUISystem())
            
            //down
            .Add(new DonerUISystem())
            .Add(new GetterUnitsUISystem())

            //up
            .Add(new SyncEconomyUpUISystem())
            .Add(new SyncToolsUpUISystem())

            //center
            .Add(new SelectorTypeUISystem()) 
            .Add(new TheEndGameUISystem())
            .Add(new MotionCenterUISystem())  
            .Add(new ReadyZoneUISystem())
            .Add(new CenterSupTextUISystem());


        GetUnitWaySystems = new EcsSystems(gameWorld)
            .Add(new FillAvailCellsSystem());



        EventSystems
            .Add(new StaticEventsGameSys())
            .Add(_photonSceneGameGeneralSystem);


        InitSystems
            .Add(spawnerAndCreatorEntSystems)
            .Add(EventSystems)
            .Add(_rPCGameSystem);


        RunSystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(syncCellVisionSystems)
            .Add(syncCanvasSystems)

            .Add(new SoundSystem());



        allGameSystems
            .Add(InitSystems)
            .Add(RunSystems)
            .Add(GetUnitWaySystems);

    }
}
