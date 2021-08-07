using UnityEngine;

namespace Assets.Scripts
{
    internal struct CameraComponent
    {
        private static Camera _camera;
        internal static Vector3 PosForCamera { get; private set; }

        internal CameraComponent(Camera camera, Vector3 posForCamera)
        {
            _camera = camera;
            PosForCamera = posForCamera;
        }

        internal static void SetPosition(Vector3 pos) => _camera.transform.position = pos;
        internal static void SetRotation(Quaternion rotation) => _camera.transform.rotation = rotation;
        internal static void ResetRotation() => _camera.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}