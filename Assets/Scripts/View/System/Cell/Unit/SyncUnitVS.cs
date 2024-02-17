using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.System;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.Model
{
    sealed class SyncUnitVS : SystemViewAbstract
    {
        readonly EntitiesView _eV;

        readonly SpriteRenderer[,,] _bowCrossbowSRs = new SpriteRenderer[IndexCellsValues.CELLS, (byte)LevelTypes.End, 2];
        readonly GameObject[,,] _bowCrossbowGOs = new GameObject[IndexCellsValues.CELLS, (byte)LevelTypes.End, 2];
        readonly bool[,,] _needActiveBowCrossbow = new bool[IndexCellsValues.CELLS, (byte)LevelTypes.End, 2];
        readonly bool[,,] _waActivatedBowCrossbow = new bool[IndexCellsValues.CELLS, (byte)LevelTypes.End, 2];
        readonly Color[,,] _needColorBowCrossbow = new Color[IndexCellsValues.CELLS, (byte)LevelTypes.End, 2];




        readonly SpriteRenderer[,] _unitSRCs = new SpriteRenderer[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly GameObject[,] _unitGOCs = new GameObject[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly bool[,] _wasActivatedUnitBefore = new bool[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly bool[,] _needActiveUnit = new bool[IndexCellsValues.CELLS, (byte)UnitTypes.End];
        readonly Color[] _needColorUnit = new Color[(byte)UnitTypes.End];

        internal SyncUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eV = eV;

            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                for (var unitT = (UnitTypes)1; unitT < UnitTypes.End; unitT++)
                {
                    _unitSRCs[cellIdx, (byte)unitT] = _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).SRVC.SR;
                    _unitGOCs[cellIdx, (byte)unitT] = _eV.CellEs(cellIdx).UnitEs.UnitSRC(unitT).GOVC.GO;
                }



                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    foreach (var isRight in new[] { true, false })
                    {
                        var sr = _eV.CellEs(cellIdx).UnitEs.MainBowCrossbowSRC(levelT, isRight).SR;

                        _bowCrossbowSRs[cellIdx, (byte)levelT, isRight ? 1 : 0] = sr;
                        _bowCrossbowGOs[cellIdx, (byte)levelT, isRight ? 1 : 0] = sr.gameObject;
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

                foreach (var isRight in new[] { true, false })
                {
                    _needActiveBowCrossbow[cellIdx, levelTbyte, isRight ? 1 : 0] = false;
                    _needColorBowCrossbow[cellIdx, levelTbyte, isRight ? 1 : 0] = ColorsValues.ColorStandart;
                }
            }



            if (indexesCellsC.Current == cellIdx)
            {
                if (aboutGameC.CellClickType == CellClickTypes.SetUnit)
                {
                    var idx_cur = indexesCellsC.Current;
                    var selUnitT = selectedUnitC.UnitT;
                    var levT = selectedUnitC.LevelT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                    }
                    else
                    {
                        _needActiveUnit[cellIdx, (byte)selUnitT] = true;
                    }
                }
            }

            if (_unitWhereViewDataCs[cellIdx].HaveDataReference)
            {
                var dataUnitIdxCell = _unitWhereViewDataCs[cellIdx].DataIdxCellP;

                if (unitCs[dataUnitIdxCell].HaveUnit)
                {
                    if (_unitVisibleCs[dataUnitIdxCell].IsVisible(aboutGameC.CurrentPlayerIType) || unitCs[indexesCellsC.Selected].UnitType == UnitTypes.Elfemale && unitCs[indexesCellsC.Selected].PlayerType == aboutGameC.CurrentPlayerIType && unitCs[dataUnitIdxCell].UnitType == UnitTypes.King)
                    {
                        var nextPlayer = unitCs[dataUnitIdxCell].PlayerType.NextPlayer();
                        var isVisibleForNextPlayer = _unitVisibleCs[dataUnitIdxCell].IsVisible(nextPlayer);



                        var unitT = unitCs[dataUnitIdxCell].UnitType;

                        _needColorUnit[(byte)unitT] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;



                        if (unitT == UnitTypes.Pawn)
                        {
                            if (_mainTWC[dataUnitIdxCell].ToolWeaponType == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {
                                _needActiveBowCrossbow[cellIdx, (byte)_mainTWC[dataUnitIdxCell].LevelType, unitCs[dataUnitIdxCell].IsArcherDirectedToRightP ? 1 : 0] = true;
                                _needColorBowCrossbow[cellIdx, (byte)_mainTWC[dataUnitIdxCell].LevelType, unitCs[dataUnitIdxCell].IsArcherDirectedToRightP ? 1 : 0] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
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
                    var neededActive = _needActiveUnit[cellIdx, unitTbyte];
                    ref var wasActivated = ref _wasActivatedUnitBefore[cellIdx, unitTbyte];

                    if (wasActivated != neededActive) _unitGOCs[cellIdx, unitTbyte].SetActive(neededActive);
                    if (neededActive) _unitSRCs[cellIdx, unitTbyte].color = _needColorUnit[unitTbyte];


                    wasActivated = neededActive;
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;


                foreach (var isRight in new[] { true, false })
                {
                    var isRightByte = isRight ? 1 : 0;

                    var needActive = _needActiveBowCrossbow[cellIdx, levelTbyte, isRightByte];
                    ref var wasActivated = ref _waActivatedBowCrossbow[cellIdx, levelTbyte, isRightByte];

                    if (needActive != wasActivated) _bowCrossbowGOs[cellIdx, levelTbyte, isRightByte].SetActive(needActive);
                    if (needActive) _bowCrossbowSRs[cellIdx, levelTbyte, isRightByte].color = _needColorBowCrossbow[cellIdx, levelTbyte, isRightByte];

                    wasActivated = needActive;
                }
            }
        }
    }
}
