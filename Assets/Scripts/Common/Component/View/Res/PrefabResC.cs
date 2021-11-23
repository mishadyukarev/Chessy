using System;
using UnityEngine;

namespace Game.Common
{
    public struct PrefabResC
    {
        public static Canvas Canvas { get; private set; }
        public static GameObject MenuZone_GO { get; private set; }
        public static GameObject GameZone_GO { get; private set; }

        public static Camera Camera { get; private set; }
        public static GameObject CellGO { get; private set; }
        public static GameObject BackGroundCollider2D { get; private set; }

        static PrefabResC()
        {
            Canvas = Resources.Load<Canvas>("Canvas");
            Camera = Resources.Load<Camera>("Camera");
            CellGO = Resources.Load<GameObject>("CellPrefab");
            BackGroundCollider2D = Resources.Load<GameObject>("Background");

            var toggle_Trans = Canvas.transform.Find("Toggle");

            MenuZone_GO = toggle_Trans.Find("Menu").gameObject;
            GameZone_GO = toggle_Trans.Find("Game").gameObject;
        }
    }
}