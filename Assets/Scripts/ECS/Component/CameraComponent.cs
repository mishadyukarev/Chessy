using UnityEngine;

namespace Assets.Scripts
{
    internal struct CameraComponent
    {
        internal Camera Camera { get; private set; }
        internal Vector3 PosForCamera { get; private set; }

        internal CameraComponent(Camera camera, Vector3 posForCamera)
        {
            Camera = camera;
            PosForCamera = posForCamera;
        }
    }
}