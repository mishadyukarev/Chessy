using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.System.View.Menu
{
    internal sealed class InitSpawnMenuSys : IEcsInitSystem
    {
        private EcsWorld _menuWorld = default;

        public void Init()
        {
            CanvasCom.ReplaceZone(Main.CurrentSceneType);
            ToggleZoneComponent.ReplaceZone(Main.CurrentSceneType);


            var centerZone_Trans = CanvasCom.FindUnderParent<Transform>("CenterZone");



            _menuWorld.NewEntity()
                .Replace(new CenterMenuUICom(centerZone_Trans, SoundComComp.Volume));


            var rightZone = CanvasCom.FindUnderParent<RectTransform>("OnlineRightZone");
            _menuWorld.NewEntity()
                .Replace(new OnlineZoneUICom(rightZone))
                .Replace(new ConnectButtonUICom(true, rightZone))
                .Replace(new BackgroundMenuUICom(true, rightZone));


            var leftZone = CanvasCom.FindUnderParent<RectTransform>("OfflineLeftZone");
            _menuWorld.NewEntity()
                .Replace(new OfflineZoneUICom(leftZone))
                .Replace(new ConnectButtonUICom(false, leftZone))
                .Replace(new BackgroundMenuUICom(false, leftZone));
        }
    }
}
