using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.System;
using Chessy.View.UI.Entity;
using System.Collections.Generic;
using UnityEngine;
namespace Chessy.Model
{
    sealed class SyncUnitVS : SystemViewAbstract
    {
        readonly EntitiesView _eV;

        readonly bool[] _needActiveUnit = new bool[(byte)UnitTypes.End];
        readonly Color[] _needColorUnit = new Color[(byte)UnitTypes.End];

        readonly Dictionary<LevelTypes, bool[]> _needActiveMainTW = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, Color[]> _needColorMainTW = new Dictionary<LevelTypes, Color[]>();
        readonly Dictionary<LevelTypes, bool[]> _needActiveBowCrossbow = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, Color[]> _needColorBowCrossbow = new Dictionary<LevelTypes, Color[]>();
        readonly Dictionary<LevelTypes, bool[]> _needActiveExtraTW = new Dictionary<LevelTypes, bool[]>();
        readonly Dictionary<LevelTypes, Color[]> _needColorExtraTW = new Dictionary<LevelTypes, Color[]>();


        internal SyncUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;

            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                _needActiveMainTW.Add(levelT, new bool[(byte)ToolsWeaponsWarriorTypes.End]);
                _needColorMainTW.Add(levelT, new Color[(byte)ToolsWeaponsWarriorTypes.End]);
                _needActiveBowCrossbow.Add(levelT, new bool[2]);
                _needColorBowCrossbow.Add(levelT, new Color[2]);
                _needActiveExtraTW.Add(levelT, new bool[(byte)ToolsWeaponsWarriorTypes.End]);
                _needColorExtraTW.Add(levelT, new Color[(byte)ToolsWeaponsWarriorTypes.End]);
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                Sync(cellIdxCurrent);
            }
        }

        internal void Sync(in byte cellIdx)
        {
            for (var i = 0; i < _needActiveUnit.Length; i++)
            {
                _needActiveUnit[i] = false;
                _needColorUnit[i] = ColorsValues.ColorStandart;
            }
            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    _needActiveMainTW[levelT][(byte)toolWeaponT] = false;
                    _needColorMainTW[levelT][(byte)toolWeaponT] = ColorsValues.ColorStandart;
                }

                foreach (var isRight in new[] { true, false })
                {
                    _needActiveBowCrossbow[levelT][isRight ? 0 : 1] = false;
                    _needColorBowCrossbow[levelT][isRight ? 0 : 1] = ColorsValues.ColorStandart;
                }

                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Pick, ToolsWeaponsWarriorTypes.Shield, ToolsWeaponsWarriorTypes.Sword })
                {
                    _needActiveExtraTW[levelT][(byte)twT] = false;
                    _needColorExtraTW[levelT][(byte)twT] = ColorsValues.ColorStandart;
                }
            }



            if (_e.CurrentCellIdx == cellIdx)
            {
                if (_e.CellClickT.Is(CellClickTypes.SetUnit))
                {
                    var idx_cur = _e.CurrentCellIdx;
                    var selUnitT = _e.SelectedUnitC.UnitT;
                    var levT = _e.SelectedUnitC.LevelT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        _needActiveMainTW[LevelTypes.First][(byte)ToolsWeaponsWarriorTypes.Axe] = true;
                    }
                    else
                    {
                        _needActiveUnit[(byte)selUnitT] = true;
                    }
                }
            }

            if (_e.SkinInfoUnitC(cellIdx).HaveData)
            {
                var givenIdxCell = _e.SkinInfoUnitC(cellIdx).DataIdxCell;

                if (_e.UnitT(givenIdxCell).HaveUnit())
                {
                    if (_e.UnitVisibleC(givenIdxCell).IsVisible(_e.CurrentPlayerIT))
                    {
                        var isSelectedCell = givenIdxCell == _e.SelectedCellIdx;

                        var nextPlayer = _e.UnitPlayerT(givenIdxCell).NextPlayer();
                        var isVisibleForNextPlayer = _e.UnitVisibleC(givenIdxCell).IsVisible(nextPlayer);



                        var unitT = _e.UnitT(givenIdxCell);

                        _needColorUnit[(byte)unitT] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;



                        if (unitT == UnitTypes.Pawn)
                        {
                            if (_e.MainToolWeaponT(givenIdxCell).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                            {
                                _needActiveBowCrossbow[_e.MainTWLevelT(givenIdxCell)][_e.IsRightArcherUnit(givenIdxCell) ? 0 : 1] = true;
                                _needColorBowCrossbow[_e.MainTWLevelT(givenIdxCell)][_e.IsRightArcherUnit(givenIdxCell) ? 0 : 1] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }
                            else
                            {
                                var v = _e.MainTWLevelT(givenIdxCell);
                                var vv = _e.MainToolWeaponT(givenIdxCell);

                                _needActiveMainTW[_e.MainTWLevelT(givenIdxCell)][(byte)_e.MainToolWeaponT(givenIdxCell)] = true;
                                _needColorMainTW[_e.MainTWLevelT(givenIdxCell)][(byte)_e.MainToolWeaponT(givenIdxCell)] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }

                            if (_e.ExtraToolWeaponT(givenIdxCell).HaveToolWeapon())
                            {
                                var twT = _e.ExtraToolWeaponT(givenIdxCell);
                                var levT = _e.ExtraTWLevelT(givenIdxCell);

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
            }



            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                if (unitT != UnitTypes.Pawn)
                {
                    _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).SR.color = _needColorUnit[(byte)unitT];

                    if (_eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).GO.activeSelf != _needActiveUnit[(byte)unitT])
                    {
                        //_eV.CellEs(cellIdx).UnitEs.AnimationUnitC.Play();
                    }

                    //if (e.IsClicked)
                    //{
                    //    if (e.SelectedCell == cellIdxCurrent)
                    //    {
                    //        _eV.CellEs(cellIdxCurrent).UnitEs.AnimationUnitC.Play();
                    //    }
                    //}

                    _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).GO.SetActive(_needActiveUnit[(byte)unitT]);
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    if (_eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).GO.activeSelf != _needActiveMainTW[levelT][(byte)toolWeaponT])
                    {
                        //_eV.CellEs(cellIdx).UnitEs.AnimationUnitC.Play();
                    }

                    _eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).SetActiveGO(_needActiveMainTW[levelT][(byte)toolWeaponT]);
                    _eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).SR.color = _needColorMainTW[levelT][(byte)toolWeaponT];
                }


                foreach (var isRight in new[] { true, false })
                {
                    _eV.CellEs(cellIdx).UnitEs.MainBowCrossbowSRC(levelT, isRight).SetActiveGO(_needActiveBowCrossbow[levelT][isRight ? 0 : 1]);
                    _eV.CellEs(cellIdx).UnitEs.MainBowCrossbowSRC(levelT, isRight).SR.color = _needColorBowCrossbow[levelT][isRight ? 0 : 1];
                }

                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Pick, ToolsWeaponsWarriorTypes.Shield, ToolsWeaponsWarriorTypes.Sword })
                {
                    _eV.CellEs(cellIdx).UnitEs.ExtraToolWeaponSRC(levelT, twT).SetActiveGO(_needActiveExtraTW[levelT][(byte)twT]);
                    _eV.CellEs(cellIdx).UnitEs.ExtraToolWeaponSRC(levelT, twT).SR.color = _needColorExtraTW[levelT][(byte)twT];
                }
            }
        }
    }
}
