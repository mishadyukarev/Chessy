using Chessy.Common.Entity.View.UI;
using Chessy.Common.Interface;
using Chessy.Common.View.UI.Entity;
using System;
using UnityEngine;

namespace Chessy.Common.View.UI
{
    public sealed class EntitiesViewUICommon : IToggleScene
    {
        public readonly CanvasUIE CanvasE;
        public readonly BookUIE BookE;
        public readonly ShopUIE ShopE;
        public readonly SettingsUIE SettingsE;

        public EntitiesViewUICommon(in Canvas canvas, in Transform commonZone)
        {
            canvas.name = "Canvas";
            canvas.transform.SetParent(commonZone);
            CanvasE = new CanvasUIE(canvas);


            var commonZoneUI = canvas.transform.Find("Common+");

            SettingsE = new SettingsUIE(commonZoneUI.Find("Settings+"));
            BookE = new BookUIE(commonZoneUI);
            ShopE = new ShopUIE(commonZoneUI.Find("ShopZone"));
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        CanvasE.MenuCanvasGOC.SetActive(true);
                        CanvasE.GameCanvasGOC.SetActive(false);
                        break;
                    }

                case SceneTypes.Game:
                    {
                        CanvasE.MenuCanvasGOC.SetActive(false);
                        CanvasE.GameCanvasGOC.SetActive(true);
                        break;
                    }
                default: throw new Exception();
            }
        }
    }
}