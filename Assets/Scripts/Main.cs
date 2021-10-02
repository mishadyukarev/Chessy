using UnityEngine;

namespace Assets.Scripts
{
    public sealed class Main : MonoBehaviour
    {
        private static SceneTypes _currentSceneType = SceneTypes.Menu;
        private static ECSManager _eCSmanager;

        private void Start()
        {
            _eCSmanager = new ECSManager(ToggleScene, gameObject);
            ToggleScene(_currentSceneType);
        }

        private void Update()
        {
            _eCSmanager.OwnUpdate(_currentSceneType);
        }

        public static void ToggleScene(SceneTypes sceneType)
        {
            _currentSceneType = sceneType;
            _eCSmanager.ToggleScene(sceneType);
        }
    }
}