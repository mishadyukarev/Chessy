using System;

namespace Scripts.Common
{
    public struct CommonDataC
    {
        public static Action<SceneTypes> ToggleScene { get; private set; }
        public static Action LaunchAdSys { get; private set; }

        public CommonDataC(Action<SceneTypes> toggleScene, Action launchAdd)
        {
            ToggleScene = toggleScene;
            LaunchAdSys = launchAdd;
        }
    }
}