using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public struct UnityEventBaseComponent
    {
        private EventSystem _eventSystem;
        private StandaloneInputModule _standaloneInputModule;
        private AudioListener _audioListener;

        public UnityEventBaseComponent(EventSystem eventSystem, StandaloneInputModule standaloneInputModule)
        {
            _eventSystem = eventSystem;
            _standaloneInputModule = standaloneInputModule;
            _audioListener = _eventSystem.gameObject.AddComponent<AudioListener>();
        }
    }
}