using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using static Assets.Scripts.Main;

internal sealed class RightZoneUISystem : SystemGeneralReduction
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public override void Run()
    {
        base.Run();

        if (_eGM.CellUnitEnt_ActivatedForPlayersCom(XySelectedCell).IsActivated(Instance.IsMasterClient))
        {
            if (CellUnitWorker.HaveAnyUnit(XySelectedCell))
            {
                if (CellUnitWorker.HaveOwner(XySelectedCell))
                {
                    _eGGUIM.RightZoneEnt_ParentCom.SetActive(true);
                }
                else if (_eGM.CellUnitEnt_CellOwnerBotCom(XySelectedCell).IsBot)
                {
                    _eGGUIM.RightZoneEnt_ParentCom.SetActive(true);
                }
            }
            else _eGGUIM.RightZoneEnt_ParentCom.SetActive(false);
        }
        else
        {
            _eGGUIM.RightZoneEnt_ParentCom.SetActive(false);
        }
    }
}
