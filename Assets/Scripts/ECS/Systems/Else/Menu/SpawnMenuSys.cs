using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.System.View.Menu
{
    internal sealed class SpawnMenuSys : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _menuWorld = default;

        private EcsFilter<OnlineZoneUIComponent> _onlineFilter = default;
        private EcsFilter<CenterMenuUIComponent> _centerFilter = default;

        public void Init()
        {
            CanvasComponent.ReplaceZone(Main.CurrentSceneType);
            ToggleZoneComponent.ReplaceZone(Main.CurrentSceneType);

            _menuWorld.NewEntity()
                .Replace(new CenterMenuUIComponent(CanvasComponent.FindUnderParent<Slider>("Slider"), SaverComponent.SliderVolume));


            var rightZone = CanvasComponent.FindUnderParent<RectTransform>("OnlineRightZone");
            _menuWorld.NewEntity()
                .Replace(new OnlineZoneUIComponent(rightZone))
                .Replace(new ConnectButtonUIComponent(true, rightZone))
                .Replace(new BackgroundImagesUIComponent(true, rightZone));


            var leftZone = CanvasComponent.FindUnderParent<RectTransform>("OfflineLeftZone");
            _menuWorld.NewEntity()
                .Replace(new OfflineZoneUIComponent(leftZone))
                .Replace(new ConnectButtonUIComponent(false, leftZone))
                .Replace(new BackgroundImagesUIComponent(false, leftZone));
        }

        public void Run()
        {
            SaverComponent.StepModeType = _onlineFilter.Get1(0).StepModValue;
            SaverComponent.SliderVolume = _centerFilter.Get1(0).MusicVolume;
        }
    }
}
