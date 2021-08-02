using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Common
{
    internal struct ViewCommonContainerUICanvas
    {
        private static EcsEntity _canvasEnt;

        internal ViewCommonContainerUICanvas(EcsWorld commonWorld, Canvas CanvasFromResources)
        {
            var canvas = GameObject.Instantiate(CanvasFromResources);
            canvas.name = "Canvas";
            _canvasEnt = commonWorld.NewEntity()
                .Replace(new CanvasComponent(canvas));
        }

        internal static void SetZoneUI(SceneTypes sceneType, ResourcesComponent resourcesCommComponent)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    _canvasEnt.Get<CanvasComponent>().InMenuZoneGO = UnityEngine.Object.Instantiate(resourcesCommComponent.InMenuZoneGO, _canvasEnt.Get<CanvasComponent>().Canvas.transform);
                    break;

                case SceneTypes.Game:
                    _canvasEnt.Get<CanvasComponent>().InGameZoneGO = UnityEngine.Object.Instantiate(resourcesCommComponent.InGameZoneGO, _canvasEnt.Get<CanvasComponent>().Canvas.transform);
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
                    GameObject.Destroy(_canvasEnt.Get<CanvasComponent>().InMenuZoneGO);
                    break;

                case SceneTypes.Game:
                    GameObject.Destroy(_canvasEnt.Get<CanvasComponent>().InGameZoneGO);
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
                    return _canvasEnt.Get<CanvasComponent>().InMenuZoneGO.transform.Find(nameGO).GetComponent<T>();

                case SceneTypes.Game:
                    return _canvasEnt.Get<CanvasComponent>().InGameZoneGO.transform.Find(nameGO).GetComponent<T>();

                default:
                    throw new Exception();
            }
        }

        internal static GameObject FindUnderParent(SceneTypes sceneType, string nameGO)
        {
            switch (sceneType)
            {
                case SceneTypes.Menu:
                    return _canvasEnt.Get<CanvasComponent>().InMenuZoneGO.transform.Find(nameGO).gameObject;

                case SceneTypes.Game:
                    return _canvasEnt.Get<CanvasComponent>().InGameZoneGO.transform.Find(nameGO).gameObject;

                default:
                    throw new Exception();
            }
        }
    }
}
