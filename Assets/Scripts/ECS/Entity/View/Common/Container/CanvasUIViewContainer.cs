using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Common
{
    internal sealed class CanvasUIViewContainer
    {
        private static EntCommonManager ECM => Main.Instance.ECSmanager.EntCommonManager;

        internal static void SetZoneUI(SceneTypes sceneType, ResourcesComponent resourcesCommComponent)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    ECM.CanvasEnt_CanvasCommCom.InMenuZoneGO = UnityEngine.Object.Instantiate(resourcesCommComponent.InMenuZoneGO, ECM.CanvasEnt_CanvasCommCom.Canvas.transform);
                    break;

                case SceneTypes.Game:
                    ECM.CanvasEnt_CanvasCommCom.InGameZoneGO = UnityEngine.Object.Instantiate(resourcesCommComponent.InGameZoneGO, ECM.CanvasEnt_CanvasCommCom.Canvas.transform);
                    break;

                default:
                    break;
            }
        }

        internal static void DestroyZoneUI(SceneTypes sceneType)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    GameObject.Destroy(ECM.CanvasEnt_CanvasCommCom.InMenuZoneGO);
                    break;

                case SceneTypes.Game:
                    GameObject.Destroy(ECM.CanvasEnt_CanvasCommCom.InGameZoneGO);
                    break;

                default:
                    break;
            }
        }

        internal static T FindUnderParent<T>(SceneTypes sceneType, string nameGO)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    return ECM.CanvasEnt_CanvasCommCom.InMenuZoneGO.transform.Find(nameGO).GetComponent<T>();

                case SceneTypes.Game:
                    return ECM.CanvasEnt_CanvasCommCom.InGameZoneGO.transform.Find(nameGO).GetComponent<T>();

                default:
                    throw new Exception();
            }
        }

        internal static GameObject FindUnderParent(SceneTypes sceneType, string nameGO)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    return ECM.CanvasEnt_CanvasCommCom.InMenuZoneGO.transform.Find(nameGO).gameObject;

                case SceneTypes.Game:
                    return ECM.CanvasEnt_CanvasCommCom.InGameZoneGO.transform.Find(nameGO).gameObject;

                default:
                    throw new Exception();
            }
        }
    }
}
