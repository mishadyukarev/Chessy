using System;
using UnityEngine;

namespace Chessy.Common
{
    public readonly struct ToggleZoneComponent
    {
        private static GameObject _toggleZoneGO;

        public ToggleZoneComponent(GameObject toggleZoneGO) => _toggleZoneGO = toggleZoneGO;


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
