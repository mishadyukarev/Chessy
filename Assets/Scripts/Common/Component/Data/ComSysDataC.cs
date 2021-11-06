using System;
using System.Collections.Generic;

namespace Scripts.Common
{
    public struct ComSysDataC
    {
        private static Dictionary<ActionDataTypes, Action> _events;
        private static Action<SceneTypes> _toggleScene;

        public ComSysDataC(Dictionary<ActionDataTypes, Action> dict, Action<SceneTypes> toggleScene)
        {
            _events = dict;
            _toggleScene = toggleScene;
        }

        public static void Invoke(ActionDataTypes eventData) => _events[eventData].Invoke();
        public static void ToggleScene(SceneTypes scene) => _toggleScene(scene);
    }
}