public class SupportManager
{
    private BuilderManager _builderManager;
    private CellManager _cellManager;
    private ResourcesLoadManager _resourcesLoadManager;
    private UnityEvents _unityEvents;
    private NameManager _nameManager;
    private StartValuesConfig _startValues;



    public BuilderManager BuilderManager => _builderManager;
    public CellManager CellManager => _cellManager;
    public ResourcesLoadManager ResourcesLoadManager => _resourcesLoadManager;
    public UnityEvents UnityEvents => _unityEvents;
    internal NameManager NameManager => _nameManager;
    internal StartValuesConfig StartValuesConfig => _startValues;


    public SupportManager()
    {
        _resourcesLoadManager = new ResourcesLoadManager();
        _startValues = _resourcesLoadManager.StartValuesConfig;
        _builderManager = new BuilderManager();
        _cellManager = new CellManager(_startValues);
        _unityEvents = new UnityEvents(_builderManager);
        _nameManager = new NameManager();
    }
}
