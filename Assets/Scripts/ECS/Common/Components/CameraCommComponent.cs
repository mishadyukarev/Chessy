using UnityEngine;

namespace Assets.Scripts
{
    internal struct CameraCommComponent
    {
        private Camera _camera;
        internal Camera Camera => _camera;

        internal void SetCamera(Camera camera)
        {
            _camera = camera;
        }
    }
}