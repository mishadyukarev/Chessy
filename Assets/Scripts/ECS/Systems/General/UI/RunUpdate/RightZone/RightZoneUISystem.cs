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

        if (CellUnitsDataWorker.IsVisibleUnit(Instance.IsMasterClient, XySelectedCell))
        {
            if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
            {
                if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
                {
                    _eGGUIM.RightZoneEnt_ParentCom.ParentGO.SetActive(true);
                }
                else if (CellUnitsDataWorker.IsBot(XySelectedCell))
                {
                    _eGGUIM.RightZoneEnt_ParentCom.ParentGO.SetActive(true);
                }
            }
            else _eGGUIM.RightZoneEnt_ParentCom.ParentGO.SetActive(false);
        }
        else
        {
            _eGGUIM.RightZoneEnt_ParentCom.ParentGO.SetActive(false);
        }
    }
}
