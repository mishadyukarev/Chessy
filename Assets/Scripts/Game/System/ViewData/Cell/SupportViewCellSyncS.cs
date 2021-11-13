using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SupportViewCellSyncS : IEcsRunSystem
    {
        private EcsFilter<SupportVC> _supViewFilter = default;
        private EcsFilter<EnvC> _envFilt = default;
        private EcsFilter<FireC> _fireFilt = default;

        private EcsFilter<UnitC, LevelUnitC, OwnerC> _cellUnitFilter = default;
        private EcsFilter<UnitC, VisibleC> _unitVisFilt = default;


        public void Run()
        {
            foreach (var idx_0 in _supViewFilter)
            {
                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var levUnit_0 = ref _cellUnitFilter.Get2(idx_0);
                ref var ownUnit_0 = ref _cellUnitFilter.Get3(idx_0);
                ref var visUnit_0 = ref _unitVisFilt.Get2(idx_0);

                ref var supView_0 = ref _supViewFilter.Get1(idx_0);

                ref var env_0 = ref _envFilt.Get1(idx_0);
                ref var fire_0 = ref _fireFilt.Get1(idx_0);


                supView_0.DisableSR();

                if (SelectorC.IsSelCell)
                {
                    if (SelectorC.IdxSelCell == idx_0)
                    {
                        supView_0.EnableSR();
                        supView_0.SetColor(SupVisTypes.Selector);
                    }
                }

                if (fire_0.Have)
                {
                    if (SelectorC.Is(CellClickTypes.PutOutFireElfemale))
                    {
                        supView_0.EnableSR();
                        supView_0.SetColor(SupVisTypes.GivePawnTool);
                    }
                }

                if (unit_0.HaveUnit)
                {
                    if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (SelectorC.Is(CellClickTypes.GiveTakeTW, CellClickTypes.GiveScout))
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                supView_0.EnableSR();
                                supView_0.SetColor(SupVisTypes.GivePawnTool);
                            }
                        }

                        else if (SelectorC.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn, UnitTypes.Archer))
                            {
                                if (levUnit_0.Is(LevelUnitTypes.First))
                                {
                                    supView_0.EnableSR();
                                    supView_0.SetColor(SupVisTypes.GivePawnTool);
                                }
                            }
                        }

                        else if (SelectorC.Is(CellClickTypes.GiveHero))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                supView_0.EnableSR();
                                supView_0.SetColor(SupVisTypes.GivePawnTool);
                            }
                        }
                    }

                    else
                    {
                        if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (env_0.Have(EnvTypes.AdultForest))
                            {
                                if (SelectorC.Is(CellClickTypes.StunElfemale))
                                {
                                    supView_0.EnableSR();
                                    supView_0.SetColor(SupVisTypes.GivePawnTool);
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
                    if (_cellUnitFilter.Get3(SelectorC.IdxSelCell).Is(WhoseMoveC.CurPlayerI))
                    {
                        if (SelectorC.Is(CellClickTypes.PickFire))
                        {
                            foreach (var curIdxCell in CellsArsonArcherComp.GetListCopy(WhoseMoveC.CurPlayerI, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.FireSelector);
                            }
                        }

                        else if (SelectorC.Is(CellClickTypes.None))
                        {
                            foreach (var curIdxCell in CellsForShiftCom.GetListCopy(WhoseMoveC.CurPlayerI, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.Shift);
                            }

                            foreach (var curIdxCell in CellsAttackC.GetListCopy(WhoseMoveC.CurPlayerI, AttackTypes.Simple, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.SimpleAttack);
                            }

                            foreach (var curIdxCell in CellsAttackC.GetListCopy(WhoseMoveC.CurPlayerI, AttackTypes.Unique, SelectorC.IdxSelCell))
                            {
                                _supViewFilter.Get1(curIdxCell).EnableSR();
                                _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.UniqueAttack);
                            }
                        }
                    }
                }
            }
            if (SelUnitC.IsSelUnit)
            {
                foreach (var curIdxCell in CellsForSetUnitC.GetListCells(WhoseMoveC.CurPlayerI))
                {
                    _supViewFilter.Get1(curIdxCell).EnableSR();
                    _supViewFilter.Get1(curIdxCell).SetColor(SupVisTypes.Spawn);
                }
            }
        }
    }
}