using Assets.Scripts.ECS.Component.Common;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Common
{
    internal sealed class SyncSoundComSys : IEcsRunSystem
    {
        private EcsFilter<SoundComComp> _soundComFilter = default;

        public void Run()
        {
            _soundComFilter.Get1(0).Volume = SaverComponent.SliderVolume;
        }
    }
}
