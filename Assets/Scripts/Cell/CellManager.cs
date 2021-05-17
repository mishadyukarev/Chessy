internal class CellManager
{
    private CellBaseOperations _cellBaseOperations;
    private CellFinderWay _cellFinderWay;

    public CellBaseOperations CellBaseOperations => _cellBaseOperations;
    internal CellFinderWay CellFinderWay => _cellFinderWay;


    internal CellManager(StartValuesGameConfig startValuesGameConfig)
    {
        _cellBaseOperations = new CellBaseOperations(startValuesGameConfig);
        _cellFinderWay = new CellFinderWay(startValuesGameConfig, _cellBaseOperations);
    }
}
