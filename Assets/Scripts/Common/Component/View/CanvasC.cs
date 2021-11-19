using System;
using UnityEngine;

namespace Game.Common
{
    public struct CanvasC
    {
        private static Canvas _canvas;
        private static GameObject _comZone_GO;

        private static Transform _toggle_Trans;
        private static GameObject _curZone_GO;

        public CanvasC(Canvas canvas)
        {
            _canvas = canvas;
            _curZone_GO = default;

            _comZone_GO = _canvas.transform.Find("Common").gameObject;


            _toggle_Trans = _canvas.transform.Find("Toggle");

            GameObject.Destroy(_toggle_Trans.Find(PrefabResComC.GameZone_GO.name).gameObject);
            GameObject.Destroy(_toggle_Trans.Find(PrefabResComC.MenuZone_GO.name).gameObject);
        }

        public static void SetCurZone(SceneTypes sceneType)
        {
            GameObject.Destroy(_curZone_GO);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _curZone_GO = GameObject.Instantiate(PrefabResComC.MenuZone_GO, _toggle_Trans);
                    break;

                case SceneTypes.Game:
                    _curZone_GO = GameObject.Instantiate(PrefabResComC.GameZone_GO, _toggle_Trans);
                    break;

                default:
                    throw new Exception();
            }
        }
        public static T FindUnderCurZone<T>(string name) => _curZone_GO.transform.Find(name).GetComponent<T>();
        public static GameObject FindUnderCurZone(string name) => _curZone_GO.transform.Find(name).gameObject;

        public static T FindUnderComZone<T>(string name) => _comZone_GO.transform.Find(name).GetComponent<T>();
    }
}