using UnityEngine;

namespace Scripts.Common
{
    public struct CameraComComp
    {
        private static Camera _camera;
        private static Vector3 _gamePosCamera;

        public CameraComComp(Camera camera, Vector3 gamePosCamera)
        {
            _camera = camera;
            _gamePosCamera = gamePosCamera;
        }

        public static void SetPosRotClient(bool isMasterClient, Vector3 posMain)
        {
            if (isMasterClient)
            {
                _camera.transform.position = posMain + _gamePosCamera;
            }
            else
            {
                _camera.transform.position = posMain + _gamePosCamera + new Vector3(0, 0.5f, 0);
            }

            if (isMasterClient)
            {
                _camera.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                _camera.transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}