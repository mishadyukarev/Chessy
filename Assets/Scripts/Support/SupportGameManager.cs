internal sealed class SupportGameManager : SupportManager
{
    private CellManager _cellManager = new CellManager();
    private ResourcesLoadGame _resourcesLoadManager = new ResourcesLoadGame();
    private CellFinderWay _finderWay = new CellFinderWay();

    public CellManager CellManager => _cellManager;
    public ResourcesLoadGame ResourcesLoadGameManager => _resourcesLoadManager;
    internal CellFinderWay CellFinderWay => _finderWay;
}
