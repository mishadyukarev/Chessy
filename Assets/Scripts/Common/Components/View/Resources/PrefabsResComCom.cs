using UnityEngine;

namespace Scripts.Common
{
    public struct PrefabsResComCom
    {
        public static Canvas Canvas { get; private set; }
        public static GameObject InMenuZoneGO { get; private set; }
        public static GameObject InGameZoneGO { get; private set; }

        public static Camera Camera { get; private set; }
        public static GameObject CellGO { get; private set; }
        public static GameObject BackGroundCollider2D { get; private set; }

        internal PrefabsResComCom(bool needUpload) : this()
        {
            if (needUpload)
            {
                var name = "Prefabs/";

                Canvas = Resources.Load<Canvas>(name + "Canvas");
                Camera = Resources.Load<Camera>(name + "Camera");
                CellGO = Resources.Load<GameObject>(name + "CellPrefab");
                BackGroundCollider2D = Resources.Load<GameObject>(name + "Background");


                InMenuZoneGO = Canvas.transform.Find("InMenuZone").gameObject;
                InGameZoneGO = Canvas.transform.Find("InGameZone").gameObject;
            }
        }
    }
}