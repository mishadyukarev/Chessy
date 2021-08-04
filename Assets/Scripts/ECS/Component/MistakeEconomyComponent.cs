using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Component
{
    internal struct MistakeEconomyComponent
    {
        private Dictionary<ResourceTypes, UnityEvent> _events;

        internal MistakeEconomyComponent(Dictionary<ResourceTypes, UnityEvent> events)
        {
            _events = events;

            for (ResourceTypes resourceType = 0; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
            {
                _events.Add(resourceType, new UnityEvent());
            }
        }

        private UnityEvent GetEvent(ResourceTypes resourceType) => _events[resourceType];
        internal void AddListenerEconomyMistake(ResourceTypes resourceType, UnityAction unityAction) => GetEvent(resourceType).AddListener(unityAction);
        internal void InvokeEconomyMistake(ResourceTypes resourceType) => GetEvent(resourceType).Invoke();
    }
}
