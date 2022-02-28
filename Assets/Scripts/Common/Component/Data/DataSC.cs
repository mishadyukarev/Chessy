using System;

namespace Chessy.Common
{
    public struct DataSC
    {
        private static Action _runUpdate;
        private static Action<SceneTypes> _toggleScene;

        public DataSC(Action runUpd, Action<SceneTypes> toggleScene)
        {
            _runUpdate = runUpd;
            _toggleScene = toggleScene;
        }

        public static void RunUpdate() => _runUpdate.Invoke();
        public static void ToggleScene(SceneTypes scene) => _toggleScene(scene);
    }
}