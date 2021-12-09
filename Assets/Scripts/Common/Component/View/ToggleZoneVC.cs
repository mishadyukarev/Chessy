using System;
using UnityEngine;

namespace Game.Common
{
    public readonly struct ToggleZoneVC
    {
        static GameObject _toggleZoneGO;

        public ToggleZoneVC(GameObject toggleZoneGO) => _toggleZoneGO = toggleZoneGO;


        public static void ReplaceZone(SceneTypes sceneType)
        {
            GameObject.Destroy(_toggleZoneGO);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    _toggleZoneGO = new GameObject(NameConst.IN_MENU_GAME_ZONE);
                    break;

                case SceneTypes.Game:
                    _toggleZoneGO = new GameObject(NameConst.GAME);
                    break;

                default:
                    throw new Exception();
            }
        }
        public static void Attach(Transform transform) => transform.transform.SetParent(_toggleZoneGO.transform);
    }
}
