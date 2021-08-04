using UnityEngine;

namespace Assets.Scripts.ECS.Component
{
    internal struct CanvasComponent
    {
        private Canvas _canvas;

        internal CanvasComponent(Canvas canvas) => _canvas = canvas;
    }
}
