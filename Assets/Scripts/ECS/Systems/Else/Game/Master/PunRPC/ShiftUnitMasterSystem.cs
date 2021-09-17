using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForShiftMasCom> _forShiftFilter = default;

    private EcsFilter<AvailCellsForShiftComp> _cellsShiftFilter = default;

    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerOfflineCom> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvrDataFilter = default;

    private EcsFilter<WhoseMoveCom> _whoseMoveFilter = default;
    private EcsFilter<SoundEffectsComp> _soundEffFilter = default;

    public void Run()
    {
        var fromInfo = _infoFilter.Get1(0).FromInfo;

        var fromIdx = _forShiftFilter.Get1(0).IdxFrom;
        var toIdx = _forShiftFilter.Get1(0).IdxTo;



        var isMaster = false;

        if (PhotonNetwork.OfflineMode) isMaster = _whoseMoveFilter.Get1(0).IsMainMove;

        else isMaster = fromInfo.Sender.IsMasterClient;


        if (_cellsShiftFilter.Get1(0).HaveIdxCell(isMaster, fromIdx, toIdx))
        {
            ref var forShiftMasCom = ref _forShiftFilter.Get1(0);

            ref var fromUnitDatCom = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromOnlineUnitCom = ref _cellUnitFilter.Get2(fromIdx);

            ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdx);
            ref var toOnlineUnitCom = ref _cellUnitFilter.Get2(toIdx);
            ref var toOfflineUnitCom = ref _cellUnitFilter.Get3(toIdx);

            ref var toCellEnvDataCom = ref _cellEnvrDataFilter.Get1(toIdx);


            fromUnitDatCom.TakeAmountSteps(toCellEnvDataCom.NeedAmountSteps);
            if (fromUnitDatCom.AmountSteps < 0) fromUnitDatCom.ResetAmountSteps();


            toUnitDatCom.ArcherWeaponType = fromUnitDatCom.ArcherWeaponType;
            toUnitDatCom.ExtraTWPawnType = fromUnitDatCom.ExtraTWPawnType;
            toUnitDatCom.UnitType = fromUnitDatCom.UnitType;
            toUnitDatCom.AmountHealth = fromUnitDatCom.AmountHealth;
            toUnitDatCom.AmountSteps = fromUnitDatCom.AmountSteps;
            toUnitDatCom.ConditionUnitType = default;

            if (PhotonNetwork.OfflineMode)
            {
                if (isMaster) toOfflineUnitCom.LocalPlayerType = PlayerTypes.First;
                else toOfflineUnitCom.LocalPlayerType = PlayerTypes.Second;

                _soundEffFilter.Get1(0).Play(SoundEffectTypes.ClickToTable);
            }

            else
            {
                toOnlineUnitCom.SetOwner(fromInfo.Sender);

                RpcSys.SoundToGeneral(fromInfo.Sender, SoundEffectTypes.ClickToTable);
            }


            fromUnitDatCom.ResetUnitType();

            
        }
    }
}
