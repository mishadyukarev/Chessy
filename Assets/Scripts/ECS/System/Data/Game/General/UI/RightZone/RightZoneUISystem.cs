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
            if (CellUnitsDataContainer.IsVisibleUnit(PhotonNetwork.IsMasterClient, XySelectedCell))
            {
                if (CellUnitsDataContainer.HaveAnyUnit(XySelectedCell))
                {
                    if (CellUnitsDataContainer.HaveOwner(XySelectedCell))
                    {
                        RightUIViewContainer.SetActiveRightZoneGO(true);
                    }
                    else if (CellUnitsDataContainer.IsBot(XySelectedCell))
                    {
                        RightUIViewContainer.SetActiveRightZoneGO(true);
                    }
                }
                else RightUIViewContainer.SetActiveRightZoneGO(false);
            }
            else
            {
                RightUIViewContainer.SetActiveRightZoneGO(false);
            }
        }
        else
        {
            RightUIViewContainer.SetActiveRightZoneGO(false);
        }
    }
}
