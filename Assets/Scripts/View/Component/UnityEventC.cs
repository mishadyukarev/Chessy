using UnityEngine;
using UnityEngine.EventSystems;
namespace Chessy.View.Component
{
    public struct UnityEventC
    {
        public EventSystem EventSystem;
        public StandaloneInputModule StandaloneInputModule;
        public AudioListener AudioListener;

        public UnityEventC(EventSystem eventS, StandaloneInputModule sIM)
        {
            EventSystem = eventS;
            StandaloneInputModule = sIM;
            AudioListener = EventSystem.gameObject.AddComponent<AudioListener>();
        }
    }
}