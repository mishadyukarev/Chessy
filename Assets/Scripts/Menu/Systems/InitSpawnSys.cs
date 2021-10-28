using Leopotam.Ecs;
using Scripts.Common;
using UnityEngine;

namespace Scripts.Menu
{
    public sealed class InitSpawnSys : IEcsInitSystem
    {
        private EcsWorld _curMenuWorld = default;

        public void Init()
        {
            CanvasCom.ReplaceZone(SceneTypes.Menu);
            ToggleZoneComponent.ReplaceZone(SceneTypes.Menu);


            var centerZone_Trans = CanvasCom.FindUnderParent<Transform>("CenterZone");


            _curMenuWorld.NewEntity()
                .Replace(new CenterZoneUICom(centerZone_Trans, SoundComComp.Volume))
                .Replace(new ShopZoneUICom(centerZone_Trans))
                .Replace(new LikeGameUICom(centerZone_Trans));


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
