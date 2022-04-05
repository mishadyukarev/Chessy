using Chessy.Game.Model.Entity;
using Chessy.Game.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncUnitVS
    {
        readonly EntitiesViewGame _eV;
        readonly EntitiesModelGame _e;

        public readonly Color Color1 = new Color(1, 1, 1, 1f);
        public readonly Color Color2 = new Color(1, 1, 1, 0.6f);

        internal SyncUnitVS(in EntitiesViewGame eViewGame, in EntitiesModelGame eModelGame)
        {
            _eV = eViewGame;
            _e = eModelGame;
        }

        internal void Sync(in byte cell_0)
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



            foreach (var item in _eV.UnitEs(cell_0).Ents.Values) item.GameObject.SetActive(false);
            foreach (var item in _eV.UnitEs(cell_0).ExtraTws.Values) item.GameObject.SetActive(false);
            foreach (var item in _eV.UnitEs(cell_0).MainTws.Values) item.GameObject.SetActive(false);
            foreach (var item in _eV.UnitEs(cell_0).BowCrossbows.Values) item.GameObject.SetActive(false);




            if (_e.CellsC.Current == cell_0)
            {
                if (_e.CellClickTC.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = _e.CellsC.Current;
                    var selUnitT = _e.SelectedUnitE.UnitTC.UnitT;
                    var levT = _e.SelectedUnitE.LevelTC.LevelT;

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


            if (_e.UnitTC(cell_0).HaveUnit)
            {
                if (_e.UnitVisibleC(cell_0).IsVisible(_e.CurPlayerITC.PlayerT))
                {
                    var isSelected = cell_0 == _e.CellsC.Selected;


                    if (_e.UnitPlayerTC(cell_0).Is(PlayerTypes.First))
                    {
                        if (_e.UnitTC(cell_0).Is(UnitTypes.Elfemale))
                        {

                        }
                        else if (_e.UnitTC(cell_0).Is(UnitTypes.King))
                        {

                        }
                        else if (_e.UnitTC(cell_0).Is(UnitTypes.Pawn))
                        {

                        }
                    }

                    var nextPlayer = _e.UnitPlayerTC(cell_0).PlayerT.NextPlayer();

                    var isVisForNext = _e.UnitVisibleC(cell_0).IsVisible(nextPlayer);

                    //if (!_e.UnitTC(cell_0).IsAnimal)
                    //{
                    //    isVisForNext = _e.UnitVisibleC(cell_0).IsVisible(_e.CurPlayerITC.PlayerT.NextPlayer());
                    //}



                    var unitT = _e.UnitTC(cell_0).UnitT;


                    SpriteRendererVC sr = default;

                    if (unitT == UnitTypes.Pawn)
                    {
                        if (_e.MainToolWeaponTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            sr = _eV.UnitEs(cell_0).MainBowCrossbowE(isSelected, _e.MainTWLevelTC(cell_0).LevelT, _e.UnitIsRightArcherC(cell_0).IsRight);
                        }
                        else
                        {
                            sr = _eV.UnitEs(cell_0).MainToolWeaponE(isSelected, _e.MainTWLevelTC(cell_0).LevelT, _e.MainToolWeaponTC(cell_0).ToolWeaponT);
                        }


                        //if (isVisForNext) SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 1);
                        //else SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 0.6f);

                        if (_e.ExtraToolWeaponTC(cell_0).HaveToolWeapon)
                        {
                            var twT = _e.ExtraToolWeaponTC(cell_0).ToolWeaponT;
                            var levT = _e.ExtraTWLevelTC(cell_0).LevelT;

                            var sr2 = _eV.UnitEs(cell_0).ExtraToolWeaponE(isSelected, levT, twT);

                            sr2.GameObject.SetActive(true);
                            sr2.SR.color = isVisForNext ? Color1 : Color2;
                        }
                    }
                    else
                    {
                        sr = _eV.UnitE(cell_0, isSelected, _e.UnitLevelTC(cell_0).LevelT, unitT);
                    }

                    sr.GameObject.SetActive(true);
                    sr.SR.color = isVisForNext ? Color1 : Color2;
                }
            }
        }
    }
}
