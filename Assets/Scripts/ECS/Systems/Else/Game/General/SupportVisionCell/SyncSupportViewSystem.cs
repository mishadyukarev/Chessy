using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SyncSupportViewSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, CellUnitMainViewComp> _cellUnitFilter = default;
    private EcsFilter<CellSupViewComponent> _supViewFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<CellsForSetUnitComp> _availCellsForSetUnitFilter = default;
    private EcsFilter<AvailCellsForShiftComp> _cellsShiftFilter = default;
    private EcsFilter<CellsArsonArcherComp> _availCellsForArcherArsonFilter = default;
    private EcsFilter<AvailCellsForAttackComp> _availCellsForSimpleAttackFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);


        foreach (var idxCurCell in _xyCellFilter)
        {
            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
            ref var curUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);
            ref var curSupViewCom = ref _supViewFilter.Get1(idxCurCell);

            curSupViewCom.DisableSR();

            if (selCom.IsSelectedCell)
            {
                if (selCom.IdxSelectedCell == idxCurCell)
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
                            if (curUnitDatCom.IsUnitType(UnitTypes.Pawn))
                            {
                                curSupViewCom.EnableSR();
                                curSupViewCom.SetColor(SupportVisionTypes.GivePawnTool);
                            }

                            else if (curUnitDatCom.IsUnitType(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
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
            if (_cellUnitFilter.Get1(selCom.IdxSelectedCell).HaveUnit)
            {
                if (_cellUnitFilter.Get2(selCom.IdxSelectedCell).HaveOwner)
                {
                    if (_cellUnitFilter.Get2(selCom.IdxSelectedCell).IsMine)
                    {


                        if (selCom.IsCellClickType(CellClickTypes.PickFire))
                        {
                            foreach (var curIdxCell in _availCellsForArcherArsonFilter.Get1(0).GetListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.FireSelector);
                            }
                        }

                        else if(selCom.IsCellClickType(CellClickTypes.None))
                        {
                            foreach (var curIdxCell in _cellsShiftFilter.Get1(0).GetListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Shift);
                            }

                            foreach (var curIdxCell in _availCellsForSimpleAttackFilter.Get1(0).GetListCopy(AttackTypes.Simple, PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.SimpleAttack);
                            }

                            foreach (var curIdxCell in _availCellsForSimpleAttackFilter.Get1(0).GetListCopy(AttackTypes.Unique, PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.UniqueAttack);
                            }
                        }




                    }
                }
            }
        }
        if (selCom.IsSelectedUnit)
        {
            foreach (var curIdxCell in _availCellsForSetUnitFilter.Get1(0).GetListAvailCellsCopy(PhotonNetwork.IsMasterClient))
            {
                _supViewFilter.Get1(curIdxCell).EnableSR();
                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Spawn);
            }
        }
    }
}
