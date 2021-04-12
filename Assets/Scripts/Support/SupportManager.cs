using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SupportManager
{
    private BuilderManager _builderManager;
    private NameValueManager _nameValueManager;
    private CellManager _cellManager;
    private ResourcesLoadManager _resourcesLoadManager;
    private UnityEvents _eventDelegateManager;
    private SoundManager _soundManager;


    public BuilderManager BuilderManager => _builderManager;
    public NameValueManager NameValueManager => _nameValueManager;
    public CellManager CellManager => _cellManager;
    public ResourcesLoadManager ResourcesLoadManager => _resourcesLoadManager;
    public UnityEvents DelegateEventManager => _eventDelegateManager;
    internal SoundManager SoundManager => _soundManager;



    public SupportManager(int percentTree, int percentHill, int percentMountain, int cellCountX, int cellCountY)
    {
        _nameValueManager = new NameValueManager(percentTree, percentHill, percentMountain, cellCountX, cellCountY);
        _builderManager = new BuilderManager();
        _cellManager = new CellManager(_nameValueManager);
        _resourcesLoadManager = new ResourcesLoadManager();
        _eventDelegateManager = new UnityEvents(this);
        _soundManager = new SoundManager(this);
    }
}
