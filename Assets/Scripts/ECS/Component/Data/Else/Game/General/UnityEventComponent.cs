using UnityEngine.Events;

namespace Assets.Scripts.ECS.Components
{
    internal struct UnityEventComponent
    {
        internal UnityEvent UnityEvent { get; private set; }

        internal UnityEventComponent(UnityEvent unityEvent) => UnityEvent = unityEvent;
    }
}
