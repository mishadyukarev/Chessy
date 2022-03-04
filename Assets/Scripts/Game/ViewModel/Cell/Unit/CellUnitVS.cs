namespace Chessy.Game
{
    sealed class CellUnitVS : SystemViewAbstract, IEcsRunSystem
    {
        internal CellUnitVS(in EntitiesModel ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
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
                                VEs.UnitE(idx_0, isSelZone, levT, unitT).SpriteRenderC.Disable();
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

                if (E.UnitTC(idx_0).HaveUnit)
                {
                    if (E.UnitEs(idx_0).ForPlayer(E.CurPlayerITC.Player).IsVisible)
                    {
                        var isSelected = idx_0 == E.SelectedIdxC.Idx;
                        var isVisForNext = E.UnitEs(idx_0).ForPlayer(E.NextPlayer(E.CurPlayerITC.Player).Player).IsVisible;

                        var unitT = E.UnitTC(idx_0).Unit;

                        if (unitT == UnitTypes.Pawn)
                        {
                            if (E.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                            {
                                VEs.UnitEs(idx_0).MainBowCrossbowE(isSelected, E.UnitMainTWLevelTC(idx_0).Level, E.UnitIsRightArcherC(idx_0).IsRight).Enable(isVisForNext);
                            }
                            else
                            {
                                VEs.UnitEs(idx_0).MainToolWeaponE(isSelected, E.UnitMainTWLevelTC(idx_0).Level, E.UnitMainTWTC(idx_0).ToolWeapon).Enable(isVisForNext);

                            }

                            if (E.UnitExtraTWTC(idx_0).HaveToolWeapon)
                            {
                                var twT = E.UnitExtraTWTC(idx_0).ToolWeapon;
                                var levT = E.UnitExtraLevelTC(idx_0).Level;

                                VEs.UnitEs(idx_0).ExtraToolWeaponE(isSelected, levT, twT).Enable(isVisForNext);
                            }
                        }
                        else
                        {
                            VEs.UnitE(idx_0, isSelected, E.UnitLevelTC(idx_0).Level, unitT).Enable(isVisForNext);
                        }
                    }
                }
            }
        }
    }
}
