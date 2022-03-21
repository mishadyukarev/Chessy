using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using UnityEngine;

namespace Chessy.Menu
{
    public sealed class EntitieManager
    {

        public EntitieManager(in EntitiesView eVC, in EntitiesModel eC)
        {
            eVC.GameGOC.SetActive(false);
            eVC.MenuGOC.SetActive(true);

            var menuZone = eVC.MenuGOC.Transform;

            ToggleZoneVC.ReplaceZone(SceneTypes.Menu);


            var centerZone = menuZone.Find("Center+").GetComponent<Transform>();
            new CenterZoneUICom(centerZone, 0.1f, eC.IsOnHint);
            new LikeGameUICom(centerZone);


            var rightZone = menuZone.Find("OnlineRightZone").GetComponent<RectTransform>();
            new OnZoneUIC(rightZone);
            new ConnectorUIC(true, rightZone);
            new BackgroundUIC(true, rightZone);


            var leftZone = menuZone.Find("OfflineLeftZone").GetComponent<RectTransform>();
            new OffZoneUIC(leftZone);
            new ConUIC(false, leftZone);
            new BackUIC(false, leftZone);
        }
    }
}
