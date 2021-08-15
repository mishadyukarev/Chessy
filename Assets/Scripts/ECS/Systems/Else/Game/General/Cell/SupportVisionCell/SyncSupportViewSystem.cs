using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SyncSupportViewSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellDataComponent> _cellDataFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent, CellUnitMainViewComp> _cellUnitFilter = default;
    private EcsFilter<CellPawnDataComp> _cellPawnFilter = default;
    private EcsFilter<CellSupViewComponent> _cellSupViewFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvFilter = default;
    private EcsFilter<CellFireDataComponent> _cellFireFilter = default;

    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<IdxAvailableCellsComponent> _idxAvailCellsFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);


        foreach (var idxCurCell in _xyCellFilter)
        {
            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curCellPawnDataComp =ref _cellPawnFilter.Get1(idxCurCell);
            ref var curOwnerCellUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
            ref var curCellUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);
            ref var curCellDataCom = ref _cellDataFilter.Get1(idxCurCell);
            ref var curCellSupViewCom = ref _cellSupViewFilter.Get1(idxCurCell);

            if (selCom.IsSelectedUnit)
            {
                if (curCellUnitDataCom.HaveUnit) { }

                else
                {
                    if (curCellDataCom.IsStartedCell(PhotonNetwork.IsMasterClient))
                    {
                        curCellSupViewCom.EnableSR();
                        curCellSupViewCom.SetColor(SupportVisionTypes.Spawn);
                    }
                }
            }

            else if (selCom.IsSelectedCell)
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


            if (selCom.CellClickType == CellClickTypes.PickFire)
            {
                foreach (var xy1 in CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(selCom.IdxSelectedCell)))
                {
                    var curIdxCell = _xyCellFilter.GetIndexCell(xy1);

                    ref var idx1CellEnvDataCom = ref _cellEnvFilter.Get1(curIdxCell);
                    ref var idx1CellFireDataCom = ref _cellFireFilter.Get1(curIdxCell);
                    ref var idx1CellSupViewCom = ref _cellSupViewFilter.Get1(curIdxCell);

                    if (idx1CellEnvDataCom.HaveEnvironment(EnvironmentTypes.AdultForest))
                    {
                        if (!idx1CellFireDataCom.HaveFire)
                        {
                            idx1CellSupViewCom.EnableSR();
                            idx1CellSupViewCom.SetColor(SupportVisionTypes.FireSelector);
                        }
                    }
                }
            }

            else if (selCom.CellClickType == CellClickTypes.UpgradeUnit)
            {
                if (curCellUnitDataCom.IsUnitType(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop }))
                {
                    if (curOwnerCellUnitCom.HaveOwner)
                    {
                        if (curOwnerCellUnitCom.IsMine)
                        {
                            curCellSupViewCom.EnableSR();
                            curCellSupViewCom.SetColor(SupportVisionTypes.Upgrade);
                        }
                    }
                }
            }


            else if (curCellUnitDataCom.IsUnitType(UnitTypes.Pawn))
            {
                if (curOwnerCellUnitCom.HaveOwner)
                {
                    if (curOwnerCellUnitCom.IsMine)
                    {
                        if (selCom.IsCellClickType(CellClickTypes.GiveExtraThing))
                        {
                            if (!curCellPawnDataComp.HaveExtraTool)
                            {
                                curCellSupViewCom.EnableSR();
                                curCellSupViewCom.SetColor(SupportVisionTypes.GivePawnTool);
                            }
                        }

                        else if (selCom.IsCellClickType(CellClickTypes.TakeExtraThing))
                        {
                            if (curCellPawnDataComp.HaveExtraTool)
                            {
                                curCellSupViewCom.EnableSR();
                                curCellSupViewCom.SetColor(SupportVisionTypes.TakePawnTool);
                            }
                        }
                    }
                }
            }
        }
        


        foreach (var curIdxCell in _idxAvailCellsFilter.Get1(0).GetAllCellsCopy(AvailableCellTypes.Shift))
        {
            _cellSupViewFilter.Get1(curIdxCell).EnableSR();
            _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Shift);
        }

        foreach (var curIdxCell in _idxAvailCellsFilter.Get1(0).GetAllCellsCopy(AvailableCellTypes.SimpleAttack))
        {
            _cellSupViewFilter.Get1(curIdxCell).EnableSR();
            _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.SimpleAttack);
        }

        foreach (var curIdxCell in _idxAvailCellsFilter.Get1(0).GetAllCellsCopy(AvailableCellTypes.UniqueAttack))
        {
            _cellSupViewFilter.Get1(curIdxCell).EnableSR();
            _cellSupViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.UniqueAttack);
        }
    }
}
