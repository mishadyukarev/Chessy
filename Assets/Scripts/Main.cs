using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        private PhotonMainManager _photonManager;
        public const string VERSION_PHOTON_GAME = "0.1i";

        public static Main Instance { get; private set; }
        public SceneTypes SceneType { get; private set; } = SceneTypes.Menu;
        public ECSManager ECSmanager { get; private set; }


        private void Start()
        {
            Instance = this;

            ECSmanager = new ECSManager();
            _photonManager = new PhotonMainManager();

            ToggleScene(SceneType);
        }

        private void Update()
        {
            ECSmanager.OwnUpdate(SceneType);
            _photonManager.OwnUpdate(SceneType);

            switch (SceneType)
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

        public void ToggleScene(SceneTypes sceneType)
        {
            SceneType = sceneType;

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