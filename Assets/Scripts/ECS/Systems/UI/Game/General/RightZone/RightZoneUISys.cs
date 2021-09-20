using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class RightZoneUISys : IEcsRunSystem
{
    private EcsFilter<SelectorCom> _selFilt = default;
    private EcsFilter<StatZoneViewUICom> _unitZoneFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, OwnerBotComponent> _cellUnitFilter = default;


    public void Run()
    {
        var idxSelCell = _selFilt.Get1(0).IdxSelCell;
        ref var unitZoneViewCom = ref _unitZoneFilter.Get1(0);

        ref var selUnitDatCom = ref _cellUnitFilter.Get1(idxSelCell);

        var activeParent = false;


        if (_selFilt.Get1(0).IsSelectedCell)
        {
            var isMaster = false;
            if (PhotonNetwork.OfflineMode) isMaster = WhoseMoveCom.IsMainMove;
            else isMaster = PhotonNetwork.IsMasterClient;

            if (selUnitDatCom.HaveUnit)
            {
                if (selUnitDatCom.IsVisibleUnit(isMaster))
                {
                    activeParent = true;
                }
            }
        }

        unitZoneViewCom.SetActiveParentZone(activeParent);
    }
}
