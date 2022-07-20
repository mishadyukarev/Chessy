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

            if (_e.CurrentCellIdx == cellIdxForSync)
            {
                if (_e.CellClickT.Is(CellClickTypes.SetUnit))
                {
                    var selUnitT = _e.SelectedUnitC.UnitT;

                    if (selUnitT == UnitTypes.Pawn)
                    {
                        _needActiveMainTW[(byte)ToolsWeaponsWarriorTypes.Axe, (byte)LevelTypes.First] = true;
                    }
                }
            }

            if (_e.WhereViewDataUnitC(cellIdxForSync).HaveDataReference)
            {
                var dataUnitIdxCell = _e.WhereViewDataUnitC(cellIdxForSync).DataIdxCellP;

                if (_e.UnitT(dataUnitIdxCell).HaveUnit())
                {
                    if (_e.UnitVisibleC(dataUnitIdxCell).IsVisible(_aboutGameC.CurrentPlayerIType) || _e.UnitT(_e.SelectedCellIdx) == UnitTypes.Elfemale && _e.UnitPlayerT(_e.SelectedCellIdx) == _aboutGameC.CurrentPlayerIType && _e.UnitT(dataUnitIdxCell) == UnitTypes.King)
                    {
                        var isSelectedCell = dataUnitIdxCell == _e.SelectedCellIdx;

                        var nextPlayer = _e.UnitPlayerT(dataUnitIdxCell).NextPlayer();
                        var isVisibleForNextPlayer = _e.UnitVisibleC(dataUnitIdxCell).IsVisible(nextPlayer);



                        var unitT = _e.UnitT(dataUnitIdxCell);


                        if (unitT == UnitTypes.Pawn)
                        {
                            if (_e.MainToolWeaponT(dataUnitIdxCell).Is(ToolsWeaponsWarriorTypes.BowCrossbow))
                            {

                            }
                            else
                            {
                                _needActiveMainTW[(byte)_e.MainToolWeaponT(dataUnitIdxCell), (byte)_e.MainTWLevelT(dataUnitIdxCell)] = true;
                                _needColorMainTW[(byte)_e.MainToolWeaponT(dataUnitIdxCell), (byte)_e.MainTWLevelT(dataUnitIdxCell)] = isVisibleForNextPlayer ? ColorsValues.ColorStandart : ColorsValues.ColorTransparent;
                            }

                            if (_e.ExtraToolWeaponT(dataUnitIdxCell).HaveToolWeapon())
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