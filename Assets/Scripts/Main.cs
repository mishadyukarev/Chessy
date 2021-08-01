using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        private PhotonMainManager _photonManager;
        public const string VERSION_PHOTON_GAME = "0.1i";


        #region Properties

        public static Main Instance { get; private set; }
        public SceneTypes CurrentSceneType { get; private set; } = SceneTypes.Menu;
        public ECSManager ECSmanager { get; private set; }

        #endregion


        private void Start()
        {
            Instance = this;

            ECSmanager = new ECSManager();
            _photonManager = new PhotonMainManager();

            ToggleScene(CurrentSceneType);
        }

        private void Update()
        {
            ECSmanager.OwnUpdate(CurrentSceneType);
            _photonManager.OwnUpdate(CurrentSceneType);

            switch (CurrentSceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
            }
        }

        private void OnApplicationQuit() { }

        public void ToggleScene(SceneTypes sceneType)
        {
            CurrentSceneType = sceneType;

            ECSmanager.ToggleScene(sceneType);
            _photonManager.ToggleScene(sceneType, ECSmanager);

            switch (sceneType)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    break;

                case SceneTypes.Game:
                    break;

                default:
                    throw new Exception();
            }
        }
    }
}