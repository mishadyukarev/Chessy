using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncSupportViewSystem : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, OwnerCom, CellUnitMainViewCom> _cellUnitFilter = default;
        private EcsFilter<CellSupViewComponent> _supViewFilter = default;

        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;
        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;

        public void Run()
        {
            foreach (var idxCurCell in _xyCellFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curOnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);
                ref var curUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);

                ref var curSupViewCom = ref _supViewFilter.Get1(idxCurCell);

                curSupViewCom.DisableSR();

                if (SelectorC.IsSelCell)
                {
                    if (SelectorC.IdxSelCell == idxCurCell)
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
                                if (SelectorC.Is(CellClickTypes.GiveTakeTW) 
                                    || SelectorC.Is(CellClickTypes.OldToNewUnit))
                                {
                                    curSupViewCom.EnableSR();
                                    curSupViewCom.SetColor(SupVisTypes.GivePawnTool);
                                }

                            }

                            if (curUnitDatCom.LevelUnitType == LevelUnitTypes.Wood)
                            {
                                if (SelectorC.Is(CellClickTypes.UpgradeUnit))
                                {
                                    curSupViewCom.EnableSR();
                                    curSupViewCom.SetColor(SupVisTypes.GivePawnTool);
                                }
                            }
                        }
                    }
                }
            }


            if (SelectorC.IsSelCell)
            {
                ref var selUnitDatCom = ref _cellUnitFilter.Get1(SelectorC.IdxSelCell);
                ref var selOffUnitCom = ref _cellUnitFilter.Get3(SelectorC.IdxSelCell);


                if (selUnitDatCom.HaveUnit)
                {
                    if (_cellUnitFilter.Get2(SelectorC.IdxSelCell).IsMine)
                    {
                        if (SelectorC.Is(CellClickTypes.PickFire))
                        {
                            foreach (var curIdxCell in _cellsArcherArsonFilt.Get1(0).GetListCopy(WhoseMoveC.CurPlayer, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.FireSelector);
                            }
                        }

                        else if (SelectorC.Is(CellClickTypes.None))
                        {
                            foreach (var curIdxCell in CellsForShiftCom.GetListCopy(WhoseMoveC.CurPlayer, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.Shift);
                            }

                            foreach (var curIdxCell in CellsAttackC.GetListCopy(WhoseMoveC.CurPlayer, AttackTypes.Simple, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.SimpleAttack);
                            }

                            foreach (var curIdxCell in CellsAttackC.GetListCopy(WhoseMoveC.CurPlayer, AttackTypes.Unique, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.UniqueAttack);
                            }
                        }
                    }
                }
            }
            if (SelectorC.IsSelUnit)
            {
                ref var cellsSetUnitCom = ref _cellsSetUnitFilter.Get1(0);

                foreach (var curIdxCell in cellsSetUnitCom.GetListCells(WhoseMoveC.CurPlayer))
                {
                    _supViewFilter.Get1(curIdxCell).EnableSR();
                    _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.Spawn);
                }
            }
        }
    }
}