using Leopotam.Ecs;

namespace Scripts.Game
{
    internal sealed class SyncSupportViewSystem : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom, LevelUnitC, OwnerCom> _cellUnitFilter = default;
        private EcsFilter<CellUnitDataCom, CellUnitMainViewCom> _cellUnitViewFilt = default;
        private EcsFilter<CellSupViewComponent> _supViewFilter = default;

        private EcsFilter<CellsForSetUnitComp> _cellsSetUnitFilter = default;
        private EcsFilter<CellsArsonArcherComp> _cellsArcherArsonFilt = default;

        public void Run()
        {
            foreach (var idx_0 in _xyCellFilter)
            {
                ref var unitC_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levUnitC_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var ownUnitC_0 = ref _cellUnitFilter.Get3(idx_0);

                ref var mainUnitC_0 = ref _cellUnitViewFilt.Get2(idx_0);

                ref var supViewC_0 = ref _supViewFilter.Get1(idx_0);

                supViewC_0.DisableSR();

                if (SelectorC.IsSelCell)
                {
                    if (SelectorC.IdxSelCell == idx_0)
                    {
                        supViewC_0.EnableSR();
                        supViewC_0.SetColor(SupVisTypes.Selector);
                    }
                }
                if (unitC_0.HaveUnit)
                {
                    if (ownUnitC_0.IsMine)
                    {
                        if (unitC_0.Is(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop}))
                        {
                            if (unitC_0.Is(UnitTypes.Pawn))
                            {
                                if (SelectorC.Is(CellClickTypes.GiveTakeTW) 
                                    || SelectorC.Is(CellClickTypes.OldToNewUnit))
                                {
                                    supViewC_0.EnableSR();
                                    supViewC_0.SetColor(SupVisTypes.GivePawnTool);
                                }

                            }

                            if (levUnitC_0.Is(LevelUnitTypes.Wood))
                            {
                                if (SelectorC.Is(CellClickTypes.UpgradeUnit))
                                {
                                    supViewC_0.EnableSR();
                                    supViewC_0.SetColor(SupVisTypes.GivePawnTool);
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
                    if (_cellUnitFilter.Get3(SelectorC.IdxSelCell).IsMine)
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