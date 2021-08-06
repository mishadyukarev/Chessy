using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Component
{
    internal struct MistakeUEComponent
    {
        private Dictionary<ResourceTypes, UnityEvent> _events;

        private static UnityEvent _mistakeSteps_UE;
        private static UnityEvent _mistakePlace_UE;

        internal MistakeUEComponent(Dictionary<ResourceTypes, UnityEvent> events)
        {
            _events = events;

            for (ResourceTypes resourceType = 0; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
            {
                _events.Add(resourceType, new UnityEvent());
            }

            _mistakeSteps_UE = new UnityEvent();
            _mistakePlace_UE = new UnityEvent();
        }



        private UnityEvent GetEvent(ResourceTypes resourceType) => _events[resourceType];
        internal void AddListenerEconomyMistake(ResourceTypes resourceType, UnityAction unityAction) => GetEvent(resourceType).AddListener(unityAction);
        internal void InvokeEconomyMistake(ResourceTypes resourceType) => GetEvent(resourceType).Invoke();

        internal void AddListenerStepMistake(UnityAction unityAction) => _mistakeSteps_UE.AddListener(unityAction);
        internal void AddListenerNeedOtherPlaceMistake(UnityAction unityAction) => _mistakePlace_UE.AddListener(unityAction);

        internal void InvokeStepsMistake() => _mistakeSteps_UE.Invoke();
        internal void InvokeNeedOtherPlace() => _mistakePlace_UE.Invoke();
    }
}
