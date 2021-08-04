using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.Game.General.Systems.SupportVision;
using Assets.Scripts.ECS.Game.General.Systems.SyncCellVision;
using Assets.Scripts.ECS.Systems.Game.General.UI.View;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.General.UI.RunUpdate.CenterZone;
using Leopotam.Ecs;

public sealed class ElseGameGeneralDataSysManager : SystemAbstManager
{
    private EcsSystems _eventSystems;

    internal static EcsSystems SyncCellVisionSystems { get; private set; }

    internal ElseGameGeneralDataSysManager(EcsWorld gameWorld) : base(gameWorld)
    {
        InitSystems
            .Add(new InitSystem());

        UpdateSystems
            .Add(new InputSystem())
            .Add(new RaySystem())
            .Add(new SelectorSystem())

            .Add(new SupportVisionSystem(), nameof(SupportVisionSystem))
            .Add(new FliperAndRotatorUnitSystem(), nameof(FliperAndRotatorUnitSystem))

            .Add(new SoundEventsSystem(), nameof(SoundEventsSystem))

            .Add(new DonerUISystem(), nameof(DonerUISystem))
            .Add(new GetterUnitsUISystem(), nameof(GetterUnitsUISystem))
            .Add(new ConditionAbilitiesUISystem(), nameof(ConditionAbilitiesUISystem))
            .Add(new StatsUISystem(), nameof(StatsUISystem))
            .Add(new TheEndGameUISystem(), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(), nameof(EconomyUISystem))
            .Add(new LeftBuildingUISystem(), nameof(LeftBuildingUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem))
            .Add(new ReadyZoneUISystem(), nameof(ReadyZoneUISystem))
            .Add(new RightZoneUISystem(), nameof(RightZoneUISystem))
            .Add(new MistakeBarUISystem())
            .Add(new MistakeUISystem())
            .Add(new CenterSupTextUISystem());

        _eventSystems = new EcsSystems(gameWorld)
            .Add(new EventGeneralSystem(), nameof(EventGeneralSystem));

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

        _eventSystems.Init();
        SyncCellVisionSystems.Init();
    }
}
