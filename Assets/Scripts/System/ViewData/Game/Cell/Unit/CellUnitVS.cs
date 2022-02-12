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
                    }
                }

                if (Es.UnitE(idx_0).HaveUnit)
                {
                    if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        var isSelected = idx_0 == Es.SelectedIdxE.IdxC.Idx;
                        var isVisForNext = Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(Es.WhoseMoveE.CurPlayerI)).IsVisibleC.IsVisible;

                        var unitT = Es.UnitE(idx_0).Unit;

                        if (unitT == UnitTypes.Pawn)
                        {
                            VEs.UnitEs(idx_0).MainToolWeaponE(isSelected, Es.UnitEs(idx_0).MainToolWeaponE.Level, Es.UnitEs(idx_0).MainToolWeaponE.ToolWeapon).Enable(isVisForNext);

                            if (Es.UnitEs(idx_0).ExtraToolWeaponE.ToolWeaponTC.HaveTW)
                            {
                                var twT = Es.UnitExtraTWE(idx_0).ToolWeaponT;
                                var levT = Es.UnitExtraTWE(idx_0).LevelT;

                                if (twT == ToolWeaponTypes.BowCrossbow)
                                {
                                    //VEs.UnitEs(idx_0).BowCrossbowE(isSelected, levT, Es.UnitEs(idx_0).CornedE.IsRight).Enable(isVisForNext);
                                }
                                else
                                {
                                    VEs.UnitEs(idx_0).ExtraToolWeaponE(isSelected, levT, twT).Enable(isVisForNext);
                                }

                                //if (twT == ToolWeaponTypes.BowCrossbow || twT == ToolWeaponTypes.Axe)
                                //{
                                //    VEs.UnitEs(idx_0).PawnE(isSelected, Es.UnitLevelE(idx_0).LevelT).Disable();
                                //}
                            }
                        }
                        else
                        {
                            VEs.UnitE(idx_0, isSelected, Es.UnitE(idx_0).Level, unitT).Enable(isVisForNext);
                        }
                    }
                }
            }
        }
    }
}
