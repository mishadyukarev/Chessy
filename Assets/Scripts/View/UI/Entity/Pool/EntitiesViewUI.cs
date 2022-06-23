using Chessy.Common;
using Chessy.Common.Component;
using Chessy.Common.Entity.View.UI;
using Chessy.Common.View.UI;
using Chessy.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Model
{
    public sealed class EntitiesViewUI
    {
        public readonly CanvasC CanvasC;
        public readonly GameObjectVC MenuCanvasGOC;
        public readonly GameObjectVC GameCanvasGOC;
        internal readonly BookUIE BookE;
        public readonly ShopUIE ShopE;
        public readonly SettingsUIE SettingsE;

        public readonly LeftUIEs LeftEs;
        public readonly RightUIEs RightEs;
        public readonly CenterUIEs CenterEs;
        public readonly DownUIEs DownEs;
        public readonly UpUIEs UpEs;

        #region Menu

        public readonly CenterUIE CenterE;
        public readonly OnlineZoneUIE OnlineZoneE;
        public readonly OfflineZoneUIE OfflineZoneE;

        #endregion


        public LeftEnvironmentUIEs LeftEnvEs => LeftEs.EnvironmentEs;

        public EntitiesViewUI()
        {
            var canvas = GameObject.Instantiate(UnityEngine.Resources.Load<Canvas>("Canvas+"));

            CanvasC = new CanvasC(canvas);
            MenuCanvasGOC = new GameObjectVC(canvas.transform.Find("Menu+").gameObject);
            GameCanvasGOC = new GameObjectVC(canvas.transform.Find("Game+").gameObject);

            GameCanvasGOC.SetActive(false);
            MenuCanvasGOC.SetActive(true);


            canvas.name = "Canvas";


            SettingsE = new SettingsUIE(canvas.transform.Find("Settings+"));
            BookE = new BookUIE(canvas.transform);
            ShopE = new ShopUIE(canvas.transform.Find("ShopZone"));



            var gameZone = GameCanvasGOC.Transform;

            LeftEs = new LeftUIEs(gameZone.Find("Left+"));
            RightEs = new RightUIEs(gameZone.Find("Right+"));
            CenterEs = new CenterUIEs(gameZone.Find("Center+"));
            DownEs = new DownUIEs(gameZone.Find("Down+"));
            UpEs = new UpUIEs(gameZone.Find("Exit+").Find("Button+").GetComponent<Button>(), gameZone.Find("Up+"));



            #region Menu

            var menuZoneUI = MenuCanvasGOC.Transform;

            var centerZoneUI = menuZoneUI.Find("Center+");
            CenterE = new CenterUIE(centerZoneUI);


            var rightZone = menuZoneUI.Find("OnlineRightZone").GetComponent<RectTransform>();
            OnlineZoneE = new OnlineZoneUIE(rightZone);


            var leftZone = menuZoneUI.Find("OfflineLeftZone").GetComponent<RectTransform>();
            OfflineZoneE = new OfflineZoneUIE(leftZone);

            #endregion

        }
    }
}