using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    internal struct UnityEventComponent
    {
        private EventSystem _eventSystem;
        private StandaloneInputModule _standaloneInputModule;

        internal UnityEventComponent(EventSystem eventSystem, StandaloneInputModule standaloneInputModule)
        {
            _eventSystem = eventSystem;
            _standaloneInputModule = standaloneInputModule;
        }
    }
}