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

        readonly bool[,] _needActiveMainTW = new bool[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
        readonly Color[,] _needColorMainTW = new Color[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
        readonly bool[,] _needActiveBowCrossbow = new bool[(byte)LevelTypes.End, 2];
        readonly Color[,] _needColorBowCrossbow = new Color[(byte)LevelTypes.End, 2];
        readonly bool[,] _needActiveExtraTW = new bool[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
        readonly Color[,] _needColorExtraTW = new Color[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];


        internal SyncUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;
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
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    _needActiveMainTW[levelTbyte, (byte)toolWeaponT] = false;
                    _needColorMainTW[levelTbyte, (byte)toolWeaponT] = ColorsValues.ColorStandart;
                }

                foreach (var isRight in new[] { true, false })
                {
                    _needActiveBowCrossbow[levelTbyte, isRight ? 1 : 0] = false;
                    _needColorBowCrossbow[levelTbyte, isRight ? 1 : 0] = ColorsValues.ColorStandart;
                }

                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Pick, ToolsWeaponsWarriorTypes.Shield, ToolsWeaponsWarriorTypes.Sword })
                {
                    _needActiveExtraTW[levelTbyte, (byte)twT] = false;
                    _needColorExtraTW[levelTbyte, (byte)twT] = ColorsValues.ColorStandart;
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
                        _needActiveMainTW[(byte)LevelTypes.First, (byte)ToolsWeaponsWarriorTypes.Axe] = true;
                    }
                    else
                    {
                        _needActiveUnit[(byte)selUnitT] = true;
                    }
                }
            }

            if (_e.SkinInfoUnitC(cellIdx).HaveDataReference)
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
                                _needActiveBowCrossbow[(byte)_e.MainTWLevelT(givenIdxCell), _e.IsRightArcherUnit(givenIdxCell) ? 1 : 0] = true;
                                _needColorBowCrossbow[(byte)_e.MainTWLevelT(givenIdxCell), _e.IsRightArcherUnit(givenIdxCell) ? 1 : 0] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }
                            else
                            {
                                var v = _e.MainTWLevelT(givenIdxCell);
                                var vv = _e.MainToolWeaponT(givenIdxCell);

                                _needActiveMainTW[(byte)_e.MainTWLevelT(givenIdxCell), (byte)_e.MainToolWeaponT(givenIdxCell)] = true;
                                _needColorMainTW[(byte)_e.MainTWLevelT(givenIdxCell), (byte)_e.MainToolWeaponT(givenIdxCell)] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }

                            if (_e.ExtraToolWeaponT(givenIdxCell).HaveToolWeapon())
                            {
                                var twT = _e.ExtraToolWeaponT(givenIdxCell);
                                var levT = _e.ExtraTWLevelT(givenIdxCell);

                                _needActiveExtraTW[(byte)levT, (byte)twT] = true;
                                _needColorExtraTW[(byte)levT, (byte)twT] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
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
                    _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).SetColor(_needColorUnit[(byte)unitT]);
                    _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).SetActiveGO(_needActiveUnit[(byte)unitT]);
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    //if (_eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).GO.activeSelf != _needActiveMainTW[levelT][(byte)toolWeaponT])
                    //{
                    //    //_eV.CellEs(cellIdx).UnitEs.AnimationUnitC.Play();
                    //}

                    _eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).SetActiveGO(_needActiveMainTW[levelTbyte, (byte)toolWeaponT]);
                    _eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).SetColor(_needColorMainTW[levelTbyte, (byte)toolWeaponT]);
                }


                foreach (var isRight in new[] { true, false })
                {
                    _eV.CellEs(cellIdx).UnitEs.MainBowCrossbowSRC(levelT, isRight).SetActiveGO(_needActiveBowCrossbow[levelTbyte, isRight ? 1 : 0]);
                    _eV.CellEs(cellIdx).UnitEs.MainBowCrossbowSRC(levelT, isRight).SetColor(_needColorBowCrossbow[levelTbyte, isRight ? 1 : 0]);
                }

                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Pick, ToolsWeaponsWarriorTypes.Shield, ToolsWeaponsWarriorTypes.Sword })
                {
                    _eV.CellEs(cellIdx).UnitEs.ExtraToolWeaponSRC(levelT, twT).SetActiveGO(_needActiveExtraTW[levelTbyte, (byte)twT]);
                    _eV.CellEs(cellIdx).UnitEs.ExtraToolWeaponSRC(levelT, twT).SetColor(_needColorExtraTW[levelTbyte, (byte)twT]);
                }
            }
        }
    }
}
