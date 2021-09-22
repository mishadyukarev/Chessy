using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        private static ECSManager _eCSmanager;

        public const string VERSION_PHOTON_GAME = "0.1i";

        public static Main Instance { get; private set; }
        public static SceneTypes CurrentSceneType { get; private set; } = SceneTypes.Menu;

        private void Start()
        {
            Instance = this;
            _eCSmanager = new ECSManager();
            ToggleScene(CurrentSceneType);

            //if (Advertisement.isSupported)
            //{
            //    Advertisement.Initialize("4097313", true);
            //}
        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(CurrentSceneType);


            //if (Input.GetMouseButtonDown(0))
            //{
            //    if (Advertisement.IsReady())
            //    {
            //        Advertisement.Show();
            //    }
            //}
        }

        public static void ToggleScene(SceneTypes sceneType)
        {
            CurrentSceneType = sceneType;
            _eCSmanager.ToggleScene(sceneType);
        }
    }
}