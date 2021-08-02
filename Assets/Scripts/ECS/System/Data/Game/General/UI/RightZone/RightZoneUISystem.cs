using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class RightZoneUISystem : IEcsRunSystem
{
    private int[] XySelectedCell => SelectorSystem.XySelectedCell;

    public void Run()
    {
        if (SelectorSystem.IsSelectedCell)
        {
            if (CellUnitsDataSystem.IsVisibleUnit(PhotonNetwork.IsMasterClient, XySelectedCell))
            {
                if (CellUnitsDataSystem.HaveAnyUnit(XySelectedCell))
                {
                    if (CellUnitsDataSystem.HaveOwner(XySelectedCell))
                    {
                        RightUIViewContainer.SetActiveRightZoneGO(true);
                    }
                    else if (CellUnitsDataSystem.IsBot(XySelectedCell))
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
