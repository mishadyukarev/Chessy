using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Leopotam.Ecs;

internal sealed class SyncSupportViewSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerCom, CellUnitMainViewComp> _cellUnitFilter = default;
    private EcsFilter<CellSupViewComponent> _supViewFilter = default;

    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;
    private EcsFilter<AvailCellsForShiftComp> _cellsShiftFilter = default;
    private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;
    private EcsFilter<CellsForAttackCom> _cellsSimpleFilter = default;

    public void Run()
    {
        ref var selCom = ref _selectorFilter.Get1(0);


        foreach (var idxCurCell in _xyCellFilter)
        {
            ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
            ref var curOnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
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
                    if (curOnUnitCom.IsMine)
                    {
                        if (curUnitDatCom.IsUnit(UnitTypes.Pawn))
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


        if (selCom.IsSelectedCell)
        {
            ref var selUnitDatCom = ref _cellUnitFilter.Get1(selCom.IdxSelCell);
            ref var selOffUnitCom = ref _cellUnitFilter.Get3(selCom.IdxSelCell);

            ref var cellsShiftCom = ref _cellsShiftFilter.Get1(0);


            if (selUnitDatCom.HaveUnit)
            {
                if (_cellUnitFilter.Get2(selCom.IdxSelCell).IsMine)
                {
                    if (selCom.IsCellClickType(CellClickTypes.PickFire))
                    {
                        foreach (var curIdxCell in _cellsArcherArsonFilt.Get1(0).GetListCopy(WhoseMoveCom.CurPlayer, selCom.IdxSelCell))
                        {
                            _supViewFilter.Get1(curIdxCell).EnableSR();
                            _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.FireSelector);
                        }
                    }

                    else if (selCom.IsCellClickType(CellClickTypes.None))
                    {
                        foreach (var curIdxCell in cellsShiftCom.GetListCopy(WhoseMoveCom.CurPlayer, selCom.IdxSelCell))
                        {
                            _supViewFilter.Get1(curIdxCell).EnableSR();
                            _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Shift);
                        }

                        foreach (var curIdxCell in _cellsSimpleFilter.Get1(0).GetListCopy(WhoseMoveCom.CurPlayer, AttackTypes.Simple, selCom.IdxSelCell))
                        {
                            _supViewFilter.Get1(curIdxCell).EnableSR();
                            _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.SimpleAttack);
                        }

                        foreach (var curIdxCell in _cellsSimpleFilter.Get1(0).GetListCopy(WhoseMoveCom.CurPlayer, AttackTypes.Unique, selCom.IdxSelCell))
                        {
                            _supViewFilter.Get1(curIdxCell).EnableSR();
                            _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.UniqueAttack);
                        }
                    }
                }
            }
        }
        if (selCom.IsSelectedUnit)
        {
            ref var cellsSetUnitCom = ref _cellsSetUnitFilter.Get1(0);

            foreach (var curIdxCell in cellsSetUnitCom.GetListCells(WhoseMoveCom.CurPlayer))
            {
                _supViewFilter.Get1(curIdxCell).EnableSR();
                _supViewFilter.Get1(curIdxCell).SetColor(SupportVisionTypes.Spawn);
            }
        }
    }
}
