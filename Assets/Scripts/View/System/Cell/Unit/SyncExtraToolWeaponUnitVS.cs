using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncExtraToolWeaponUnitVS : SystemViewAbstract
    {
        readonly bool[,,] _needActiveExtraTW = new bool[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly bool[,,] _wasActivatedExtraTW = new bool[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly Color[,,] _needColorExtraTW = new Color[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly SpriteRenderer[,,] _sRs = new SpriteRenderer[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly GameObject[,,] _gOs = new GameObject[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];

        readonly byte[] _toolsWeaponsWarriorTypesByte = new[] { (byte)ToolsWeaponsWarriorTypes.Pick, (byte)ToolsWeaponsWarriorTypes.Shield, (byte)ToolsWeaponsWarriorTypes.Sword };


        internal SyncExtraToolWeaponUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                foreach (var twT in _toolsWeaponsWarriorTypesByte)
                {
                    for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                    {
                        var sR = eV.CellEs(cellIdxCurrent).UnitEs.ExtraToolWeaponSRC(levelT, (ToolsWeaponsWarriorTypes)twT).SR;
                        _sRs[cellIdxCurrent, (byte)twT, (byte)levelT] = sR;
                        _gOs[cellIdxCurrent, (byte)twT, (byte)levelT] = sR.gameObject;
                    }
                }
            }
        }

        internal override void Sync()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (CellC(cellIdxCurrent).IsBorder) continue;

                Sync(cellIdxCurrent);
            }
        }

        internal void Sync(in byte cellIdxForSync)
        {
            for (byte levelTbyte = 1; levelTbyte < (byte)LevelTypes.End; levelTbyte++)
            {
                for (var i = 0; i < _toolsWeaponsWarriorTypesByte.Length; i++)
                {
                    var twTByte = _toolsWeaponsWarriorTypesByte[i];

                    _needActiveExtraTW[cellIdxForSync, twTByte, levelTbyte] = false;
                    _needColorExtraTW[cellIdxForSync, twTByte, levelTbyte] = ColorsValues.ColorStandart;
                }
            }

            if (_unitWhereViewDataCs[cellIdxForSync].HaveDataReference)
            {
                var dataUnitIdxCell = _unitWhereViewDataCs[cellIdxForSync].DataIdxCellP;

                if (unitCs[dataUnitIdxCell].HaveUnit)
                {
                    if (_unitVisibleCs[dataUnitIdxCell].IsVisible(aboutGameC.CurrentPlayerIType) || unitCs[indexesCellsC.Selected].UnitType == UnitTypes.Elfemale && unitCs[indexesCellsC.Selected].PlayerType == aboutGameC.CurrentPlayerIType && unitCs[dataUnitIdxCell].UnitType == UnitTypes.King)
                    {
                        var nextPlayer = unitCs[dataUnitIdxCell].PlayerType.NextPlayer();
                        var isVisibleForNextPlayer = _unitVisibleCs[dataUnitIdxCell].IsVisible(nextPlayer);

                        var unitT = unitCs[dataUnitIdxCell].UnitType;

                        if (unitT == UnitTypes.Pawn)
                        {
                            if (_extraTWC[dataUnitIdxCell].HaveToolWeapon)
                            {
                                var twT = _extraTWC[dataUnitIdxCell].ToolWeaponType;
                                var levT = _extraTWC[dataUnitIdxCell].LevelType;

                                _needActiveExtraTW[cellIdxForSync, (byte)twT, (byte)levT] = true;
                                _needColorExtraTW[cellIdxForSync, (byte)twT, (byte)levT] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }
                        }
                    }
                }
            }

            for (var levelTbyte = 1; levelTbyte < (byte)LevelTypes.End; levelTbyte++)
            {
                for (var i = 0; i < _toolsWeaponsWarriorTypesByte.Length; i++)
                {
                    var twTByte = _toolsWeaponsWarriorTypesByte[i];

                    var needActive = _needActiveExtraTW[cellIdxForSync, twTByte, levelTbyte];
                    ref var wasActivated = ref _wasActivatedExtraTW[cellIdxForSync, twTByte, levelTbyte];

                    if (needActive != wasActivated) _gOs[cellIdxForSync, twTByte, levelTbyte].SetActive(needActive);
                    if (needActive) _sRs[cellIdxForSync, twTByte, levelTbyte].color = _needColorExtraTW[cellIdxForSync, twTByte, levelTbyte];

                    wasActivated = needActive;
                }
            }
        }
    }
}