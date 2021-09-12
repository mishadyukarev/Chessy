using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.UI;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Common
{
    internal sealed class SyncSoundMenuSys : IEcsRunSystem
    {
        private EcsFilter<CenterMenuUIComponent> _centerUIFilter = default;

        public void Run()
        {
            SoundComComp.Volume = _centerUIFilter.Get1(0).MusicVolume;
        }
    }
}
