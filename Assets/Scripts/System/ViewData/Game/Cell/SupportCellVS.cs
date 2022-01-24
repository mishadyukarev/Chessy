using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;

namespace Game.Game
{
    struct SupportCellVS : IEcsRunSystem
    {
        public void Run()
        {
            ref var cellClick = ref ClickerObject<CellClickC>();

            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit(idx_0);
                ref var lev_0 = ref EntitiesPool.UnitElse.Level(idx_0);
                ref var own_0 = ref EntitiesPool.UnitElse.Owner(idx_0);

                ref var fire_0 = ref Fire<HaveEffectC>(idx_0);

                ref var support_0 = ref SupportCellVEs.Support<SpriteRendererVC>(idx_0);


                support_0.Disable();

                if (SelectedIdxE.IdxC.Is(idx_0))
                {
                    support_0.Enable();
                    support_0.Color = ColorsValues.Color(SupportCellVisionTypes.Selector);

                }

                if (fire_0.Have)
                {
                    if (cellClick.Is(CellClickTypes.UniqueAbility))
                    {
                        if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.ChangeDirectionWind))
                        {
                            support_0.Enable();
                            support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
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
                                support_0.Enable();
                                support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                            }
                        }

                        else if (cellClick.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn, UnitTypes.Archer))
                            {
                                if (lev_0.Is(LevelTypes.First))
                                {
                                    support_0.Enable();
                                    support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                }
                            }
                        }

                        else if (cellClick.Is(CellClickTypes.GiveHero))
                        {
                            if (unit_0.Is(UnitTypes.Archer))
                            {
                                support_0.Enable();
                                support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                            }
                        }
                    }

                    else
                    {
                        if (CellUnitVisibleEs.Visible(WhoseMoveE.CurPlayerI, idx_0).IsVisible)
                        {
                            if (Resources(EnvironmentTypes.AdultForest, idx_0).Have)
                            {
                                if (cellClick.Is(CellClickTypes.UniqueAbility))
                                {
                                    if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.StunElfemale))
                                    {
                                        support_0.Enable();
                                        support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                    }
                                }
                            }
                        }
                    }
                }


                if (cellClick.Is(CellClickTypes.SetUnit))
                {        
                    if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(WhoseMoveE.CurPlayerI, idx_0).Can)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }
            }


            if (cellClick.Is(CellClickTypes.UniqueAbility))
            {
                if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.ChangeDirectionWind))
                {
                    foreach (var idx_0 in DirectsWindForElfemaleE.IdxsDirects)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }
                else if (SelectedUniqueAbilityC.AbilityC.Is(UniqueAbilityTypes.FireArcher))
                {
                    foreach (var idx in CellsForArsonArcherEs.Idxs<IdxsC>(SelectedIdxE.IdxC.Idx).Idxs)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                var idxs = CellsForShiftUnitsEs.CellsForShift<IdxsC>(WhoseMoveE.CurPlayerI, SelectedIdxE.IdxC.Idx).Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(SelectedIdxE.IdxC.Idx, AttackTypes.Simple, WhoseMoveE.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(SelectedIdxE.IdxC.Idx, AttackTypes.Unique, WhoseMoveE.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                }
            }
        }
    }
}