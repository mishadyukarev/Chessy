using Chessy.Common;
using UnityEngine;

namespace Chessy
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