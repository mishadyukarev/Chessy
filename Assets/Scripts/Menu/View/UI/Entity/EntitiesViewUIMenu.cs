using Chessy.Common.View.UI;
using UnityEngine;

namespace Chessy.Menu.View.UI
{
    public class EntitiesViewUIMenu
    {
        public readonly CenterUIEs CenterE;
        public readonly BackUIE BackE;

        public EntitiesViewUIMenu(in EntitiesViewUICommon eUIC)
        {
            var menuZoneUI = eUIC.CanvasE.MenuCanvasGOC.Transform;

            var centerZoneUI = menuZoneUI.Find("Center+");
            CenterE = new CenterUIEs(centerZoneUI);


            var rightZone = menuZoneUI.Find("OnlineRightZone").GetComponent<RectTransform>();
            new OnZoneUIC(rightZone);
            new ConnectorUIC(true, rightZone);
            new BackgroundUIC(true, rightZone);


            var leftZone = menuZoneUI.Find("OfflineLeftZone").GetComponent<RectTransform>();
            new OffZoneUIC(leftZone);
            new ConUIC(false, leftZone);
            BackE = new BackUIE(false, leftZone);
        }
    }
}