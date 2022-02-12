namespace Game.Game
{
    sealed class CellSupportVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellSupportVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var cellClick = ref Es.ClickerObjectE.CellClickCRef;

            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                var unit_0 = UnitEs(idx_0).UnitE.UnitTC;
                var own_0 = Es.UnitE(idx_0).OwnerC;

                ref var support_0 = ref SupportCellVEs.Support<SpriteRendererVC>(idx_0);


                support_0.Disable();



                if (Es.EffectEs(idx_0).FireE.HaveFireC.Have)
                {
                    if (cellClick.Is(CellClickTypes.UniqueAbility))
                    {
                        if (Es.SelectedAbilityE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                        {
                            support_0.Enable();
                            support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                        }
                    }
                }

                if (Es.UnitEs(idx_0).UnitE.HaveUnit)
                {
                    if (own_0.Is(Es.WhoseMoveE.CurPlayerI))
                    {
                        if (cellClick.Is(CellClickTypes.UpgradeUnit))
                        {
                            if (unit_0.Is(UnitTypes.Pawn))
                            {
                                if (Es.UnitE(idx_0).Is(LevelTypes.First))
                                {
                                    support_0.Enable();
                                    support_0.Color = ColorsValues.Color(SupportCellVisionTypes.GivePawnTool);
                                }
                            }
                        }
                    }

                    else
                    {
                        if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                        {
                            if (Es.EnvironmentEs(idx_0).AdultForest.HaveEnvironment)
                            {
                                if (cellClick.Is(CellClickTypes.UniqueAbility))
                                {
                                    if (Es.SelectedAbilityE.AbilityTC.Is(AbilityTypes.StunElfemale))
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
                    if (CellsForSetUnitsEs.CanSet<CanSetUnitC>(Es.WhoseMoveE.CurPlayerI, idx_0).Can)
                    {
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                        SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                    }
                }
            }

            SupportCellVEs.Support<SpriteRendererVC>(Es.SelectedIdxE.IdxC.Idx).Enable();
            SupportCellVEs.Support<SpriteRendererVC>(Es.SelectedIdxE.IdxC.Idx).Color = ColorsValues.Color(SupportCellVisionTypes.Selector);


            if (cellClick.Is(CellClickTypes.UniqueAbility))
            {
                if (cellClick.Is(CellClickTypes.UniqueAbility))
                {
                    if (Es.SelectedAbilityE.AbilityTC.Is(AbilityTypes.ChangeDirectionWind))
                    {
                        CellWorker.TryGetIdxAround(Es.WindCloudE.CenterCloud.Idx, out var dirs);

                        foreach (var item in dirs)
                        {
                            SupportCellVEs.Support<SpriteRendererVC>(item.Value).Enable();
                            SupportCellVEs.Support<SpriteRendererVC>(item.Value).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                        }
                    }
                }

                else if (Es.SelectedAbilityE.AbilityTC.Is(AbilityTypes.FireArcher))
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
                var idxs = CellsForShiftUnitsEs.CellsForShift<IdxsC>(Es.WhoseMoveE.CurPlayerI, Es.SelectedIdxE.IdxC.Idx).Idxs;

                foreach (var idx_0 in idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.Shift);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(Es.SelectedIdxE.IdxC.Idx, AttackTypes.Simple, Es.WhoseMoveE.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.SimpleAttack);
                }

                foreach (var idx_0 in CellsForAttackUnitsEs.CanAttack<IdxsC>(Es.SelectedIdxE.IdxC.Idx, AttackTypes.Unique, Es.WhoseMoveE.CurPlayerI).Idxs)
                {
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Enable();
                    SupportCellVEs.Support<SpriteRendererVC>(idx_0).Color = ColorsValues.Color(SupportCellVisionTypes.UniqueAttack);
                }
            }
        }
    }
}