using UnityEngine;

namespace Assets.Scripts
{
    internal struct CameraComComp
    {
        private static Camera _camera;
        private static Vector3 _gamePosCamera;

        internal CameraComComp(Camera camera, Vector3 gamePosCamera)
        {
            _camera = camera;
            _gamePosCamera = gamePosCamera;
        }

        internal static void SetPosForMaster(bool isMasterClient)
        {
            if (isMasterClient)
            {
                _camera.transform.position = Main.Instance.transform.position + _gamePosCamera;
            }
            else
            {
                _camera.transform.position = Main.Instance.transform.position + _gamePosCamera + new Vector3(0, 0.5f, 0);
            } 
        }

        internal static void SetRotForMaster(bool isMasterClient)
        {
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