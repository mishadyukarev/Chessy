using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.System.View.Menu
{
    internal sealed class SpawnMenuSys : IEcsInitSystem
    {
        private EcsWorld _menuWorld = default;

        private EcsFilter<OnlineZoneUIComponent> _onlineFilter = default;
        private EcsFilter<CenterMenuUIComp> _centerFilter = default;

        public void Init()
        {
            CanvasComp.ReplaceZone(Main.CurrentSceneType);
            ToggleZoneComponent.ReplaceZone(Main.CurrentSceneType);




            _menuWorld.NewEntity()
                .Replace(new CenterMenuUIComp(CanvasComp.FindUnderParent<Slider>("Slider"), SoundComComp.Volume));


            var rightZone = CanvasComp.FindUnderParent<RectTransform>("OnlineRightZone");
            _menuWorld.NewEntity()
                .Replace(new OnlineZoneUIComponent(rightZone))
                .Replace(new ConnectButtonUIComp(true, rightZone))
                .Replace(new BackgroundImagesUIComponent(true, rightZone));


            var leftZone = CanvasComp.FindUnderParent<RectTransform>("OfflineLeftZone");
            _menuWorld.NewEntity()
                .Replace(new OfflineZoneUIComponent(leftZone))
                .Replace(new ConnectButtonUIComp(false, leftZone))
                .Replace(new BackgroundImagesUIComponent(false, leftZone));
        }
    }
}
