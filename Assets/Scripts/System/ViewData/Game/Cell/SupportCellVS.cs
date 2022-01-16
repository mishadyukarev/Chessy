using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.EntityCellFirePool;

namespace Game.Game
{
    struct SupportCellVS : IEcsRunSystem
    {
        public void Run()
        {
            ref var cellClick = ref ClickerObject<CellClickC>();

            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitTC>(idx_0);
                ref var unitE_0 = ref Unit<UnitCellEC>(idx_0);
                ref var lev_0 = ref Unit<LevelTC>(idx_0);
                ref var own_0 = ref Unit<PlayerTC>(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

                ref var supportC = ref SupportCellVEs.Support<SpriteRendererVC>(idx_0);


                supportC.Disable();

                if (SelIdx<IdxC>().Is(idx_0))
                {
                    supportC.Enable();
                    supportC.Color = ColorsValues.Color(SupportCellVisionTypes.Selector);

                }

                if (fire_0.Have)
                {
                    if (cellClick.Is(CellClickTypes.UniqAbil))
                    {
                        if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirWind))
                        {
                            supportC.Enable();
                            supportC.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                        }
                    }
                }

                if (unit_0.Have)
                {
                    if (own_0.Is(WhoseMoveE.CurPlayerI))
                    {
                        if (cellClick.Is(CellClickTypes.GiveTakeTW, CellClickTypes.GiveScout))
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                supportC.Enable();
                                supportC.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                            }
                        }

                        else if (cellClick.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn, UnitTypes.Archer))
                            {
                                if (lev_0.Is(LevelTypes.First))
                                {
                                    supportC.Enable();
                                    supportC.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                }
                            }
                        }

                        else if (cellClick.Is(CellClickTypes.GiveHero))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                supportC.Enable();
                                supportC.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                            }
                        }
                    }

                    else
                    {
                        if (Unit<IsVisibledC>(WhoseMoveE.CurPlayerI, idx_0).IsVisibled)
                        {
                            if (Environment<HaveEnvironmentC>(EnvTypes.AdultForest, idx_0).Have)
                            {
                                if (cellClick.Is(CellClickTypes.UniqAbil))
                                {
                                    if (SelUniqAbilC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        supportC.Enable();
                                        supportC.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
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
                        if (unitE_0.CanArson(WhoseMoveE.CurPlayerI, idx_0))
                        {
                            supportC.Enable();
                            supportC.Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                        }
                    }
                }



                if (cellClick.Is(CellClickTypes.SetUnit))
                {        
                    if (CellsForSetUnitEs.CanSet<CanSetUnitC>(WhoseMoveE.CurPlayerI, idx_0).Can)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }
            }


            if (cellClick.Is(CellClickTypes.UniqAbil))
            {
                if (SelUniqAbilC.Is(UniqueAbilityTypes.ChangeDirWind))
                {
                    //foreach (var item in WindC.Directs)
                    //{
                    //    EntityCellVPool.ElseCellVE<SupportVC>(item.Value).EnableSR(SupVisTypes.Spawn);
                    //}
                }
            }


            else
            {
                var idxs = CellsForShiftUnitsEs.CellsForShift<IdxsC>(WhoseMoveE.CurPlayerI, SelIdx<IdxC>().Idx).Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                //foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI, AttackTypes.Simple, SelIdx<IdxC>().Idx))
                //{
                //    EntityCellVPool.ElseCellVE<SupportVC>(idx_0).EnableSR(SupVisTypes.SimpleAttack);
                //}

                //foreach (var idx_0 in AttackCellsC.List(WhoseMoveC.WhoseMove<WhoseMoveEC>().CurPlayerI, AttackTypes.Unique, SelIdx<IdxC>().Idx))
                //{
                //    EntityCellVPool.ElseCellVE<SupportVC>(idx_0).EnableSR(SupVisTypes.UniqueAttack);
                //}
            }
        }
    }
}