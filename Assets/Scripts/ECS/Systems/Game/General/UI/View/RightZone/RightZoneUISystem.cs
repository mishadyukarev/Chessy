using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class RightZoneUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorWorker.GetXy(SelectorCellTypes.Selected);

    public void Run()
    {
        if (SelectorWorker.IsSelectedCell)
        {
            if (CellUnitsDataWorker.IsVisibleUnit(PhotonNetwork.IsMasterClient, XySelectedCell))
            {
                if (CellUnitsDataWorker.HaveAnyUnit(XySelectedCell))
                {
                    if (CellUnitsDataWorker.HaveOwner(XySelectedCell))
                    {
                        UIRightWorker.SetActiveRightZoneGO(true);
                    }
                    else if (CellUnitsDataWorker.IsBot(XySelectedCell))
                    {
                        UIRightWorker.SetActiveRightZoneGO(true);
                    }
                }
                else UIRightWorker.SetActiveRightZoneGO(false);
            }
            else
            {
                UIRightWorker.SetActiveRightZoneGO(false);
            }
        }
        else
        {
            UIRightWorker.SetActiveRightZoneGO(false);
        }
    }
}
