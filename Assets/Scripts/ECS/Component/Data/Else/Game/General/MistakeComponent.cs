using UnityEngine.Events;

public struct MistakeComponent
{
    internal UnityEvent MistakeUnityEvent { get; private set; }

    internal MistakeTypes MistakeType { get; set; }


    internal MistakeComponent(UnityEvent unityEvent, MistakeTypes mistakeType)
    {
        MistakeUnityEvent = unityEvent;
        MistakeType = mistakeType;
    }
}
