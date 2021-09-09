using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SyncSupportViewSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent, CellUnitMainViewComp> _cellUnitFilter = default;
    private EcsFilter<CellSupViewComponent> _cellSupViewFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<AvailCellsForSetUnitComp> _availCellsForSetUnitFilter = default;
    private EcsFilter<AvailCellsForShiftComp> _availCellsForShiftUnitFilter = default;
    private EcsFilter<AvailCellsForArcherArsonComp> _availCellsForArcherArsonFilter = default;
    private EcsFilter<AvailCellsForAttackComp> _availCellsForSimpleAttackFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);


        foreach (var idxCurCell in _xyCellFilter)
        {
            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
            ref var curCellUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);
            ref var curCellSupViewCom = ref _cellSupViewFilter.Get1(idxCurCell);

            curCellSupViewCom.DisableSR();

            if (selCom.IsSelectedCell)
            {
                if (selCom.IdxSelectedCell == idxCurCell)
                {
                    curCellSupViewCom.EnableSR();
                    curCellSupViewCom.SetColor(SupportVisionTypes.Selector);
                }
                else
                {
                    curCellSupViewCom.DisableSR();
                }
            }

            else
            {
                curCellSupViewCom.DisableSR();
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
                            foreach (var curIdxCell in _availCellsForArcherArsonFilter.Get1(0).GetListCopy(PhotonNetwork.IsMasterClient))
                            {
                                _cellSupViewFilter.Get1(curIdxCell).EnableSR();
                                _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.FireSelector);
                            }
                        }

                        foreach (var curIdxCell in _availCellsForShiftUnitFilter.Get1(0).GetListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                        {
                            _cellSupViewFilter.Get1(curIdxCell).EnableSR();
                            _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Shift);
                        }

                        foreach (var curIdxCell in _availCellsForSimpleAttackFilter.Get1(0).GetSimpleListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                        {
                            _cellSupViewFilter.Get1(curIdxCell).EnableSR();
                            _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.SimpleAttack);
                        }

                        foreach (var curIdxCell in _availCellsForSimpleAttackFilter.Get1(0).GetUniqueListCopy(PhotonNetwork.IsMasterClient, selCom.IdxSelectedCell))
                        {
                            _cellSupViewFilter.Get1(curIdxCell).EnableSR();
                            _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.UniqueAttack);
                        }

                        //foreach (var curIdxCell in _availCellsForUniqueAttackFilter.Get1(0).GetListCopy(PhotonNetwork.IsMasterClient))
                        //{
                        //    _cellSupViewFilter.Get1(curIdxCell).EnableSR();
                        //    _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.UniqueAttack);
                        //}
                    }
                }
            }
        }
        if (selCom.IsSelectedUnit)
        {
            foreach (var curIdxCell in _availCellsForSetUnitFilter.Get1(0).GetListAvailCellsCopy(PhotonNetwork.IsMasterClient))
            {
                _cellSupViewFilter.Get1(curIdxCell).EnableSR();
                _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Spawn);
            }
        }
    }
}
