using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SyncSupportViewSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellDataComponent> _cellDataFilter = default;
    private EcsFilter<CellUnitDataComponent, CellUnitViewComponent> _cellUnitFilter = default;
    private EcsFilter<CellSupViewComponent> _cellSupViewFilter = default;

    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<IdxAvailableCellsComponent> _idxAvailCellsFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);


        foreach (var idxCurCell in _xyCellFilter)
        {
            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curCellUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);
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


                    //if (selCom.SelectorType == SelectorTypes.PickFire)
                    //{
                    //    foreach (var xy1 in CellSpaceSupport.TryGetXyAround(selCom.XySelectedCell))
                    //    {
                    //        if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                    //        {
                    //            if (!CellFireDataSystem.HaveFireCom(xy1).HaveFire)
                    //            {
                    //                CellSupViewSystem.EnableSupVis(SupportVisionTypes.FireSelector, xy1);
                    //            }
                    //        }
                    //    }
                    //}
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


        if (selCom.CellClickType == CellClickTypes.UpgradeUnit)
        {
            //foreach (var xy in xyUnitsCom.GetLixtXyUnits(UnitTypes.Pawn, PhotonNetwork.IsMasterClient))
            //{
            //    if (CellUnitsDataSystem.HaveOwner(xy))
            //    {
            //        if (CellUnitsDataSystem.IsMine(xy))
            //        {
            //            CellSupViewSystem.EnableSupVis(SupportVisionTypes.Upgrade, xy);
            //        }
            //    }
            //}
            //foreach (var xy in xyUnitsCom.GetLixtXyUnits(UnitTypes.Rook, PhotonNetwork.IsMasterClient))
            //{
            //    if (CellUnitsDataSystem.HaveOwner(xy))
            //    {
            //        if (CellUnitsDataSystem.IsMine(xy))
            //        {
            //            CellSupViewSystem.EnableSupVis(SupportVisionTypes.Upgrade, xy);
            //        }
            //    }
            //}
            //foreach (var xy in xyUnitsCom.GetLixtXyUnits(UnitTypes.Bishop, PhotonNetwork.IsMasterClient))
            //{
            //    if (CellUnitsDataSystem.HaveOwner(xy))
            //    {
            //        if (CellUnitsDataSystem.IsMine(xy))
            //        {
            //            CellSupViewSystem.EnableSupVis(SupportVisionTypes.Upgrade, xy);
            //        }
            //    }
            //}
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
