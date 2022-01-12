using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityPool;
using static Game.Game.EntityCellEnvPool;
using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct SupportSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            ref var cellClick = ref ClickerObject<CellClickC>();

            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
                ref var lev_0 = ref Unit<LevelC>(idx_0);
                ref var own_0 = ref Unit<PlayerC>(idx_0);

                ref var supV_0 = ref EntityCellVPool.ElseCellVE<SupportVC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                supV_0.DisableSR();

                if (SelIdx<IdxC>().Is(idx_0))
                {
                    supV_0.EnableSR(SupVisTypes.Selector);
                }

                if (fire_0.Have)
                {
                    if (cellClick.Is(CellClickTypes.UniqAbil))
                    {
                        if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirWind))
                        {
                            supV_0.EnableSR(SupVisTypes.GivePawnTool);
                        }
                    }
                }

                if (unit_0.Have)
                {
                    if (own_0.Is(WhoseMoveC.CurPlayerI))
                    {
                        if (cellClick.Is(CellClickTypes.GiveTakeTW, CellClickTypes.GiveScout))
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                supV_0.EnableSR(SupVisTypes.GivePawnTool);
                            }
                        }

                        else if (cellClick.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn, UnitTypes.Archer))
                            {
                                if (lev_0.Is(LevelTypes.First))
                                {
                                    supV_0.EnableSR(SupVisTypes.GivePawnTool);
                                }
                            }
                        }

                        else if (cellClick.Is(CellClickTypes.GiveHero))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                supV_0.EnableSR(SupVisTypes.GivePawnTool);
                            }
                        }
                    }

                    else
                    {
                        if (Unit<VisibledC>(WhoseMoveC.CurPlayerI, idx_0).IsVisibled)
                        {
                            if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                            {
                                if (cellClick.Is(CellClickTypes.UniqAbil))
                                {
                                    if (SelUniqAbilC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        supV_0.EnableSR(SupVisTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                    }
                }

                if (cellClick.Is(CellClickTypes.UniqAbil))
                {
                    if (SelUniqAbilC.Is(UniqueAbilityTypes.FireArcher))
                    {
                        if (unitE_0.CanArson(WhoseMoveC.CurPlayerI, idx_0))
                        {
                            EntityCellVPool.ElseCellVE<SupportVC>(idx_0).EnableSR(SupVisTypes.FireSelector);
                        }
                    }
                }
            }


            if (cellClick.Is(CellClickTypes.UniqAbil))
            {
                if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirWind))
                {
                    foreach (var item in WindC.Directs)
                    {
                        EntityCellVPool.ElseCellVE<SupportVC>(item.Value).EnableSR(SupVisTypes.Spawn);
                    }
                }
            }

            else if (cellClick.Is(CellClickTypes.SetUnit))
            {
                foreach (var idx_0 in SetUnitCellsC.List(WhoseMoveC.CurPlayerI))
                {
                    EntityCellVPool.ElseCellVE<SupportVC>(idx_0).EnableSR(SupVisTypes.Spawn);
                }
            }

            else
            {
                //foreach (var idx_0 in ShiftCellsC.List(WhoseMoveC.CurPlayerI, SelIdx.Idx))
                //{
                //    EntityVPool.ElseCellVC<SupportVC>(idx_0).EnableSR(SupVisTypes.Shift);
                //}

                foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Simple, SelIdx<IdxC>().Idx))
                {
                    EntityCellVPool.ElseCellVE<SupportVC>(idx_0).EnableSR(SupVisTypes.SimpleAttack);
                }

                foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Unique, SelIdx<IdxC>().Idx))
                {
                    EntityCellVPool.ElseCellVE<SupportVC>(idx_0).EnableSR(SupVisTypes.UniqueAttack);
                }
            }
        }
    }
}