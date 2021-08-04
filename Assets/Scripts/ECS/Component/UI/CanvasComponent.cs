using System;
using UnityEngine;

namespace Assets.Scripts
{
    public struct CanvasComponent
    {
        private Canvas _canvas;
        private GameObject _currentZoneGO;

        internal CanvasComponent(Canvas canvas)
        {
            _canvas = canvas;
            _currentZoneGO = default;

            GameObject.Destroy(_canvas.transform.Find("InGameZone").gameObject);
            GameObject.Destroy(_canvas.transform.Find("InMenuZone").gameObject);
        }

        internal void ReplaceZone(SceneTypes sceneType, ResourcesComponent resourcesComponent)
        {
            GameObject.Destroy(_currentZoneGO);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _currentZoneGO = GameObject.Instantiate(resourcesComponent.InMenuZoneGO, _canvas.transform);
                    break;

                case SceneTypes.Game:
                    _currentZoneGO = GameObject.Instantiate(resourcesComponent.InGameZoneGO, _canvas.transform);
                    break;

                default:
                    throw new Exception();
            }
        }
        internal T FindUnderParent<T>(string name) => _currentZoneGO.transform.Find(name).GetComponent<T>();
        internal GameObject FindUnderParent(string name) => _currentZoneGO.transform.Find(name).gameObject;
    }
}