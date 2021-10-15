using System;
using UnityEngine;

namespace Scripts.Common
{
    public struct CanvasCom
    {
        private static Canvas _canvas;
        private static GameObject _currentZoneGO;

        internal CanvasCom(Canvas canvas)
        {
            _canvas = canvas;
            _currentZoneGO = default;

            GameObject.Destroy(_canvas.transform.Find("InGameZone").gameObject);
            GameObject.Destroy(_canvas.transform.Find("InMenuZone").gameObject);
        }

        public static void ReplaceZone(SceneTypes sceneType)
        {
            GameObject.Destroy(_currentZoneGO);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _currentZoneGO = GameObject.Instantiate(PrefabsResComCom.InMenuZoneGO, _canvas.transform);
                    break;

                case SceneTypes.Game:
                    _currentZoneGO = GameObject.Instantiate(PrefabsResComCom.InGameZoneGO, _canvas.transform);
                    break;

                default:
                    throw new Exception();
            }
        }
        public static T FindUnderParent<T>(string name) => _currentZoneGO.transform.Find(name).GetComponent<T>();
        public static GameObject FindUnderParent(string name) => _currentZoneGO.transform.Find(name).gameObject;
    }
}