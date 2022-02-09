using System;
using UnityEngine;

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
                for (var unitT = UnitTypes.None + 1; unitT < UnitTypes.End; unitT++)
                {
                    if (unitT == UnitTypes.Pawn)
                    {
                        for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                        {
                            VEs.UnitEs(idx_0).PawnE(true, levT).Disable();
                            VEs.UnitEs(idx_0).PawnE(false, levT).Disable();
                        }
                    }
                    else
                    {
                        VEs.UnitE(idx_0, unitT, true).Disable();
                        VEs.UnitE(idx_0, unitT, false).Disable();
                    } 
                }

                for (var tw = ToolWeaponTypes.None + 1; tw < ToolWeaponTypes.End; tw++)
                {
                    if (tw == ToolWeaponTypes.Shield)
                    {
                        for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                        {
                            VEs.UnitEs(idx_0).ShieldE(levT, true).Disable();
                            VEs.UnitEs(idx_0).ShieldE(levT, false).Disable();
                        }
                    }
                    else if (tw == ToolWeaponTypes.BowCrossbow)
                    {
                        for (var levT = LevelTypes.None + 1; levT < LevelTypes.End; levT++)
                        {
                            VEs.UnitEs(idx_0).BowCrossbowE(true, true, levT).Disable();
                            VEs.UnitEs(idx_0).BowCrossbowE(true, false, levT).Disable();
                            VEs.UnitEs(idx_0).BowCrossbowE(false, true, levT).Disable();
                            VEs.UnitEs(idx_0).BowCrossbowE(false, false, levT).Disable();

                        }
                    }
                    else
                    {
                        VEs.UnitEs(idx_0).ToolWeaponE(tw, true).Disable();
                        VEs.UnitEs(idx_0).ToolWeaponE(tw, false).Disable();
                    }
                }




                if (Es.UnitTypeE(idx_0).HaveUnit)
                {
                    if (Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.CurPlayerI).IsVisibleC.IsVisible)
                    {
                        var isSelected = idx_0 == Es.SelectedIdxE.IdxC.Idx;
                        var isVisForNext = Es.UnitEs(idx_0).VisibleE(Es.WhoseMoveE.NextPlayerFrom(Es.WhoseMoveE.CurPlayerI)).IsVisibleC.IsVisible;

                        var unitT = Es.UnitTypeE(idx_0).UnitT;
                        var levT = Es.UnitLevelE(idx_0).LevelT;

                        if (unitT == UnitTypes.Pawn)
                        {
                            VEs.UnitEs(idx_0).PawnE(isSelected, Es.UnitLevelE(idx_0).LevelT).Enable(isVisForNext);

                            if (Es.UnitEs(idx_0).ToolWeaponE.ToolWeaponTC.HaveTW)
                            {
                                var twT = Es.UnitEs(idx_0).ToolWeaponE.ToolWeaponT;

                                if (twT == ToolWeaponTypes.Shield)
                                {
                                    VEs.UnitEs(idx_0).ShieldE(Es.UnitEs(idx_0).ToolWeaponE.LevelT, isSelected).Enable(isVisForNext);
                                }
                                else if (twT == ToolWeaponTypes.BowCrossbow)
                                {
                                    VEs.UnitEs(idx_0).BowCrossbowE(Es.UnitEs(idx_0).CornedE.IsRight, isSelected, Es.UnitTWE(idx_0).LevelT).Enable(isVisForNext);
                                }
                                else
                                {
                                    VEs.UnitEs(idx_0).ToolWeaponE(twT, isSelected).Enable(isVisForNext);
                                }

                                if (twT == ToolWeaponTypes.BowCrossbow)
                                {
                                    VEs.UnitEs(idx_0).PawnE(isSelected, Es.UnitLevelE(idx_0).LevelT).Disable();
                                }
                            }
                        }
                        else
                        {
                            VEs.UnitE(idx_0, unitT, isSelected).Enable(isVisForNext);
                        }
                    }
                }
            }
        }
    }
}
