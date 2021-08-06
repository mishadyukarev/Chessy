using Assets.Scripts.ECS.Systems.Game.General.UI.View;
using Assets.Scripts.ECS.Systems.Game.General.UI.View.Down;
using Assets.Scripts.ECS.Systems.General.UI.RunUpdate.CenterZone;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.UI.Game.General.UI
{
    internal sealed class GameGeneralUISystemManager : SystemAbstManager
    {
        internal GameGeneralUISystemManager(EcsWorld gameWorld) : base(gameWorld)
        {
            InitSystems
                .Add(new MainGameGeneralUISystem())
                .Add(new EventGameGeneralSystem());

            UpdateSystems
                .Add(new DonerUISystem())
                .Add(new GetterUnitsUISystem())
                .Add(new ConditionAbilitiesUISystem())
                .Add(new StatsUISystem())
                .Add(new TheEndGameUISystem())
                .Add(new BuildingUISystem())
                .Add(new EconomyUISystem())
                .Add(new LeftBuildingUISystem())
                .Add(new UpdatedUISystem())
                .Add(new UniqueAbilitiesUISystem())
                .Add(new EnvironmentUISystem())
                .Add(new ReadyZoneUISystem())
                .Add(new RightZoneUISystem())
                .Add(new MistakeBarUISystem())
                .Add(new MistakeUISystem())
                .Add(new CenterSupTextUISystem());
        }
    }
}
