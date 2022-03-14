using UnityEngine;
using UnityEngine.EventSystems;

namespace Chessy.Common
{
    public struct UnityEventC
    {
        public static EventSystem EventSystem;
        private StandaloneInputModule _standaloneInputModule;
        private AudioListener _audioListener;

        public UnityEventC(EventSystem eventS, StandaloneInputModule sIM)
        {
            EventSystem = eventS;
            _standaloneInputModule = sIM;
            _audioListener = EventSystem.gameObject.AddComponent<AudioListener>();
        }
    }
}