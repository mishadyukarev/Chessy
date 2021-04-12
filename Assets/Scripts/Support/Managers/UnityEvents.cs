using Leopotam.Ecs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnityEvents
{
    private EventSystem _eventSystem;
    private StandaloneInputModule _standaloneInputModule;

    public UnityEvents(SupportManager supportManager)
    {
        var types = new Type[]
        {
            typeof(EventSystem),
            typeof(StandaloneInputModule),
        };

        supportManager.BuilderManager.CreateGameObject(out GameObject goES, "EventSystem", types);

        _eventSystem = goES.GetComponent<EventSystem>();
        _standaloneInputModule = goES.GetComponent<StandaloneInputModule>();
    }
}
