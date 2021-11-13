using UnityEngine;

namespace Chessy.Game
{
    public struct CameraVC
    {
        private static Camera _camera;
        private static Vector3 _gamePosCamera;

        public CameraVC(Camera camera, Vector3 gamePosCamera)
        {
            _camera = camera;
            _gamePosCamera = gamePosCamera;
        }

        public static void SetPosRotClient(PlayerTypes playerType, Vector3 posMain)
        {
            if (playerType == PlayerTypes.None) throw new System.Exception();

            if (playerType == PlayerTypes.First)
            {
                _camera.transform.position = posMain + _gamePosCamera;
                _camera.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                _camera.transform.position = posMain + _gamePosCamera + new Vector3(0, 0.5f, 0);
                _camera.transform.eulerAngles = new Vector3(0, 0, 180);
            }
        }
    }
}