using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class RightZoneUISystem : IEcsRunSystem
{
    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<UnitZoneViewUICom> _unitZoneFilter = default;
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
                        _unitZoneFilter.Get1(0).SetActiveParentZone(true);
                    }
                    else if (CellUnitsDataSystem.IsBot(XySelectedCell))
                    {
                        _unitZoneFilter.Get1(0).SetActiveParentZone(true);
                    }
                }
                else _unitZoneFilter.Get1(0).SetActiveParentZone(false);
            }
            else
            {
                _unitZoneFilter.Get1(0).SetActiveParentZone(false);
            }
        }
        else
        {
            _unitZoneFilter.Get1(0).SetActiveParentZone(false);
        }
    }
}
