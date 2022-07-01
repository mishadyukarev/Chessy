using UnityEngine;

namespace Chessy.View.UI.Component
{
    public struct CanvasC
    {
        public Canvas Canvas;

        public Transform Transform => Canvas.transform;
        public GameObject GameObject => Canvas.gameObject;

        public CanvasC(Canvas canvas) => Canvas = canvas;
    }
}