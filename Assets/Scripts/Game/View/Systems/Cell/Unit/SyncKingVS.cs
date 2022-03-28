using Chessy.Game.Entity.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncKingVS
    {
        readonly EntitiesViewGame _eV;
        readonly EntitiesModelGame _e;

        public readonly Color Color1 = new Color(1, 1, 1, 1f);
        public readonly Color Color2 = new Color(1, 1, 1, 0.6f);

        internal SyncKingVS(in EntitiesViewGame eViewGame, in EntitiesModelGame eModelGame)
        {
            _eV = eViewGame;
            _e = eModelGame;
        }

        internal void Sync(in byte cell)
        {
            //for (var playerT = (PlayerTypes)1; playerT < PlayerTypes.End; playerT++)
            //{
            //    if (playerT == _e.CurPlayerITC.Player)
            //    {
            //        if (_e.CellClickTC.Is(CellClickTypes.SetUnit))
            //        {
            //            if (_e.SelectedUnitE.UnitTC.Is(UnitTypes.King))
            //            {
            //                _eV.KingE(playerT).ParenGOC.SetActive(true);
            //                _eV.KingE(playerT).ParenGOC.Transform.position = _eV.CellEs(_e.CellsC.Current).CellParent.Transform.position;
            //            }
            //        }
            //    }

            //    if (_e.PlayerInfoE(playerT).HaveKingInInventor)
            //    {
            //        _eV.KingE(playerT).ParenGOC.SetActive(false);
            //    }
            //    else
            //    {
            //        _eV.KingE(playerT).ParenGOC.SetActive(true);

            //        var _cell = _e.PlayerInfoE(playerT).KingCell;

            //        _eV.KingE(playerT).ParenGOC.Transform.position = _eV.CellEs(_cell).CellParent.Transform.position;

            //        if (_e.CellsC.Selected == _cell)
            //        {
            //            _eV.KingE(playerT).SelectedSRC.SetActive(true);
            //            _eV.KingE(playerT).NotSelectedSRC.SetActive(false);

            //            _eV.KingE(playerT).SelectedSRC.SR.color = _e.UnitEs(_cell).ForPlayer(_e.NextPlayer(playerT).Player).IsVisible ? Color1 : Color2;
            //        }
            //        else
            //        {
            //            _eV.KingE(playerT).SelectedSRC.SetActive(false);
            //            _eV.KingE(playerT).NotSelectedSRC.SetActive(true);

            //            _eV.KingE(playerT).NotSelectedSRC.SR.color = _e.UnitEs(_cell).ForPlayer(_e.NextPlayer(playerT).Player).IsVisible ? Color1 : Color2;
            //        }
            //    }
            //}



            foreach (var item in _eV.UnitEs(cell).Ents.Values) item.GameObject.SetActive(false);
            foreach (var item in _eV.UnitEs(cell).ExtraTws.Values) item.GameObject.SetActive(false);
            foreach (var item in _eV.UnitEs(cell).MainTws.Values) item.GameObject.SetActive(false);
            foreach (var item in _eV.UnitEs(cell).BowCrossbows.Values) item.GameObject.SetActive(false);




            if (_e.CellsC.Current == cell)
            {
                if (_e.CellClickTC.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = _e.CellsC.Current;
                    var selUnitT = _e.SelectedUnitE.UnitTC.Unit;
                    var levT = _e.SelectedUnitE.LevelTC.Level;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        _eV.UnitEs(idx_cur).MainToolWeaponE(true, LevelTypes.First, ToolWeaponTypes.Axe).GameObject.SetActive(true);
                    }
                    else
                    {
                        _eV.UnitE(idx_cur, true, levT, selUnitT).GameObject.SetActive(true);
                    }
                }
            }


            if (_e.UnitTC(cell).HaveUnit)
            {
                if (_e.UnitEs(cell).ForPlayer(_e.CurPlayerITC.Player).IsVisible)
                {
                    var isSelected = cell == _e.CellsC.Selected;
                    var isVisForNext = _e.UnitEs(cell).ForPlayer(_e.NextPlayer(_e.CurPlayerITC.Player).Player).IsVisible;

                    var unitT = _e.UnitTC(cell).Unit;


                    SpriteRendererVC sr = default;

                    if (unitT == UnitTypes.Pawn)
                    {
                        if (_e.UnitMainTWTC(cell).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            sr = _eV.UnitEs(cell).MainBowCrossbowE(isSelected, _e.UnitMainTWLevelTC(cell).Level, _e.UnitIsRightArcherC(cell).IsRight);
                        }
                        else
                        {
                            sr = _eV.UnitEs(cell).MainToolWeaponE(isSelected, _e.UnitMainTWLevelTC(cell).Level, _e.UnitMainTWTC(cell).ToolWeapon);
                        }


                        //if (isVisForNext) SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 1);
                        //else SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 0.6f);

                        if (_e.UnitExtraTWTC(cell).HaveToolWeapon)
                        {
                            var twT = _e.UnitExtraTWTC(cell).ToolWeapon;
                            var levT = _e.UnitExtraLevelTC(cell).Level;

                            var sr2 = _eV.UnitEs(cell).ExtraToolWeaponE(isSelected, levT, twT);

                            sr2.GameObject.SetActive(true);
                            sr2.SR.color = isVisForNext ? Color1 : Color2;
                        }
                    }
                    else
                    {
                        sr = _eV.UnitE(cell, isSelected, _e.UnitLevelTC(cell).Level, unitT);
                    }

                    sr.GameObject.SetActive(true);
                    sr.SR.color = isVisForNext ? Color1 : Color2;
                }
            }
        }
    }
}
