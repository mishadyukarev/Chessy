﻿using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Common;
using Assets.Scripts.ECS.Component.Menu;
using Assets.Scripts.ECS.Component.UI;
using Assets.Scripts.ECS.Components.View.UI.Menu.Center;
using Assets.Scripts.ECS.Components.View.UI.Menu.Down;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.ECS.System.View.Menu
{
    internal sealed class InitSpawnMenuSys : IEcsInitSystem
    {
        private EcsWorld _curMenuWorld = default;

        public void Init()
        {
            CanvasCom.ReplaceZone(Main.CurrentSceneType);
            ToggleZoneComponent.ReplaceZone(Main.CurrentSceneType);


            var centerZone_Trans = CanvasCom.FindUnderParent<Transform>("CenterZone");
            var downZone_Trans = CanvasCom.FindUnderParent<Transform>("DownZone");


            _curMenuWorld.NewEntity()
                //center
                .Replace(new CenterMenuUICom(centerZone_Trans, SoundComComp.Volume))
                .Replace(new ShopZoneUIMenuCom(centerZone_Trans))

                //down
                .Replace(new DownZoneUIMenuCom(downZone_Trans));


            var rightZone = CanvasCom.FindUnderParent<RectTransform>("OnlineRightZone");
            _curMenuWorld.NewEntity()
                .Replace(new OnlineZoneUICom(rightZone))
                .Replace(new ConnectButtonUICom(true, rightZone))
                .Replace(new BackgroundMenuUICom(true, rightZone));


            var leftZone = CanvasCom.FindUnderParent<RectTransform>("OfflineLeftZone");
            _curMenuWorld.NewEntity()
                .Replace(new OfflineZoneUICom(leftZone))
                .Replace(new ConnectButtonUICom(false, leftZone))
                .Replace(new BackgroundMenuUICom(false, leftZone));
        }
    }
}
