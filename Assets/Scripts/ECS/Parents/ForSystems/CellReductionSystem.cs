using Leopotam.Ecs;
using static MainGame;

internal abstract class CellGeneralReduction : SystemGeneralReduction
{
    protected CellBaseOperations _cellBaseOperations = default;
    protected CellFinderWay _cellFinderWay = default;

    internal CellGeneralReduction(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _cellBaseOperations = InstanceGame.CellManager.CellBaseOperations;
        _cellFinderWay = InstanceGame.CellManager.CellFinderWay;
    }
}
