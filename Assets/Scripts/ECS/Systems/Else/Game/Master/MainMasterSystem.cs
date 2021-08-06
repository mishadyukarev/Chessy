using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Game.Master
{
    internal sealed class MainMasterSystem : IEcsInitSystem
    {
        private EcsWorld _gameWorld;

        public void Init()
        {
            _gameWorld.NewEntity()
                .Replace(new InfoMasCom());
        }
    }
}
