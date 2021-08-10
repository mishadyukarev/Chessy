using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Game.Master
{
    internal sealed class MainMasterSystem : IEcsInitSystem
    {
        private EcsWorld _currentGameWorld = default;

        public void Init()
        {
            _currentGameWorld.NewEntity()
                .Replace(new InfoMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForGettingUnitMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForSettingUnitMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForAttackMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForShiftMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForDonerMasCom())
                .Replace(new NeedActiveSomethingMasCom());

            _currentGameWorld.NewEntity()
                .Replace(new ForBuildingMasCom());
        }
    }
}
