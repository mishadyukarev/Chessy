using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.Else.Game.General.Cell;
using Assets.Scripts.ECS.Systems.Else.Game.General.Event;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.CenterZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.DownZone;
using Assets.Scripts.ECS.Systems.UI.Game.General.Sync.UpZone;
using Leopotam.Ecs;

public sealed class GameGeneralSystemManager : SystemAbstManager
{
    private PhotonSceneGameGeneralSystem _photonSceneGameGeneralSystem;
    private RPCGameSystem _rPCGameSystem;

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
           ///left
           .Add(new LeftBuildingUISystem())
           .Add(new EnvironmentUISystem())

            ///right
            .Add(new RightZoneUISystem())
            .Add(new StatsUISystem())
            .Add(new ConditionAbilitiesUISystem())
            .Add(new UniqueAbilitiesUISystem())
            .Add(new BuildRighUISystem())
            
            ///down
            .Add(new DonerUISystem())
            .Add(new GetterUnitsUISystem())
            .Add(new GiveThingUISystem())

            ///up
            .Add(new SyncEconomyUpUISystem())
            .Add(new SyncToolsUpUISystem())

            ///center
            .Add(new SelectorTypeUISystem()) 
            .Add(new TheEndGameUISystem())
            .Add(new MotionCenterUISystem())  
            .Add(new ReadyZoneUISystem())
            .Add(new CenterSupTextUISystem())
            .Add(new KingZoneUISys());


        InitOnlySystems
            .Add(spawnerAndCreatorEntSystems)
            .Add(new StaticEventsGameSys())
            .Add(_photonSceneGameGeneralSystem)
            .Add(_rPCGameSystem);


        RunOnlySystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(syncCellVisionSystems)
            .Add(syncCanvasSystems)

            .Add(new SoundSystem())
            .Add(new FillAvailCellsSystem());


        InitRunSystems
            .Add(new DinamicEventsGameSys());



        allGameSystems
            .Add(InitOnlySystems)
            .Add(RunOnlySystems)
            .Add(InitRunSystems);
    }
}
