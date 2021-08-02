using UnityEngine.Events;

namespace Assets.Scripts.ECS.Components.UI
{
    internal struct MistakeResourcesUIComponent
    {
        internal UnityEvent MistakeResourcesUI { get; private set; }


        internal MistakeResourcesUIComponent(UnityEvent unityEvent) => MistakeResourcesUI = unityEvent;
    }
}
