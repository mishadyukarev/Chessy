using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncSupportViewSystem : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom, CellUnitMainViewCom> _cellUnitFilter = default;
        private EcsFilter<CellSupViewComponent> _supViewFilter = default;

        private EcsFilter<SelectorCom> _selectorFilter = default;
        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;
        private EcsFilter<CellsForShiftCom> _cellsShiftFilter = default;
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

                if (selCom.IsSelCell)
                {
                    if (selCom.IdxSelCell == idxCurCell)
                    {
                        curSupViewCom.EnableSR();
                        curSupViewCom.SetColor(SupVisTypes.Selector);
                    }
                }
                if (curUnitDatCom.HaveUnit)
                {
                    if (curOnUnitCom.IsMine)
                    {
                        if (curUnitDatCom.Is(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop}))
                        {
                            if (curUnitDatCom.Is(UnitTypes.Pawn))
                            {
                                if (selCom.IsCellClickType(CellClickTypes.GiveTakeTW) 
                                    || selCom.IsCellClickType(CellClickTypes.OldToNewUnit))
                                {
                                    curSupViewCom.EnableSR();
                                    curSupViewCom.SetColor(SupVisTypes.GivePawnTool);
                                }

                            }

                            if (curUnitDatCom.LevelUnitType == LevelUnitTypes.Wood)
                            {
                                if (selCom.IsCellClickType(CellClickTypes.UpgradeUnit))
                                {
                                    curSupViewCom.EnableSR();
                                    curSupViewCom.SetColor(SupVisTypes.GivePawnTool);
                                }
                            }
                        }
                    }
                }
            }


            if (selCom.IsSelCell)
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
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.FireSelector);
                            }
                        }

                        else if (selCom.IsCellClickType(CellClickTypes.None))
                        {
                            foreach (var curIdxCell in cellsShiftCom.GetListCopy(WhoseMoveCom.CurPlayer, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.Shift);
                            }

                            foreach (var curIdxCell in _cellsSimpleFilter.Get1(0).GetListCopy(WhoseMoveCom.CurPlayer, AttackTypes.Simple, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.SimpleAttack);
                            }

                            foreach (var curIdxCell in _cellsSimpleFilter.Get1(0).GetListCopy(WhoseMoveCom.CurPlayer, AttackTypes.Unique, selCom.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.UniqueAttack);
                            }
                        }
                    }
                }
            }
            if (selCom.IsSelUnit)
            {
                ref var cellsSetUnitCom = ref _cellsSetUnitFilter.Get1(0);

                foreach (var curIdxCell in cellsSetUnitCom.GetListCells(WhoseMoveCom.CurPlayer))
                {
                    _supViewFilter.Get1(curIdxCell).EnableSR();
                    _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.Spawn);
                }
            }
        }
    }
}