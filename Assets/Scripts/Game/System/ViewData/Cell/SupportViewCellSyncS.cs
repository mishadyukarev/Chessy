using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class SupportViewCellSyncS : IEcsRunSystem
    {
        private EcsFilter<SupportVC> _supVF = default;
        private EcsFilter<EnvC> _envFilt = default;
        private EcsFilter<FireC> _fireFilt = default;

        private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _unitF = default;


        public void Run()
        {
            foreach (var idx_0 in _supVF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var levUnit_0 = ref _unitF.Get2(idx_0);
                ref var ownUnit_0 = ref _unitF.Get3(idx_0);
                ref var visUnit_0 = ref _unitF.Get4(idx_0);

                ref var supView_0 = ref _supVF.Get1(idx_0);

                ref var env_0 = ref _envFilt.Get1(idx_0);
                ref var fire_0 = ref _fireFilt.Get1(idx_0);


                supView_0.DisableSR();

                if (SelIdx.IsSelCell)
                {
                    if (SelIdx.Idx == idx_0)
                    {
                        supView_0.EnableSR(SupVisTypes.Selector);
                    }
                }

                if (fire_0.Have)
                {
                    if (CellClickC.Is(CellClickTypes.UniqAbil))
                    {
                        if (SelUniqAbilC.Is(UniqAbilTypes.ChangeDirWind))
                        {
                            supView_0.EnableSR(SupVisTypes.GivePawnTool);
                        }
                    }
                }

                if (unit_0.HaveUnit)
                {
                    if (ownUnit_0.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (CellClickC.Is(CellClickTypes.GiveTakeTW, CellClickTypes.GiveScout))
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                supView_0.EnableSR(SupVisTypes.GivePawnTool);
                            }
                        }

                        else if (CellClickC.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn, UnitTypes.Archer))
                            {
                                if (levUnit_0.Is(LevelTypes.First))
                                {
                                    supView_0.EnableSR(SupVisTypes.GivePawnTool);
                                }
                            }
                        }

                        else if (CellClickC.Is(CellClickTypes.GiveHero))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                supView_0.EnableSR(SupVisTypes.GivePawnTool);
                            }
                        }
                    }

                    else
                    {
                        if (visUnit_0.IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (env_0.Have(EnvTypes.AdultForest))
                            {
                                if (CellClickC.Is(CellClickTypes.UniqAbil))
                                {
                                    if (SelUniqAbilC.Is(UniqAbilTypes.StunElfemale))
                                    {
                                        supView_0.EnableSR(SupVisTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            if (SelIdx.IsSelCell)
            {
                ref var selUnitDatCom = ref _unitF.Get1(SelIdx.Idx);
                ref var selOffUnitCom = ref _unitF.Get3(SelIdx.Idx);


                if (selUnitDatCom.HaveUnit)
                {
                    if (_unitF.Get3(SelIdx.Idx).Is(WhoseMoveC.CurPlayerI))
                    {
                        if (CellClickC.Is(CellClickTypes.UniqAbil))
                        {
                            if (SelUniqAbilC.Is(UniqAbilTypes.FireArcher))
                            {
                                foreach (var curIdxCell in CellsArsonArcherComp.GetListCopy(WhoseMoveC.CurPlayerI, SelIdx.Idx))
                                {
                                    _supVF.Get1(curIdxCell).EnableSR(SupVisTypes.FireSelector);
                                }
                            }
                        }

                        else if (CellClickC.Is(CellClickTypes.None))
                        {
                            foreach (var curIdxCell in CellsForShiftCom.List(WhoseMoveC.CurPlayerI, SelIdx.Idx))
                            {
                                _supVF.Get1(curIdxCell).EnableSR(SupVisTypes.Shift);
                            }

                            foreach (var curIdxCell in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Simple, SelIdx.Idx))
                            {
                                _supVF.Get1(curIdxCell).EnableSR(SupVisTypes.SimpleAttack);
                            }

                            foreach (var curIdxCell in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Unique, SelIdx.Idx))
                            {
                                _supVF.Get1(curIdxCell).EnableSR(SupVisTypes.UniqueAttack);
                            }
                        }
                    }
                }
            }

            if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                foreach (var idx_0 in CellsForSetUnitC.GetListCells(WhoseMoveC.CurPlayerI))
                {
                    _supVF.Get1(idx_0).EnableSR(SupVisTypes.Spawn);
                }
            }

            if (CellClickC.Is(CellClickTypes.UniqAbil))
            {
                if (SelUniqAbilC.Is(UniqAbilTypes.ChangeDirWind))
                {
                    foreach (var item in WindC.Directs)
                    {
                        _supVF.Get1(item.Value).EnableSR(SupVisTypes.Spawn);
                    }
                }
            }
        }
    }
}