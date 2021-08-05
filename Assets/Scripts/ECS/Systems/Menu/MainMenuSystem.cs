using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.System.View.Menu
{
    internal sealed class MainMenuSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _menuWorld;
        [EcsIgnoreInject] private EcsWorld _commonWorld;

        private EcsFilter<OnlineZoneUIComponent> _onlineFilter;
        private EcsFilter<CenterMenuUIComponent> _centerFilter;

        internal MainMenuSystem(EcsWorld commonWorld)
        {
            _commonWorld = commonWorld;
        }

        public void Init()
        {
            ref var canvasCom = ref _commonWorld.GetPool<CanvasComponent>().GetItem(0);
            ref var toggleZoneCom = ref _commonWorld.GetPool<ToggleZoneComponent>().GetItem(0);
            ref var resourcesCom = ref _commonWorld.GetPool<ResourcesComponent>().GetItem(0);
            ref var saverCom = ref _commonWorld.GetPool<SaverComponent>().GetItem(0);

            canvasCom.ReplaceZone(Main.SceneType, resourcesCom);
            toggleZoneCom.ReplaceZone(Main.SceneType);

            _menuWorld.NewEntity()
                .Replace(new CenterMenuUIComponent(canvasCom, saverCom));


            var rightZone = canvasCom.FindUnderParent<RectTransform>("OnlineRightZone");
            _menuWorld.NewEntity()
                .Replace(new OnlineZoneUIComponent(rightZone))
                .Replace(new ConnectButtonUIComponent(true, rightZone))
                .Replace(new BackgroundImagesUIComponent(true, rightZone));


            var leftZone = canvasCom.FindUnderParent<RectTransform>("OfflineLeftZone");
            _menuWorld.NewEntity()
                .Replace(new OfflineZoneUIComponent(leftZone))
                .Replace(new ConnectButtonUIComponent(false, leftZone))
                .Replace(new BackgroundImagesUIComponent(false, leftZone));
        }

        public void Run()
        {
            _commonWorld.GetPool<SaverComponent>().GetItem(0).StepModeType = _onlineFilter.Get1(0).StepModValue;
            _commonWorld.GetPool<SaverComponent>().GetItem(0).SliderVolume = _centerFilter.Get1(0).MusicVolume;
        }
    }
}
