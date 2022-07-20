using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using UnityEditor;
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
                if (_cellCs[cellIdxCurrent].IsBorder) continue;

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

            if (_e.WhereViewDataUnitC(cellIdxForSync).HaveDataReference)
            {
                var dataUnitIdxCell = _e.WhereViewDataUnitC(cellIdxForSync).DataIdxCellP;

                if (_e.UnitT(dataUnitIdxCell).HaveUnit())
                {
                    if (_e.UnitVisibleC(dataUnitIdxCell).IsVisible(_aboutGameC.CurrentPlayerIType) || _e.UnitT(_e.SelectedCellIdx) == UnitTypes.Elfemale && _e.UnitPlayerT(_e.SelectedCellIdx) == _aboutGameC.CurrentPlayerIType && _e.UnitT(dataUnitIdxCell) == UnitTypes.King)
                    {
                        var nextPlayer = _e.UnitPlayerT(dataUnitIdxCell).NextPlayer();
                        var isVisibleForNextPlayer = _e.UnitVisibleC(dataUnitIdxCell).IsVisible(nextPlayer);

                        var unitT = _e.UnitT(dataUnitIdxCell);

                        if (unitT == UnitTypes.Pawn)
                        {
                            if (_e.ExtraToolWeaponT(dataUnitIdxCell).HaveToolWeapon())
                            {
                                var twT = _e.ExtraToolWeaponT(dataUnitIdxCell);
                                var levT = _e.ExtraTWLevelT(dataUnitIdxCell);

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