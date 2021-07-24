using UnityEngine.Events;

namespace Assets.Scripts.ECS.Components.UI
{
    internal struct MistakeResourcesUIComponent
    {
        private UnityEvent _mistakeResourcesUI;

        internal void StartFill() => _mistakeResourcesUI = new UnityEvent();

        internal void RemoveAllListeners() => _mistakeResourcesUI.RemoveAllListeners();
        internal void AddListener(UnityAction unityAction) => _mistakeResourcesUI.AddListener(unityAction);
        internal void Invoke() => _mistakeResourcesUI.Invoke();
    }
}
