internal sealed class RightZoneUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit && _eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).IsMine)
        {
            _eGM.RightZoneEnt_ParentCom.SetActive(true);
        }
        else _eGM.RightZoneEnt_ParentCom.SetActive(false);
    }
}
