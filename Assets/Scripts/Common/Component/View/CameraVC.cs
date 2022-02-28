using UnityEngine;

namespace Chessy.Game
{
    public struct CameraVC
    {
        public Camera Camera;

        public Transform Transform => Camera.transform;

    }
}