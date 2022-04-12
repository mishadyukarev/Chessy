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


        readonly bool[] _needActiveUnit = new bool[(byte)UnitTypes.End];
        readonly Color[] _needColorUnit = new Color[(byte)UnitTypes.End];

        readonly Dictionary<LevelTypes, bool[]> _needActiveMainTW = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, bool[]> _needActiveBowCrossbow = new Dictionary<LevelTypes, bool[]>();

        readonly byte _currentCell;


        internal SyncUnitVS(in byte cell, in EntitiesViewGame eViewGame, in EntitiesModelGame eModelGame)
        {
            _eV = eViewGame;
            _e = eModelGame;
            _currentCell = cell;

            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                _needActiveMainTW.Add(levelT, new bool[(byte)ToolWeaponTypes.End]);
                _needActiveBowCrossbow.Add(levelT, new bool[2]);
            }

        }

        internal void Sync()
        {
            for (var i = 0; i < _needActiveUnit.Length; i++)
            {
                _needActiveUnit[i] = false;
                _needColorUnit[i] = _color1;
            }
            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                foreach (var toolWeaponT in new[] { ToolWeaponTypes.Staff, ToolWeaponTypes.Axe })
                {
                    _needActiveMainTW[levelT][(byte)toolWeaponT] = false;
                }

                foreach (var isRight in new[] { true, false })
                {
                    _needActiveBowCrossbow[levelT][isRight ? 0 : 1] = false;
                }
            }



            if (_e.CellsC.Current == _currentCell)
            {
                if (_e.CellClickTC.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = _e.CellsC.Current;
                    var selUnitT = _e.SelectedUnitE.UnitTC.UnitT;
                    var levT = _e.SelectedUnitE.LevelTC.LevelT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        _needActiveMainTW[LevelTypes.First][(byte)ToolWeaponTypes.Axe] = true;
                    }
                    else
                    {
                        _needActiveUnit[(byte)selUnitT] = true;
                    }
                }
            }


            if (_e.UnitTC(_currentCell).HaveUnit)
            {
                if (_e.UnitVisibleC(_currentCell).IsVisible(_e.CurPlayerITC.PlayerT))
                {
                    var isSelectedCell = _currentCell == _e.SelectedCell;

                    var nextPlayer = _e.UnitPlayerTC(_currentCell).PlayerT.NextPlayer();
                    var isVisForNext = _e.UnitVisibleC(_currentCell).IsVisible(nextPlayer);



                    var unitT = _e.UnitT(_currentCell);

                    _needColorUnit[(byte)unitT] = isVisForNext ? _color1 : _color2;


                    if (unitT == UnitTypes.Pawn)
                    {
                        if (_e.MainToolWeaponTC(_currentCell).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            _needActiveBowCrossbow[_e.MainTWLevelT(_currentCell)][_e.UnitIsRightArcherC(_currentCell).IsRight ? 0 : 1] = true;
                        }
                        else
                        {
                            _needActiveMainTW[_e.MainTWLevelT(_currentCell)][(byte)_e.MainToolWeaponT(_currentCell)] = true;
                        }


                        //if (isVisForNext) SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 1);
                        //else SR.SR.color = new Color(SR.Color.r, SR.Color.g, SR.Color.b, 0.6f);

                        if (_e.ExtraToolWeaponTC(_currentCell).HaveToolWeapon)
                        {
                            var twT = _e.ExtraToolWeaponTC(_currentCell).ToolWeaponT;
                            var levT = _e.ExtraTWLevelTC(_currentCell).LevelT;

                            //var sr2 = _eV.UnitEs(_curCell).ExtraToolWeaponE(isSelected, levT, twT);

                            //sr2.GameObject.SetActive(true);
                            //sr2.SR.color = isVisForNext ? Color1 : Color2;
                        }
                    }
                    else
                    {
                        _needActiveUnit[(byte)unitT] = true;
                    }
                }
            }




            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                if(unitT != UnitTypes.Pawn)
                {
                    _eV.UnitEs(_currentCell).UnitSRC(unitT).SR.color = _needColorUnit[(byte)unitT];
                    
                    if (_eV.UnitEs(_currentCell).UnitSRC(unitT).GO.activeSelf != _needActiveUnit[(byte)unitT])
                    {
                        _eV.UnitEs(_currentCell).AnimationUnitC.Play();
                    }

                    if (_e.IsClicked)
                    {
                        if (_e.SelectedCell == _currentCell)
                        {
                            _eV.UnitEs(_currentCell).AnimationUnitC.Play();
                        }
                    }

                    _eV.UnitEs(_currentCell).UnitSRC(unitT).GO.SetActive(_needActiveUnit[(byte)unitT]);
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                foreach (var toolWeaponT in new[] { ToolWeaponTypes.Staff, ToolWeaponTypes.Axe })
                {
                    if (_eV.UnitEs(_currentCell).MainToolWeaponSRC(levelT, toolWeaponT).GO.activeSelf != _needActiveMainTW[levelT][(byte)toolWeaponT])
                    {
                        _eV.UnitEs(_currentCell).AnimationUnitC.Play();
                    }

                    _eV.UnitEs(_currentCell).MainToolWeaponSRC(levelT, toolWeaponT).SetActive(_needActiveMainTW[levelT][(byte)toolWeaponT]);
                }


                foreach (var isRight in new[] { true, false })
                {
                    _eV.UnitEs(_currentCell).MainBowCrossbowSRC(levelT, isRight).SetActive(_needActiveBowCrossbow[levelT][isRight ? 0 : 1]);
                }
            }
        }
    }
}
