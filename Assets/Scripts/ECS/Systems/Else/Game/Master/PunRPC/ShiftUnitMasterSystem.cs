using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.Supports;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class ShiftUnitMasterSystem : IEcsRunSystem
{
    private EcsFilter<InfoMasCom> _infoFilter = default;
    private EcsFilter<ForShiftMasCom> _forShiftFilter = default;

    private EcsFilter<AvailCellsForShiftComp> _cellsShiftFilter = default;

    private EcsFilter<CellUnitDataCom, OwnerCom> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvrDataFilter = default;

    public void Run()
    {
        var fromInfo = _infoFilter.Get1(0).FromInfo;

        var fromIdx = _forShiftFilter.Get1(0).IdxFrom;
        var toIdx = _forShiftFilter.Get1(0).IdxTo;



        PlayerTypes playerType = default;
        if (PhotonNetwork.OfflineMode) playerType = WhoseMoveCom.CurOfflinePlayer;
        else playerType = fromInfo.Sender.GetPlayerType();


        if (_cellsShiftFilter.Get1(0).HaveIdxCell(playerType, fromIdx, toIdx))
        {
            ref var forShiftMasCom = ref _forShiftFilter.Get1(0);

            ref var fromUnitDatCom = ref _cellUnitFilter.Get1(fromIdx);
            ref var fromOnlineUnitCom = ref _cellUnitFilter.Get2(fromIdx);

            ref var toUnitDatCom = ref _cellUnitFilter.Get1(toIdx);
            ref var toOwnUnitCom = ref _cellUnitFilter.Get2(toIdx);
            ref var toEnvDatCom = ref _cellEnvrDataFilter.Get1(toIdx);


            fromUnitDatCom.TakeAmountSteps(toEnvDatCom.NeedAmountSteps);
            if (fromUnitDatCom.AmountSteps < 0) fromUnitDatCom.ResetAmountSteps();


            toUnitDatCom.ArcherWeapType = fromUnitDatCom.ArcherWeapType;
            toUnitDatCom.ExtraTWPawnType = fromUnitDatCom.ExtraTWPawnType;
            toUnitDatCom.UnitType = fromUnitDatCom.UnitType;
            toUnitDatCom.AmountHealth = fromUnitDatCom.AmountHealth;
            toUnitDatCom.AmountSteps = fromUnitDatCom.AmountSteps;
            toUnitDatCom.CondUnitType = default;
            toOwnUnitCom.PlayerType = playerType;

            fromUnitDatCom.DefUnitType();

            RpcSys.SoundToGeneral(fromInfo.Sender, SoundEffectTypes.ClickToTable);
        }
    }
}
