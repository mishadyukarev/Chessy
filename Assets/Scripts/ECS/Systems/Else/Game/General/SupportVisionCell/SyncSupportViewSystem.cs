using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SyncSupportViewSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerOfflineCom, CellUnitMainViewComp> _cellUnitFilter = default;
    private EcsFilter<CellSupViewComponent> _supViewFilter = default;

    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;
    private EcsFilter<AvailCellsForShiftComp> _cellsShiftFilter = default;
    private EcsFilter<CellsArsonArcherComp> _availCellsForArcherArsonFilter = default;
    private EcsFilter<AvailCellsForAttackComp> _cellsSimpleFilter = default;
    private EcsFilter<WhoseMoveCom> _whoseMoveFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);


        var isMainMove = WhoseMoveCom.IsMainMove;


        foreach (var idxCurCell in _xyCellFilter)
        {
            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
            ref var curUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);

            ref var curSupViewCom = ref _supViewFilter.Get1(idxCurCell);

            curSupViewCom.DisableSR();

            if (selCom.IsSelectedCell)
            {
                if (selCom.IdxSelCell == idxCurCell)
                {
                    curSupViewCom.EnableSR();
                    curSupViewCom.SetColor(SupportVisionTypes.Selector);
                }
            }

            if (selCom.IsCellClickType(CellClickTypes.GiveTakeTW))
            {
                if (curUnitDatCom.HaveUnit)
                {
                    if (curOwnUnitCom.HaveOwner)
                    {
                        if (curOwnUnitCom.IsMine)
                        {
                            if (curUnitDatCom.Is(UnitTypes.Pawn))
                            {
                                curSupViewCom.EnableSR();
                                curSupViewCom.SetColor(SupportVisionTypes.GivePawnTool);
                            }

                            else if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                            {
                                curSupViewCom.EnableSR();
                                curSupViewCom.SetColor(SupportVisionTypes.GivePawnTool);
                            }
                        }
                    }
                }
            }
        }


        if (selCom.IsSelectedCell)
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(selCom.IdxSelCell);
            ref var selOffUnitCom = ref _cellUnitFilter.Get3(selCom.IdxSelCell);

            ref var cellsShiftCom = ref _cellsShiftFilter.Get1(0);


            if (selUnitDatCom.HaveUnit)
            {
                if (_cellUnitFilter.Get2(selCom.IdxSelCell).HaveOwner)
                {
                    if (_cellUnitFilter.Get2(selCom.IdxSelCell).IsMine)
                    {
                        if (selCom.IsCellClickType(CellClickTypes.PickFire))
                        {
                            foreach (var curIdxCell in _availCellsForArcherArsonFilter.Get1(0).GetListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.FireSelector);
                            }
                        }

                        else if (selCom.IsCellClickType(CellClickTypes.None))
                        {
                            foreach (var curIdxCell in cellsShiftCom.GetListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Shift);
                            }

                            foreach (var curIdxCell in _cellsSimpleFilter.Get1(0).GetListCopy(AttackTypes.Simple, PhotonNetwork.IsMasterClient, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.SimpleAttack);
                            }

                            foreach (var curIdxCell in _cellsSimpleFilter.Get1(0).GetListCopy(AttackTypes.Unique, PhotonNetwork.IsMasterClient, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.UniqueAttack);
                            }
                        }
                    }
                }

                else if (selOffUnitCom.LocalPlayerType != default)
                {
                    if (selCom.IsCellClickType(CellClickTypes.None))
                    {
                        foreach (var curIdxCell in cellsShiftCom.GetListCopy(isMainMove, selCom.IdxSelCell))
                        {
                            _supViewFilter.Get1(curIdxCell).EnableSR();
                            _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Shift);
                        }
                    }
                }
            }
        }
        if (selCom.IsSelectedUnit)
        {
            ref var cellsSetUnitCom = ref _cellsSetUnitFilter.Get1(0);


            if (PhotonNetwork.OfflineMode)
            {
                foreach (var curIdxCell in cellsSetUnitCom.GetListCells(isMainMove))
                {
                    _supViewFilter.Get1(curIdxCell).EnableSR();
                    _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Spawn);
                }
            }

            else
            {
                foreach (var curIdxCell in cellsSetUnitCom.GetListCells(PhotonNetwork.IsMasterClient))
                {
                    _supViewFilter.Get1(curIdxCell).EnableSR();
                    _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Spawn);
                }
            }
        }
    }
}
