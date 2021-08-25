using Assets.Scripts;
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
using System;
using UnityEngine;

public sealed class GameGeneralSystemManager : SystemAbstManager, IDisposable
{
    private PhotonSceneGameGeneralSystem _photonSceneGameSys;
    private RpcGeneralSystem _rpcGameSystem;

    internal GameGeneralSystemManager(EcsWorld gameWorld, EcsSystems allGameSystems) : base(gameWorld)
    {
        var spawnerAndCreatorEntSystems = new EcsSystems(gameWorld)
            .Add(new InitGameGeneralSystem());


        _photonSceneGameSys = Main.Instance.gameObject.AddComponent<PhotonSceneGameGeneralSystem>();
        _rpcGameSystem = Main.Instance.gameObject.AddComponent<RpcGeneralSystem>();


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
            .Add(new GiveTakeUISystem())
            .Add(new SyncToolsDownUISystem())

            ///up
            .Add(new SyncEconomyUpUISystem())

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
            .Add(_photonSceneGameSys)
            .Add(_rpcGameSystem);


        RunOnlySystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(new FillAvailCellsSystem())

            .Add(syncCellVisionSystems)
            .Add(syncCanvasSystems)

            .Add(new SoundSystem())

            .Add(new VisibilityUnitsMasterSystem());


        InitRunSystems
            .Add(new DinamicEventsGameSys());



        allGameSystems
            .Add(InitOnlySystems)
            .Add(RunOnlySystems)
            .Add(InitRunSystems);
    }

    public void Dispose()
    {
        GameObject.Destroy(_photonSceneGameSys);
        GameObject.Destroy(_rpcGameSystem);
    }
}
