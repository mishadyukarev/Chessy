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

        readonly Color _color1 = new Color(1, 1, 1, 1f);
        readonly Color _color2 = new Color(1, 1, 1, 0.6f);


        readonly bool[] _needActive = new bool[(byte)UnitTypes.End];
        readonly Color[] _needColor = new Color[(byte)UnitTypes.End];

        readonly byte _curCell;


        internal SyncUnitVS(in byte cell, in EntitiesViewGame eViewGame, in EntitiesModelGame eModelGame)
        {
            _eV = eViewGame;
            _e = eModelGame;

            _curCell = cell;
        }

        internal void Sync()
        {
            //foreach (var item in _eV.UnitEs(_curCell).Units.Values) item.GameObject.SetActive(false);
            //foreach (var item in _eV.UnitEs(_curCell).ExtraTws.Values) item.GameObject.SetActive(false);
            //foreach (var item in _eV.UnitEs(_curCell).MainTws.Values) item.GameObject.SetActive(false);
            //foreach (var item in _eV.UnitEs(_curCell).BowCrossbows.Values) item.GameObject.SetActive(false);

            for (var i = 0; i < _needActive.Length; i++)
            {
                _needActive[i] = false;
                _needColor[i] = _color1;
            }


            if (_e.CellsC.Current == _curCell)
            {
                if (_e.CellClickTC.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = _e.CellsC.Current;
                    var selUnitT = _e.SelectedUnitE.UnitTC.UnitT;
                    var levT = _e.SelectedUnitE.LevelTC.LevelT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        //_eV.UnitEs(idx_cur).MainToolWeaponE(true, LevelTypes.First, ToolWeaponTypes.Axe).GameObject.SetActive(true);
                    }
                    else
                    {
                        //_eV.UnitE(idx_cur, true, levT, selUnitT).GameObject.SetActive(true);
                    }
                }
            }


            if (_e.UnitTC(_curCell).HaveUnit)
            {
                if (_e.UnitVisibleC(_curCell).IsVisible(_e.CurPlayerITC.PlayerT))
                {
                    var isSelected = _curCell == _e.CellsC.Selected;

                    var nextPlayer = _e.UnitPlayerTC(_curCell).PlayerT.NextPlayer();
                    var isVisForNext = _e.UnitVisibleC(_curCell).IsVisible(nextPlayer);



                    var unitT = _e.UnitT(_curCell);

                    _needColor[(byte)unitT] = isVisForNext ? _color1 : _color2;


                    if (unitT == UnitTypes.Pawn)
                    {
                        if (_e.MainToolWeaponTC(_curCell).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            //sr = _eV.UnitEs(_curCell).MainBowCrossbowE(isSelected, _e.MainTWLevelTC(_curCell).LevelT, _e.UnitIsRightArcherC(_curCell).IsRight);
                        }
                        else
                        {
                            //sr = _eV.UnitEs(_curCell).MainToolWeaponE(isSelected, _e.MainTWLevelTC(_curCell).LevelT, _e.MainToolWeaponTC(_curCell).ToolWeaponT);
                        }


                        //if (isVisForNext) SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 1);
                        //else SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 0.6f);

                        if (_e.ExtraToolWeaponTC(_curCell).HaveToolWeapon)
                        {
                            var twT = _e.ExtraToolWeaponTC(_curCell).ToolWeaponT;
                            var levT = _e.ExtraTWLevelTC(_curCell).LevelT;

                            //var sr2 = _eV.UnitEs(_curCell).ExtraToolWeaponE(isSelected, levT, twT);

                            //sr2.GameObject.SetActive(true);
                            //sr2.SR.color = isVisForNext ? Color1 : Color2;
                        }
                    }
                    else
                    {
                        _needActive[(byte)unitT] = true;
                    }
                }
            }

            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                if(unitT != UnitTypes.Pawn)
                {
                    

                    _eV.UnitEs(_curCell).UnitSRC(unitT).SR.color = _needColor[(byte)unitT];
                    


                    if (_eV.UnitEs(_curCell).UnitSRC(unitT).GO.activeSelf != _needActive[(byte)unitT])
                    {
                        _eV.UnitEs(_curCell).AnimationUnitC.Play();
                    }



                    if (_e.IsClicked)
                    {
                        if (_e.SelectedCell == _curCell)
                        {
                            _eV.UnitEs(_curCell).AnimationUnitC.Play();
                        }
                    }


                    _eV.UnitEs(_curCell).UnitSRC(unitT).GO.SetActive(_needActive[(byte)unitT]);
                }
            }

        }
    }
}
