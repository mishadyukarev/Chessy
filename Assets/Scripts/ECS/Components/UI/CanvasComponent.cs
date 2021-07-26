using System;
using UnityEngine;

namespace Assets.Scripts
{
    public struct CanvasComponent
    {
        internal Canvas Canvas { get; private set; }
        internal GameObject InMenuZoneGO { get; set; }
        internal GameObject InGameZoneGO { get; set; }

        internal CanvasComponent(Canvas canvas)
        {
            Canvas = canvas;
            InMenuZoneGO = Canvas.transform.Find("InMenuZone").gameObject;
            InGameZoneGO = Canvas.transform.Find("InGameZone").gameObject;
            UnityEngine.Object.Destroy(InMenuZoneGO);
            UnityEngine.Object.Destroy(InGameZoneGO);
        }
    }
}