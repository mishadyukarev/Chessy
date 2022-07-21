using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using UnityEditor;
using UnityEngine;

namespace Chessy.View.System
{
    sealed class SyncMainToolWeaponUnitVS : SystemViewAbstract
    {
        readonly SpriteRenderer[,,] _mainToolWeapon = new SpriteRenderer[IndexCellsValues.CELLS, (byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly bool[,] _needActiveMainTW = new bool[(byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly bool[,] _wasActiveMainTW = new bool[(byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];
        readonly Color[,] _needColorMainTW = new Color[(byte)ToolsWeaponsWarriorTypes.End, (byte)LevelTypes.End];

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

        internal void Sync(in byte cellIdxForSync)
        {
            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponT in new[] { ToolsWeaponsWarriorTypes.Staff, ToolsWeaponsWarriorTypes.Axe })
                {
                    _needActiveMainTW[(byte)toolWeaponT, levelTbyte] = false;
                    _needColorMainTW[(byte)toolWeaponT, levelTbyte] = ColorsValues.ColorStandart;
                }
            }

            if (_cellsC.Current == cellIdxForSync)
            {
                if (_aboutGameC.CellClickType == CellClickTypes.SetUnit)
                {
                    var selUnitT = _selectedUnitC.UnitT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        _needActiveMainTW[(byte)ToolsWeaponsWarriorTypes.Axe, (byte)LevelTypes.First] = true;
                    }
                }
            }

            if (_unitWhereViewDataCs[cellIdxForSync].HaveDataReference)
            {
                var dataUnitIdxCell = _unitWhereViewDataCs[cellIdxForSync].DataIdxCellP;

                if (_unitCs[dataUnitIdxCell].HaveUnit)
                {
                    if (_unitVisibleCs[dataUnitIdxCell].IsVisible(_aboutGameC.CurrentPlayerIType) || _unitCs[_cellsC.Selected].UnitType == UnitTypes.Elfemale && _unitCs[_cellsC.Selected].PlayerType == _aboutGameC.CurrentPlayerIType && _unitCs[dataUnitIdxCell].UnitType == UnitTypes.King)
                    {
                        var isSelectedCell = dataUnitIdxCell == _cellsC.Selected;

                        var nextPlayer = _unitCs[dataUnitIdxCell].PlayerType.NextPlayer();
                        var isVisibleForNextPlayer = _unitVisibleCs[dataUnitIdxCell].IsVisible(nextPlayer);



                        var unitT = _unitCs[dataUnitIdxCell].UnitType;


                        if (unitT == UnitTypes.Pawn)
                        {
                            if (_mainTWC[dataUnitIdxCell].ToolWeaponType == ToolsWeaponsWarriorTypes.BowCrossbow)
                            {

                            }
                            else
                            {
                                _needActiveMainTW[(byte)_mainTWC[dataUnitIdxCell].ToolWeaponType, (byte)_mainTWC[dataUnitIdxCell].LevelType] = true;
                                _needColorMainTW[(byte)_mainTWC[dataUnitIdxCell].ToolWeaponType, (byte)_mainTWC[dataUnitIdxCell].LevelType] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }

                            if (_extraTWC[dataUnitIdxCell].HaveToolWeapon)
                            {

                            }
                        }
                    }
                }
            }


            for (var levelT = (LevelTypes)1; levelT < LevelTypes.End; levelT++)
            {
                var levelTbyte = (byte)levelT;

                foreach (var toolWeaponT in new byte[] { (byte)ToolsWeaponsWarriorTypes.Staff, (byte)ToolsWeaponsWarriorTypes.Axe })
                {
                    var needActive = _needActiveMainTW[toolWeaponT, levelTbyte];
                    ref var wasActivated = ref _wasActiveMainTW[toolWeaponT, levelTbyte];

                    if (wasActivated != needActive) _mainToolWeapon[cellIdxForSync, toolWeaponT, levelTbyte].gameObject.SetActive(needActive);
                    if (needActive) _mainToolWeapon[cellIdxForSync, toolWeaponT, levelTbyte].color = _needColorMainTW[toolWeaponT, levelTbyte];

                    wasActivated = needActive;
                }
            }
        }
    }
}