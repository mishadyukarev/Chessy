using UnityEngine.Events;

public struct MistakeComponent
{
    private UnityEvent _mistakeUnityEvent;

    internal MistakeTypes MistakeType;

    internal void CreateEvent() => _mistakeUnityEvent = new UnityEvent();
    internal void AddListener(UnityAction unityAction) => _mistakeUnityEvent.AddListener(unityAction);
    internal void Invoke() => _mistakeUnityEvent.Invoke();
}
