using System;
using UnityEngine;

namespace Scripts.Common
{
    public struct PrefabResComC
    {
        public static Canvas Canvas { get; private set; }
        public static GameObject MenuZone_GO { get; private set; }
        public static GameObject GameZone_GO { get; private set; }

        public static Camera Camera { get; private set; }
        public static GameObject CellGO { get; private set; }
        public static GameObject BackGroundCollider2D { get; private set; }

        public PrefabResComC(bool needUpload) : this()
        {
            if (needUpload)
            {
                Canvas = Resources.Load<Canvas>("Canvas");
                Camera = Resources.Load<Camera>("Camera");
                CellGO = Resources.Load<GameObject>("CellPrefab");
                BackGroundCollider2D = Resources.Load<GameObject>("Background");

                var toggle_Trans = Canvas.transform.Find("Toggle");

                MenuZone_GO = toggle_Trans.Find("Menu").gameObject;
                GameZone_GO = toggle_Trans.Find("Game").gameObject;
            }
            else throw new Exception();
        }
    }
}