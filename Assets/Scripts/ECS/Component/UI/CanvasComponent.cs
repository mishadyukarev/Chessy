using System;
using UnityEngine;

namespace Assets.Scripts
{
    public struct CanvasComponent
    {
        private Canvas _canvas;
        private GameObject _currentZoneGO;

        internal CanvasComponent(ResourcesComponent resourcesComponent, SceneTypes neededSceneType)
        {
            _canvas = GameObject.Instantiate(resourcesComponent.PrefabConfig.Canvas);

            GameObject.Destroy(_canvas.transform.Find("InGameZone").gameObject);
            GameObject.Destroy(_canvas.transform.Find("InMenuZone").gameObject);


            switch (neededSceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _currentZoneGO = GameObject.Instantiate(resourcesComponent.InMenuZoneGO);
                    break;

                case SceneTypes.Game:
                    _currentZoneGO = GameObject.Instantiate(resourcesComponent.InGameZoneGO);
                    break;

                default:
                    throw new Exception();
            }
        }

        internal T FindUnderParent<T>(string name) => _currentZoneGO.transform.Find(name).GetComponent<T>();
        internal GameObject FindUnderParent(string name) => _currentZoneGO.transform.Find(name).gameObject;
    }
}