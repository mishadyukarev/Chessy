using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    internal struct UnityEventBaseComponent
    {
        private EventSystem _eventSystem;
        private StandaloneInputModule _standaloneInputModule;

        internal UnityEventBaseComponent(EventSystem eventSystem, StandaloneInputModule standaloneInputModule)
        {
            _eventSystem = eventSystem;
            _standaloneInputModule = standaloneInputModule;
        }
    }
}