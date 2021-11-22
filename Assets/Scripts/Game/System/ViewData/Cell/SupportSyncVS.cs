using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class SupportSyncVS : IEcsRunSystem
    {
        private EcsFilter<SupportVC> _supVF = default;
        private EcsFilter<EnvC> _envF = default;
        private EcsFilter<FireC> _fireF = default;

        private EcsFilter<UnitC, LevelC, OwnerC, VisibleC> _unitF = default;


        public void Run()
        {
            foreach (var idx_0 in _supVF)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var lev_0 = ref _unitF.Get2(idx_0);
                ref var own_0 = ref _unitF.Get3(idx_0);
                ref var vis_0 = ref _unitF.Get4(idx_0);

                ref var supV_0 = ref _supVF.Get1(idx_0);

                ref var env_0 = ref _envF.Get1(idx_0);
                ref var fire_0 = ref _fireF.Get1(idx_0);


                supV_0.DisableSR();

                if (SelIdx.Idx == idx_0)
                {
                    supV_0.EnableSR(SupVisTypes.Selector);
                }      

                if (fire_0.Have)
                {
                    if (CellClickC.Is(CellClickTypes.UniqAbil))
                    {
                        if (SelUniqAbilC.Is(UniqAbilTypes.ChangeDirWind))
                        {
                            supV_0.EnableSR(SupVisTypes.GivePawnTool);
                        }
                    }
                }

                if (unit_0.HaveUnit)
                {
                    if (own_0.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (CellClickC.Is(CellClickTypes.GiveTakeTW, CellClickTypes.GiveScout))
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                supV_0.EnableSR(SupVisTypes.GivePawnTool);
                            }
                        }

                        else if (CellClickC.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn, UnitTypes.Archer))
                            {
                                if (lev_0.Is(LevelTypes.First))
                                {
                                    supV_0.EnableSR(SupVisTypes.GivePawnTool);
                                }
                            }
                        }

                        else if (CellClickC.Is(CellClickTypes.GiveHero))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                supV_0.EnableSR(SupVisTypes.GivePawnTool);
                            }
                        }
                    }

                    else
                    {
                        if (vis_0.IsVisibled(WhoseMoveC.CurPlayerI))
                        {
                            if (env_0.Have(EnvTypes.AdultForest))
                            {
                                if (CellClickC.Is(CellClickTypes.UniqAbil))
                                {
                                    if (SelUniqAbilC.Is(UniqAbilTypes.StunElfemale))
                                    {
                                        supV_0.EnableSR(SupVisTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (CellClickC.Is(CellClickTypes.UniqAbil))
            {
                if (SelUniqAbilC.Is(UniqAbilTypes.FireArcher))
                {
                    foreach (var idx_0 in ArsonCellsC.List(WhoseMoveC.CurPlayerI, SelIdx.Idx))
                    {
                        _supVF.Get1(idx_0).EnableSR(SupVisTypes.FireSelector);
                    }
                }
            }
            else
            {
                foreach (var idx_0 in ShiftCellsC.List(WhoseMoveC.CurPlayerI, SelIdx.Idx))
                {
                    _supVF.Get1(idx_0).EnableSR(SupVisTypes.Shift);
                }

                foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Simple, SelIdx.Idx))
                {
                    _supVF.Get1(idx_0).EnableSR(SupVisTypes.SimpleAttack);
                }

                foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Unique, SelIdx.Idx))
                {
                    _supVF.Get1(idx_0).EnableSR(SupVisTypes.UniqueAttack);
                }
            }

            if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                foreach (var idx_0 in SetUnitCellsC.List(WhoseMoveC.CurPlayerI))
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