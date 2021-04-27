using UnityEngine;

internal sealed class SupportGameManager : SupportManager
{
    private CellManager _cellManager;
    private ResourcesLoadGameManager _resourcesLoadManager;
    private StartValuesGameConfig _startValuesGameConfig;
    private StartSpawnGameManager _startSpawnGameManager;

    public CellManager CellManager => _cellManager;
    public ResourcesLoadGameManager ResourcesLoadGameManager => _resourcesLoadManager;
    internal StartValuesGameConfig StartValuesGameConfig => _startValuesGameConfig;
    internal StartSpawnGameManager StartSpawnGameManager => _startSpawnGameManager;


    public SupportGameManager(out Transform parentTransformScripts)
    {
        _resourcesLoadManager = new ResourcesLoadGameManager();
        _cellManager = new CellManager();


        _startValuesGameConfig = _resourcesLoadManager.StartValuesConfig;
        _startSpawnGameManager = new StartSpawnGameManager(_resourcesLoadManager, _builderManager,_startValuesGameConfig, out parentTransformScripts);
    }
}
