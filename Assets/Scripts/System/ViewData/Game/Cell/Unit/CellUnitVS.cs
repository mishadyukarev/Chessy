namespace Game.Game
{
    sealed class CellUnitVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitVS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                for (int i = 0; i <= 1; i++)
                {
                    var isSelZone = i == 0;

                    for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                    {
                        for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                        {
                            if (unitT != UnitTypes.Pawn)
                            {
                                VEs.UnitE(idx_0, isSelZone, levT, unitT).Disable();
                            }
                        }

                        for (var twT = ToolWeaponTypes.BowCrossbow; twT <= ToolWeaponTypes.Axe; twT++)
                        {
                            if (twT == ToolWeaponTypes.BowCrossbow)
                            {

                            }
                            else
                            {
                                VEs.UnitEs(idx_0).MainToolWeaponE(isSelZone, levT, twT).Disable();
                            }
                        }

                        for (var twT = ToolWeaponTypes.Pick; twT <= ToolWeaponTypes.Shield; twT++)
                        {
                            VEs.UnitEs(idx_0).ExtraToolWeaponE(isSelZone, levT, twT).Disable();
                        }

                        for (var twT = ToolWeaponTypes.BowCrossbow; twT <= ToolWeaponTypes.Axe; twT++)
                        {
                            if (twT == ToolWeaponTypes.BowCrossbow)
                            {
                                VEs.UnitEs(idx_0).MainBowCrossbowE(isSelZone, levT, true).Disable();
                                VEs.UnitEs(idx_0).MainBowCrossbowE(isSelZone, levT, false).Disable();
                            }
                            else
                            {
                                VEs.UnitEs(idx_0).MainToolWeaponE(isSelZone, levT, twT).Disable();
                            }
                            
                        }
                    }
                }

                if (Es.UnitTC(idx_0).HaveUnit)
                {
                    if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMovePlayerTC.CurPlayerI).IsVisible)
                    {
                        var isSelected = idx_0 == Es.SelectedIdxC.Idx;
                        var isVisForNext = Es.UnitEs(idx_0).VisibleE(Es.WhoseMovePlayerTC.NextPlayerFrom(Es.WhoseMovePlayerTC.CurPlayerI)).IsVisible;

                        var unitT = Es.UnitTC(idx_0).Unit;

                        if (unitT == UnitTypes.Pawn)
                        {
                            if (Es.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                VEs.UnitEs(idx_0).MainBowCrossbowE(isSelected, Es.UnitMainTWLevelTC(idx_0).Level, Es.UnitIsRightArcherC(idx_0).IsRight).Enable(isVisForNext);
                            }
                            else
                            {
                                VEs.UnitEs(idx_0).MainToolWeaponE(isSelected, Es.UnitMainTWLevelTC(idx_0).Level, Es.UnitMainTWTC(idx_0).ToolWeapon).Enable(isVisForNext);

                            }

                            if (Es.ExtraTWE(idx_0).ToolWeaponTC.HaveToolWeapon)
                            {
                                var twT = Es.ExtraTWE(idx_0).ToolWeaponTC.ToolWeapon;
                                var levT = Es.ExtraTWE(idx_0).LevelTC.Level;

                                VEs.UnitEs(idx_0).ExtraToolWeaponE(isSelected, levT, twT).Enable(isVisForNext);
                            }
                        }
                        else
                        {
                            VEs.UnitE(idx_0, isSelected, Es.UnitLevelTC(idx_0).Level, unitT).Enable(isVisForNext);
                        }
                    }
                }
            }
        }
    }
}
