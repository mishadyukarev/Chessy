using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Common
{
    public struct UnityEventC
    {
        private EventSystem _eventSystem;
        private StandaloneInputModule _standaloneInputModule;
        private AudioListener _audioListener;

        public UnityEventC(EventSystem eventS, StandaloneInputModule sIM)
        {
            _eventSystem = eventS;
            _standaloneInputModule = sIM;
            _audioListener = _eventSystem.gameObject.AddComponent<AudioListener>();
        }
    }
}