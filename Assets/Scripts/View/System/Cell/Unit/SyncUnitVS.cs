using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.Entity;
using Chessy.View.System;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncUnitVS : SystemViewAbstract
    {
        readonly EntitiesView _eV;

        readonly UnitVEs[] _unitVEs = new UnitVEs[IndexCellsValues.CELLS];

        readonly bool[,] _wasActivatedUnitBefore = new bool[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly bool[,] _needActiveUnit = new bool[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly Color[] _needColorUnit = new Color[(byte)UnitTypes.End];


        readonly bool[,] _needActiveMainTW = new bool[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
        readonly Color[,] _needColorMainTW = new Color[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
        readonly bool[,] _needActiveBowCrossbow = new bool[(byte)LevelTypes.End, 2];
        readonly Color[,] _needColorBowCrossbow = new Color[(byte)LevelTypes.End, 2];
        readonly bool[,] _needActiveExtraTW = new bool[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];
        readonly Color[,] _needColorExtraTW = new Color[(byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];


        readonly SpriteRenderer[,] _unitSRCs = new SpriteRenderer[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly GameObject[,] _unitGOCs = new GameObject[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly SpriteRendererVC[,,] _mainToolWeapon = new SpriteRendererVC[IndexCellsValues.CELLS, (byte)LevelTypes.End, (byte)ToolsWeaponsWarriorTypes.End];

        internal SyncUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                _unitVEs[cellIdx] = _eV.CellEs(cellIdx).UnitEs;

                for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
                {
                    _unitSRCs[cellIdx, (byte)unitT] = _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).SRVC.SR;
                    _unitGOCs[cellIdx, (byte)unitT] = _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).GOVC.GO;
                }

                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var toolWeaponT = (ToolsWeaponsWarriorTypes)1; toolWeaponT < ToolsWeaponsWarriorTypes.End; toolWeaponT++)
                    {
                        _mainToolWeapon[cellIdx, (byte)levelT, (byte)toolWeaponT] = _eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT);
                    }
                }
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
            for (var i = 0; i < _needColorUnit.Length; i++)
            {
                _needActiveUnit[cellIdx, i] = false;
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
                        _needActiveUnit[cellIdx, (byte)selUnitT] = true;
                    }
                }
            }

            if (_e.SkinInfoUnitC(cellIdx).HaveDataReference)
            {
                var givenIdxCell = _e.SkinInfoUnitC(cellIdx).DataIdxCell;

                if (_e.UnitT(givenIdxCell).HaveUnit())
                {
                    if (_e.UnitVisibleC(givenIdxCell).IsVisible(_e.CurrentPlayerIT) || _e.CellsC.IsSelectedCell && _e.UnitT(_e.SelectedCellIdx) == UnitTypes.Elfemale)
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
                            _needActiveUnit[cellIdx, (byte)unitT] = true;
                        }
                    }
                }
            }



            for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
            {
                var unitTbyte = (byte)unitT;

                if (unitT != UnitTypes.Pawn)
                {
                    _unitSRCs[cellIdx, unitTbyte].color = _needColorUnit[unitTbyte];

                    var neededActive = _needActiveUnit[cellIdx, unitTbyte];

                    if (_wasActivatedUnitBefore[cellIdx, unitTbyte] != neededActive) _unitGOCs[cellIdx, unitTbyte].SetActive(neededActive);

                    _wasActivatedUnitBefore[cellIdx, unitTbyte] = neededActive;
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {

                    _mainToolWeapon[cellIdx, levelTbyte, (byte)toolWeaponT].TrySetActiveGO(_needActiveMainTW[levelTbyte, (byte)toolWeaponT]);
                    _mainToolWeapon[cellIdx, levelTbyte, (byte)toolWeaponT].Color = _needColorMainTW[levelTbyte, (byte)toolWeaponT];


                    //_unitVEs[cellIdx].MainToolWeaponSRC(levelT, toolWeaponT).SetActiveGO();
                    //_unitVEs[cellIdx].MainToolWeaponSRC(levelT, toolWeaponT).SetColor();
                }


                foreach (var isRight in new[] { true, false })
                {
                    _unitVEs[cellIdx].MainBowCrossbowSRC(levelT, isRight).TrySetActiveGO(_needActiveBowCrossbow[levelTbyte, isRight ? 1 : 0]);
                    _unitVEs[cellIdx].MainBowCrossbowSRC(levelT, isRight).Color = _needColorBowCrossbow[levelTbyte, isRight ? 1 : 0];
                }

                foreach (var twT in new[] { ToolsWeaponsWarriorTypes.Pick, ToolsWeaponsWarriorTypes.Shield, ToolsWeaponsWarriorTypes.Sword })
                {
                    _unitVEs[cellIdx].ExtraToolWeaponSRC(levelT, twT).TrySetActiveGO(_needActiveExtraTW[levelTbyte, (byte)twT]);
                    _unitVEs[cellIdx].ExtraToolWeaponSRC(levelT, twT).Color = _needColorExtraTW[levelTbyte, (byte)twT];
                }
            }
        }
    }
}
