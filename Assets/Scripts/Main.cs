using System;
using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        private static ECSManager _eCSmanager;

        public const string VERSION_PHOTON_GAME = "0.1i";

        public static Main Instance { get; private set; }
        public static SceneTypes SceneType { get; private set; } = SceneTypes.Menu;

        private void Start()
        {
            Instance = this;
            _eCSmanager = new ECSManager();
            ToggleScene(SceneType);
        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(SceneType);

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

        public static void ToggleScene(SceneTypes sceneType)
        {
            SceneType = sceneType;
            _eCSmanager.ToggleScene(sceneType);

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