using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class RightZoneUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter;
    private int[] XySelectedCell => _selectorFilter.Get1(0).XySelectedCell;

    public void Run()
    {
        if (_selectorFilter.Get1(0).IsSelectedCell)
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
