using ECS;
using Chessy.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Menu
{
    public sealed class EntitieManager
    {
        static readonly Dictionary<string, Entity> _ents;

        static EntitieManager()
        {
            _ents = new Dictionary<string, Entity>();
        }
        public EntitieManager(in EcsWorld worldEcs)
        {
            CanvasC.SetCurZone(SceneTypes.Menu);
            ToggleZoneVC.ReplaceZone(SceneTypes.Menu);


            var centerZone_Trans = CanvasC.FindUnderCurZone<Transform>("CenterZone");


            worldEcs.NewEntity()
                .Add(new CenterZoneUICom(centerZone_Trans, SoundC.Volume, HintC.IsOnHint))
                .Add(new LikeGameUICom(centerZone_Trans));


            var rightZone = CanvasC.FindUnderCurZone<RectTransform>("OnlineRightZone");
            worldEcs.NewEntity()
                .Add(new OnZoneUIC(rightZone))
                .Add(new ConnectorUIC(true, rightZone))
                .Add(new BackgroundUIC(true, rightZone));


            var leftZone = CanvasC.FindUnderCurZone<RectTransform>("OfflineLeftZone");
            worldEcs.NewEntity()
                .Add(new OffZoneUIC(leftZone))
                .Add(new ConUIC(false, leftZone))
                .Add(new BackUIC(false, leftZone));
        }
    }
}
