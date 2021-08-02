using UnityEngine;

namespace Assets.Scripts
{
    internal struct CameraComponent
    {
        internal Camera Camera { get; private set; }

        internal CameraComponent(Camera camera) => Camera = camera;
    }
}