using Assets.Scripts;
using Assets.Scripts.ECS.Game.General.Systems;
using Assets.Scripts.ECS.Game.General.Systems.RunUpdate.Sound;
using Assets.Scripts.ECS.Game.General.Systems.RunUpdate.UI.DownZone;
using Leopotam.Ecs;

public sealed class SystemsGameGeneralManager : SystemsManager
{
    internal EcsSystems ForSelectorRunUpdateSystem;
    internal EcsSystems EventSystems;

    internal void CreateSystems(EcsWorld ecsWorld)
    {
        RunUpdateSystems = new EcsSystems(ecsWorld)
            .Add(new InputSystem(), nameof(InputSystem))
            .Add(new SelectorSystem(), nameof(SelectorSystem))
            .Add(new SupportVisionSystem(), nameof(SupportVisionSystem))
            .Add(new SoundEventsSystem(), nameof(SoundEventsSystem))
            .Add(new FliperAndRotatorUnitSystem(), nameof(FliperAndRotatorUnitSystem))
            .Add(new PickSoundSystem(), nameof(PickSoundSystem))

            .Add(new DonerUISystem(), nameof(DonerUISystem))
            .Add(new TakerUnitsUISystem(), nameof(TakerUnitsUISystem))
            .Add(new StandartAbilityUISystem(), nameof(StandartAbilityUISystem))
            .Add(new StatsUISystem(), nameof(StatsUISystem))
            .Add(new TheEndGameUISystem(), nameof(TheEndGameUISystem))
            .Add(new BuildingUISystem(), nameof(BuildingUISystem))
            .Add(new EconomyUISystem(), nameof(EconomyUISystem))
            .Add(new MistakeUISystem(), nameof(MistakeUISystem))
            .Add(new LeftBuildingUISystem(), nameof(LeftBuildingUISystem))
            .Add(new UpdatedUISystem(), nameof(UpdatedUISystem))
            .Add(new UniqueAbilitiesUISystem(), nameof(UniqueAbilitiesUISystem))
            .Add(new TruceUISystem(), nameof(TruceUISystem))
            .Add(new EnvironmentUISystem(), nameof(EnvironmentUISystem))
            .Add(new ReadyZoneUISystem(), nameof(ReadyZoneUISystem))
            .Add(new RightZoneUISystem(), nameof(RightZoneUISystem))
            .Add(new FinderIdleUnitUISystem(), nameof(FinderIdleUnitUISystem));

        ForSelectorRunUpdateSystem = new EcsSystems(ecsWorld)
            .Add(new GetterCellSystem(), nameof(GetterCellSystem))
            .Add(new RaySystem(), nameof(RaySystem));

        EventSystems = new EcsSystems(ecsWorld)
            .Add(new EventGeneralSystem(), nameof(EventGeneralSystem));
    }

    internal override void ProcessInjects()
    {
        base.ProcessInjects();

        ForSelectorRunUpdateSystem.ProcessInjects();
        EventSystems.ProcessInjects();
    }

    internal override void Init()
    {
        base.Init();

        ForSelectorRunUpdateSystem.Init();
        EventSystems.Init();
    }
}
