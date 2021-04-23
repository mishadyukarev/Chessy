internal sealed class SupportGameManager : SupportManager
{
    private CellManager _cellManager;
    private ResourcesLoadGameManager _resourcesLoadManager;
    private StartValuesGameConfig _startValues;

    public CellManager CellManager => _cellManager;
    public ResourcesLoadGameManager ResourcesLoadGameManager => _resourcesLoadManager;
    internal StartValuesGameConfig StartValuesGameConfig => _startValues;


    public SupportGameManager()
    {
        _resourcesLoadManager = new ResourcesLoadGameManager();
        _startValues = _resourcesLoadManager.StartValuesConfig;
        _cellManager = new CellManager(_startValues);
    }
}
