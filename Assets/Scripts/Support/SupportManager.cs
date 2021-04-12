using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SupportManager
{
    private BuilderManager _builderManager;
    private CellManager _cellManager;
    private ResourcesLoadManager _resourcesLoadManager;
    private UnityEvents _eventDelegateManager;
    private SoundManager _soundManager;
    private NameManager _nameManager;
    private StartValuesConfig _startValues;


    public BuilderManager BuilderManager => _builderManager;
    public CellManager CellManager => _cellManager;
    public ResourcesLoadManager ResourcesLoadManager => _resourcesLoadManager;
    public UnityEvents DelegateEventManager => _eventDelegateManager;
    internal SoundManager SoundManager => _soundManager;
    internal NameManager NameManager => _nameManager;
    internal StartValuesConfig StartValues => _startValues;



    public SupportManager()
    {
        _resourcesLoadManager = new ResourcesLoadManager();
        _startValues = _resourcesLoadManager.StartValuesConfig;
        _builderManager = new BuilderManager();
        _cellManager = new CellManager(_startValues);
        _eventDelegateManager = new UnityEvents(this);
        _soundManager = new SoundManager(this);
        _nameManager = new NameManager();
    }
}
