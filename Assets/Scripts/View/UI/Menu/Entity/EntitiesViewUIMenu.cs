using Chessy.Common.Component;
using Chessy.Common.View.UI;
using UnityEngine;

namespace Chessy.Menu.View.UI
{
    public class EntitiesViewUIMenu
    {
        internal readonly GameObjectVC ParentGOC;

        public readonly CenterUIE CenterE;

        public readonly OnlineZoneUIE OnlineZoneE;
        public readonly OfflineZoneUIE OfflineZoneE;

        public EntitiesViewUIMenu(in EntitiesViewUICommon eUICcommon)
        {
            var menuZoneUI = eUICcommon.CanvasE.MenuCanvasGOC.Transform;

            ParentGOC = new GameObjectVC(menuZoneUI.gameObject);

            var centerZoneUI = menuZoneUI.Find("Center+");
            CenterE = new CenterUIE(centerZoneUI);


            var rightZone = menuZoneUI.Find("OnlineRightZone").GetComponent<RectTransform>();
            OnlineZoneE = new OnlineZoneUIE(rightZone);


            var leftZone = menuZoneUI.Find("OfflineLeftZone").GetComponent<RectTransform>();
            OfflineZoneE = new OfflineZoneUIE(leftZone);
        }
    }
}