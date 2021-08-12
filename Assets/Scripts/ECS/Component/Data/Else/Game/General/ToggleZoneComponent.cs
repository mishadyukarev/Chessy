﻿using Assets.Scripts.Abstractions;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Component
{
    internal readonly struct ToggleZoneComponent
    {
        private static GameObject _toggleZoneGO;

        internal ToggleZoneComponent(GameObject toggleZoneGO) => _toggleZoneGO = toggleZoneGO;


        internal static void ReplaceZone(SceneTypes sceneType)
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
        internal static void Attach(Transform transform) => transform.transform.SetParent(_toggleZoneGO.transform);
    }
}