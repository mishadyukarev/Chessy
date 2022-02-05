namespace Game.Game
{
    sealed class SupportCellVS : SystemViewAbstract, IEcsRunSystem
    {
        public SupportCellVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var cellClick = ref Es.ClickerObject.CellClickC;

            foreach (byte idx_0 in CellWorker.Idxs)
            {
                var unit_0 = UnitEs(idx_0).MainE.UnitTC;
                var own_0 = UnitEs(idx_0).OwnerE.OwnerC;

                ref var support_0 = ref SupportCellVEs.Support<SpriteRendererVC>(idx_0);


                support_0.Disable();

                if (Es.SelectedIdxE.IdxC.Is(idx_0))
                {
                    support_0.Enable();
                    support_0.Color = ColorsValues.Color(SupportCellVisionTypes.Selector);

                }

                if (EffectEs(idx_0).FireE.HaveFireC.Have)
                {
                    if (cellClick.Is(CellClickTypes.UniqueAbility))
                    {
                        if (Es.SelectedUniqueAbilityE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                        {
                            support_0.Enable();
                            support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                        }
                    }
                }

                if (UnitEs(idx_0).MainE.HaveUnit)
                {
                    if (own_0.Is(Es.WhoseMove.CurPlayerI))
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
                                if (UnitEs(idx_0).LevelE.LevelTC.Is(LevelTypes.First))
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
                        if (UnitEs(idx_0).VisibleE(Es.WhoseMove.CurPlayerI).IsVisibleC.IsVisible)
                        {
                            if (EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                            {
                                if (cellClick.Is(CellClickTypes.UniqueAbility))
                                {
                                    if (Es.SelectedUniqueAbilityE.AbilityTC.Is(AbilityTypes.StunElfemale))
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
                    if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(Es.WhoseMove.CurPlayerI, idx_0).Can)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }
            }

            if (cellClick.Is(CellClickTypes.UniqueAbility))
            {
                if (cellClick.Is(CellClickTypes.UniqueAbility))
                {
                    if (Es.SelectedUniqueAbilityE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                    {
                        CellWorker.TryGetIdxAround(Es.WindCloudE.CenterCloud.Idx, out var dirs);

                        foreach (var item in dirs)
                        {
                            SupportCellVEs.Support<SpriteRendererVC>(item.Value).Enable();
                            SupportCellVEs.Support<SpriteRendererVC>(item.Value).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }
                    }
                }

                else if (Es.SelectedUniqueAbilityE.AbilityTC.Is(AbilityTypes.FireArcher))
                {
                    foreach (var idx in CellsForArsonArcherEs.Idxs<IdxsC>(Es.SelectedIdxE.IdxC.Idx).Idxs)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx).Color = ColorsValues.Color(SupportCellVisionTypes.FireSelector);
                    }
                }
            }


            else
            {
                var idxs = CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMove.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(Es.SelectedIdxE.IdxC.Idx, AttackTypes.Simple, Es.WhoseMove.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(Es.SelectedIdxE.IdxC.Idx, AttackTypes.Unique, Es.WhoseMove.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                }
            }
        }
    }
}