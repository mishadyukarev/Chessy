using Assets.Scripts;
using static Assets.Scripts.Main;

internal sealed class RightZoneUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => _eGM.SelectorEnt_SelectorCom.XySelectedCell;

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_CellUnitCom(XySelectedCell).IsActivatedUnitDict[Instance.IsMasterClient])
        {
            if (_eGM.CellUnitEnt_UnitTypeCom(XySelectedCell).HaveUnit)
            {
                if (_eGM.CellUnitEnt_CellOwnerCom(XySelectedCell).HaveOwner)
                {
                    _eGM.RightZoneEnt_ParentCom.SetActive(true);
                }
                else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).HaveBot)
                {
                    _eGM.RightZoneEnt_ParentCom.SetActive(true);
                }
            }
            else _eGM.RightZoneEnt_ParentCom.SetActive(false);
        }
        else
        {
            _eGM.RightZoneEnt_ParentCom.SetActive(false);
        }
    }
}
