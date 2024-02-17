using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncMainToolWeaponUnitVS : SystemViewAbstract
    {
        readonly SpriteRenderer[,,] _mainToolWeapon = new SpriteRenderer[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly bool[,,] _needActiveMainTW = new bool[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly bool[,,] _wasActiveMainTW = new bool[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly Color[,,] _needColorMainTW = new Color[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];

        internal SyncMainToolWeaponUnitVS(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            for (byte cellIdx = 0; cellIdx < IndexCellsValues.CELLS; cellIdx++)
            {
                for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
                {
                    for (var toolWeaponT = (ToolsWeaponsWarriorTypes)1; toolWeaponT < ToolsWeaponsWarriorTypes.End; toolWeaponT++)
                    {
                        _mainToolWeapon[cellIdx, (byte)toolWeaponT, (byte)levelT] = eV.CellEs(cellIdx).UnitEs.MainToolWeaponSRC(levelT, toolWeaponT).SR;
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

        internal void Sync(in byte cellIdxForSync_0)
        {
            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    _needActiveMainTW[cellIdxForSync_0, (byte)toolWeaponT, levelTbyte] = false;
                    _needColorMainTW[cellIdxForSync_0, (byte)toolWeaponT, levelTbyte] = ColorsValues.ColorStandart;
                }
            }

            if (indexesCellsC.Current == cellIdxForSync_0)
            {
                if (aboutGameC.CellClickType == CellClickTypes.SetUnit)
                {
                    var selUnitT = selectedUnitC.UnitT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        _needActiveMainTW[cellIdxForSync_0, (byte)ToolsWeaponsWarriorTypes.Axe, (byte)LevelTypes.First] = true;
                    }
                }
            }

            if (_unitWhereViewDataCs[cellIdxForSync_0].HaveDataReference)
            {
                var dataUnitIdxCell_1 = _unitWhereViewDataCs[cellIdxForSync_0].DataIdxCellP;

                var unitT = unitCs[dataUnitIdxCell_1].UnitType;

                if (unitT == UnitTypes.Pawn)
                {
                    if (_mainTWC[dataUnitIdxCell_1].ToolWeaponType != ToolsWeaponsWarriorTypes.BowCrossbow)
                    {
                        if (_unitVisibleCs[dataUnitIdxCell_1].IsVisible(aboutGameC.CurrentPlayerIType))
                        {
                            var nextPlayer = unitCs[dataUnitIdxCell_1].PlayerType.NextPlayer();
                            var isVisibleForNextPlayer = _unitVisibleCs[dataUnitIdxCell_1].IsVisible(nextPlayer);

                            _needActiveMainTW[cellIdxForSync_0, (byte)_mainTWC[dataUnitIdxCell_1].ToolWeaponType, (byte)_mainTWC[dataUnitIdxCell_1].LevelType] = true;
                            _needColorMainTW[cellIdxForSync_0, (byte)_mainTWC[dataUnitIdxCell_1].ToolWeaponType, (byte)_mainTWC[dataUnitIdxCell_1].LevelType] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                        }
                    }
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponTByte in new byte[] { (byte)ToolsWeaponsWarriorTypes.Staff, (byte)ToolsWeaponsWarriorTypes.Axe })
                {
                    var needActive = _needActiveMainTW[cellIdxForSync_0, toolWeaponTByte, levelTbyte];
                    ref var wasActivated = ref _wasActiveMainTW[cellIdxForSync_0, toolWeaponTByte, levelTbyte];

                    if (wasActivated != needActive) _mainToolWeapon[cellIdxForSync_0, toolWeaponTByte, levelTbyte].gameObject.SetActive(needActive);
                    if (needActive) _mainToolWeapon[cellIdxForSync_0, toolWeaponTByte, levelTbyte].color = _needColorMainTW[cellIdxForSync_0, toolWeaponTByte, levelTbyte];

                    wasActivated = needActive;
                }
            }
        }
    }
}