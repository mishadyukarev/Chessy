using Photon.Pun;
using Photon.Realtime;
using Scripts.Common;
using UnityEngine;

namespace Scripts
{
    public sealed class Main : MonoBehaviour
    {
        private SceneTypes _currentSceneType = SceneTypes.Menu;
        private ECSManager _eCSmanager;

        private void Start()
        {
            _eCSmanager = new ECSManager(ToggleScene, gameObject);
            ToggleScene(_currentSceneType);

            Application.runInBackground = true;

        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(_currentSceneType);
        }

        private void ToggleScene(SceneTypes sceneType)
        {
            _currentSceneType = sceneType;
            _eCSmanager.ToggleScene(sceneType);
        }
    }
}