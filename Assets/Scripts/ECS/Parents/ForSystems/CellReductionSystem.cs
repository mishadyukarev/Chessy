using static MainGame;

internal abstract class CellGeneralReduction : SystemGeneralReduction
{
    protected CellBaseOperations _cellBaseOperations = default;

    internal CellGeneralReduction(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _cellBaseOperations = Instance.CellBaseOperations;
    }
}
