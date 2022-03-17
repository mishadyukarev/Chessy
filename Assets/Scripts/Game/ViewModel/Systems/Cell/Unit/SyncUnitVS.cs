using UnityEngine;

namespace Chessy.Game
{
    static class SyncUnitVS
    {
        static readonly Color _color1 = new Color(1, 1, 1, 1f);
        static readonly Color _color2 = new Color(1, 1, 1, 0.6f);

        internal static void Sync(in byte idx_0, in EntitiesView vEs, in EntitiesModel e)
        {
            foreach (var item in vEs.UnitEs(idx_0).Ents.Values) item.Disable();
            foreach (var item in vEs.UnitEs(idx_0).ExtraTws.Values) item.Disable();
            foreach (var item in vEs.UnitEs(idx_0).MainTws.Values) item.Disable();
            foreach (var item in vEs.UnitEs(idx_0).BowCrossbows.Values) item.Disable();


            if (e.CellsC.Current == idx_0)
            {
                if (e.CellClickTC.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = e.CellsC.Current;
                    var selUnitT = e.SelectedE.UnitC.UnitTC;
                    var levT = e.SelectedE.UnitC.LevelTC;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        vEs.UnitEs(idx_cur).MainToolWeaponE(true, LevelTypes.First, ToolWeaponTypes.Axe).Enable();
                    }
                    else
                    {
                        vEs.UnitE(idx_cur, true, levT, selUnitT).Enable();
                    }
                }
            }

            if (e.UnitTC(idx_0).HaveUnit)
            {
                if (e.UnitEs(idx_0).ForPlayer(e.CurPlayerITC.Player).IsVisible)
                {
                    var isSelected = idx_0 == e.CellsC.Selected;
                    var isVisForNext = e.UnitEs(idx_0).ForPlayer(e.NextPlayer(e.CurPlayerITC.Player).Player).IsVisible;

                    var unitT = e.UnitTC(idx_0).Unit;


                    SpriteRendererVC sr = default;

                    if (unitT == UnitTypes.Pawn)
                    {
                        if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            sr = vEs.UnitEs(idx_0).MainBowCrossbowE(isSelected, e.UnitMainTWLevelTC(idx_0).Level, e.UnitIsRightArcherC(idx_0).IsRight);
                        }
                        else
                        {
                            sr = vEs.UnitEs(idx_0).MainToolWeaponE(isSelected, e.UnitMainTWLevelTC(idx_0).Level, e.UnitMainTWTC(idx_0).ToolWeapon);
                        }


                        //if (isVisForNext) SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 1);
                        //else SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 0.6f);

                        if (e.UnitExtraTWTC(idx_0).HaveToolWeapon)
                        {
                            var twT = e.UnitExtraTWTC(idx_0).ToolWeapon;
                            var levT = e.UnitExtraLevelTC(idx_0).Level;

                            var sr2 = vEs.UnitEs(idx_0).ExtraToolWeaponE(isSelected, levT, twT);

                            sr2.Enable();
                            sr2.SR.color = isVisForNext ? _color1 : _color2;
                        }
                    }
                    else
                    {
                        sr = vEs.UnitE(idx_0, isSelected, e.UnitLevelTC(idx_0).Level, unitT);
                    }

                    sr.Enable();
                    sr.SR.color = isVisForNext ? _color1 : _color2;
                }
            }
        }
    }
}
