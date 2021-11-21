using UnityEngine;

namespace Game.Game
{
    public struct CameraVC
    {
        private static Camera _camera;
        private static readonly Vector3 _gamePosCamera;

        static CameraVC()
        {
            _camera = Camera.main;
            _gamePosCamera = new Vector3(7.4f, 4.8f, -2);
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