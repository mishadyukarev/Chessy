using Leopotam.Ecs;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class SupportSyncVS : IEcsRunSystem
    {
        public void Run()
        {
            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var lev_0 = ref Unit<LevelC>(idx_0);
                ref var own_0 = ref Unit<OwnerC>(idx_0);
                ref var vis_0 = ref Unit<VisibleC>(idx_0);

                ref var supV_0 = ref EntityCellVPool.ElseCellVC<SupportVC>(idx_0);

                ref var env_0 = ref Environment<EnvC>(idx_0);
                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);


                supV_0.DisableSR();

                if (SelIdx<IdxC>().Is(idx_0))
                {
                    supV_0.EnableSR(SupVisTypes.Selector);
                }      

                if (fire_0.Have)
                {
                    if (CellClickC.Is(CellClickTypes.UniqAbil))
                    {
                        if (SelUniqAbilC.Is(UniqueAbilTypes.ChangeDirWind))
                        {
                            supV_0.EnableSR(SupVisTypes.GivePawnTool);
                        }
                    }
                }

                if (unit_0.Have)
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
                                    if (SelUniqAbilC.Is(UniqueAbilTypes.StunElfemale))
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
                if (SelUniqAbilC.Is(UniqueAbilTypes.FireArcher))
                {
                    foreach (var idx_0 in ArsonCellsC.List(WhoseMoveC.CurPlayerI, SelIdx<IdxC>().Idx))
                    {
                        EntityCellVPool.ElseCellVC<SupportVC>(idx_0).EnableSR(SupVisTypes.FireSelector);
                    }
                }

                else if (SelUniqAbilC.Is(UniqueAbilTypes.ChangeDirWind))
                {
                    foreach (var item in WindC.Directs)
                    {
                        EntityCellVPool.ElseCellVC<SupportVC>(item.Value).EnableSR(SupVisTypes.Spawn);
                    }
                }
            }

            else if (CellClickC.Is(CellClickTypes.SetUnit))
            {
                foreach (var idx_0 in SetUnitCellsC.List(WhoseMoveC.CurPlayerI))
                {
                    EntityCellVPool.ElseCellVC<SupportVC>(idx_0).EnableSR(SupVisTypes.Spawn);
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
                    EntityCellVPool.ElseCellVC<SupportVC>(idx_0).EnableSR(SupVisTypes.SimpleAttack);
                }

                foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.CurPlayerI, AttackTypes.Unique, SelIdx<IdxC>().Idx))
                {
                    EntityCellVPool.ElseCellVC<SupportVC>(idx_0).EnableSR(SupVisTypes.UniqueAttack);
                }
            }
        }
    }
}