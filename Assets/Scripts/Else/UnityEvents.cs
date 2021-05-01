using System;
using UnityEngine.EventSystems;

public class UnityEvents
{
    private EventSystem _eventSystem;
    private StandaloneInputModule _standaloneInputModule;

    public UnityEvents(Builder builderManager)
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
}
