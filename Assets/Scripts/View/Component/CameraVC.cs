using UnityEngine;
namespace Chessy.View.Component
{
    public struct CameraVC
    {
        public Camera Camera;

        public Transform Transform => Camera.transform;

        public CameraVC(in Camera camera) => Camera = camera;
    }
}