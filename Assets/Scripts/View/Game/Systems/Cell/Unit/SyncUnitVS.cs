using Chessy.Game.Entity.View.Cell;
using Chessy.Game.Extensions;
using Chessy.Game.Model.Entity;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    sealed class SyncUnitVS : SystemViewCellGameAbs
    {
        readonly UnitVEs _unitVEs;

        readonly bool[] _needActiveUnit = new bool[(byte)UnitTypes.End];
        readonly Color[] _needColorUnit = new Color[(byte)UnitTypes.End];

        readonly Dictionary<LevelTypes, bool[]> _needActiveMainTW = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, Color[]> _needColorMainTW = new Dictionary<LevelTypes, Color[]>();
        readonly Dictionary<LevelTypes, bool[]> _needActiveBowCrossbow = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, Color[]> _needColorBowCrossbow = new Dictionary<LevelTypes, Color[]>();
        readonly Dictionary<LevelTypes, bool[]> _needActiveExtraTW = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, Color[]> _needColorExtraTW = new Dictionary<LevelTypes, Color[]>();


        internal SyncUnitVS(in UnitVEs unitVEs, in byte currentCell, in EntitiesModelGame eMG) : base(currentCell, eMG)
        {
            _unitVEs = unitVEs;

            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                _needActiveMainTW.Add(levelT, new bool[(byte)ToolWeaponTypes.End]);
                _needColorMainTW.Add(levelT, new Color[(byte)ToolWeaponTypes.End]);
                _needActiveBowCrossbow.Add(levelT, new bool[2]);
                _needColorBowCrossbow.Add(levelT, new Color[2]);
                _needActiveExtraTW.Add(levelT, new bool[(byte)ToolWeaponTypes.End]);
                _needColorExtraTW.Add(levelT, new Color[(byte)ToolWeaponTypes.End]);
            }
        }

        internal override void Sync()
        {
            for (var i = 0; i < _needActiveUnit.Length; i++)
            {
                _needActiveUnit[i] = false;
                _needColorUnit[i] = ColorsValues.ColorStandart;
            }
            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                foreach (var toolWeaponT in new[] { ToolWeaponTypes.Staff, ToolWeaponTypes.Axe })
                {
                    _needActiveMainTW[levelT][(byte)toolWeaponT] = false;
                    _needColorMainTW[levelT][(byte)toolWeaponT] = ColorsValues.ColorStandart;
                }

                foreach (var isRight in new[] { true, false })
                {
                    _needActiveBowCrossbow[levelT][isRight ? 0 : 1] = false;
                    _needColorBowCrossbow[levelT][isRight ? 0 : 1] = ColorsValues.ColorStandart;
                }

                foreach (var twT in new[] { ToolWeaponTypes.Pick, ToolWeaponTypes.Shield, ToolWeaponTypes.Sword })
                {
                    _needActiveExtraTW[levelT][(byte)twT] = false;
                    _needColorExtraTW[levelT][(byte)twT] = ColorsValues.ColorStandart;
                }
            }



            if (e.CellsC.Current == _currentCell)
            {
                if (e.CellClickTC.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = e.CellsC.Current;
                    var selUnitT = e.SelectedUnitE.UnitTC.UnitT;
                    var levT = e.SelectedUnitE.LevelTC.LevelT;

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


            if (e.UnitTC(_currentCell).HaveUnit)
            {
                if (e.UnitVisibleC(_currentCell).IsVisible(e.CurPlayerITC.PlayerT))
                {
                    var isSelectedCell = _currentCell == e.SelectedCell;

                    var nextPlayer = e.UnitPlayerTC(_currentCell).PlayerT.NextPlayer();
                    var isVisibleForNextPlayer = e.UnitVisibleC(_currentCell).IsVisible(nextPlayer);



                    var unitT = e.UnitT(_currentCell);

                    _needColorUnit[(byte)unitT] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;



                    if (unitT == UnitTypes.Pawn)
                    {
                        if (e.MainToolWeaponTC(_currentCell).Is(ToolWeaponTypes.BowCrossbow))
                        {
                            _needActiveBowCrossbow[e.MainTWLevelT(_currentCell)][e.UnitIsRightArcherC(_currentCell).IsRight ? 0 : 1] = true;
                            _needColorBowCrossbow[e.MainTWLevelT(_currentCell)][e.UnitIsRightArcherC(_currentCell).IsRight ? 0 : 1] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                        }
                        else
                        {
                            _needActiveMainTW[e.MainTWLevelT(_currentCell)][(byte)e.MainToolWeaponT(_currentCell)] = true;
                            _needColorMainTW[e.MainTWLevelT(_currentCell)][(byte)e.MainToolWeaponT(_currentCell)] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                        }

                        if (e.ExtraToolWeaponTC(_currentCell).HaveToolWeapon)
                        {
                            var twT = e.ExtraToolWeaponTC(_currentCell).ToolWeaponT;
                            var levT = e.ExtraTWLevelTC(_currentCell).LevelT;

                            _needActiveExtraTW[levT][(byte)twT] = true;
                            _needColorExtraTW[levT][(byte)twT] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
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
                if (unitT != UnitTypes.Pawn)
                {
                    _unitVEs.UnitSRC(unitT).SR.color = _needColorUnit[(byte)unitT];

                    if (_unitVEs.UnitSRC(unitT).GO.activeSelf != _needActiveUnit[(byte)unitT])
                    {
                        _unitVEs.AnimationUnitC.Play();
                    }

                    //if (e.IsClicked)
                    //{
                    //    if (e.SelectedCell == _currentCell)
                    //    {
                    //        _unitVEs.AnimationUnitC.Play();
                    //    }
                    //}

                    _unitVEs.UnitSRC(unitT).GO.SetActive(_needActiveUnit[(byte)unitT]);
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                foreach (var toolWeaponT in new[] { ToolWeaponTypes.Staff, ToolWeaponTypes.Axe })
                {
                    if (_unitVEs.MainToolWeaponSRC(levelT, toolWeaponT).GO.activeSelf != _needActiveMainTW[levelT][(byte)toolWeaponT])
                    {
                        _unitVEs.AnimationUnitC.Play();
                    }

                    _unitVEs.MainToolWeaponSRC(levelT, toolWeaponT).SetActive(_needActiveMainTW[levelT][(byte)toolWeaponT]);
                    _unitVEs.MainToolWeaponSRC(levelT, toolWeaponT).SR.color = _needColorMainTW[levelT][(byte)toolWeaponT];
                }


                foreach (var isRight in new[] { true, false })
                {
                    _unitVEs.MainBowCrossbowSRC(levelT, isRight).SetActive(_needActiveBowCrossbow[levelT][isRight ? 0 : 1]);
                    _unitVEs.MainBowCrossbowSRC(levelT, isRight).SR.color = _needColorBowCrossbow[levelT][isRight ? 0 : 1];
                }

                foreach (var twT in new[] { ToolWeaponTypes.Pick, ToolWeaponTypes.Shield, ToolWeaponTypes.Sword })
                {
                    _unitVEs.ExtraToolWeaponSRC(levelT, twT).SetActive(_needActiveExtraTW[levelT][(byte)twT]);
                    _unitVEs.ExtraToolWeaponSRC(levelT, twT).SR.color = _needColorExtraTW[levelT][(byte)twT];
                }
            }
        }
    }
}
