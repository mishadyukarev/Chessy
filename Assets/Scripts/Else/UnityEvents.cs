using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnityEvents : IDisposable
{
    private EventSystem _eventSystem;
    private StandaloneInputModule _standaloneInputModule;

    internal UnityEvents()
    {
        var goES = new GameObject("EventSystem");

        _eventSystem = goES.AddComponent<EventSystem>();
        _standaloneInputModule = goES.AddComponent<StandaloneInputModule>();
    }

    public void Dispose()
    {
        GameObject.Destroy(_eventSystem.gameObject);
    }
}
