using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnityEvents : IDisposable
{
    private EventSystem _eventSystem;
    private StandaloneInputModule _standaloneInputModule;

    internal UnityEvents(Builder builderManager)
    {
        var types = new Type[]
        {
            typeof(EventSystem),
            typeof(StandaloneInputModule),
        };

        var goES = builderManager.CreateGameObject("EventSystem", types);

        _eventSystem = goES.GetComponent<EventSystem>();
        _standaloneInputModule = goES.GetComponent<StandaloneInputModule>();
    }

    public void Dispose()
    {
        GameObject.Destroy(_eventSystem.gameObject);
    }
}
