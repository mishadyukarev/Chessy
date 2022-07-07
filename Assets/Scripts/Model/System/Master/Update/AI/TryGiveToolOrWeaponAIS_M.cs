using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using UnityEngine;
namespace Chessy.Model
{
    sealed class TryGiveToolOrWeaponAIS_M : SystemModelAbstract
    {
        internal TryGiveToolOrWeaponAIS_M(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {

        }

        internal void TryGive()
        {
            var playerBotT = PlayerTypes.Second;

            for (byte cellIdxStart = 0; cellIdxStart < IndexCellsValues.CELLS; cellIdxStart++)
            {
                if (_e.UnitT(cellIdxStart) == UnitTypes.Pawn && _e.UnitPlayerT(cellIdxStart) == playerBotT)
                {
                    if (!_e.ExtraToolWeaponT(cellIdxStart).HaveToolWeapon())
                    {
                        if (Random.Range(0f, 1f) <= 0.50f)
                        {
                            var levetTW = Random.Range(0f, 1f) <= 0.70f ? LevelTypes.First : LevelTypes.Second;

                            _e.UnitExtraTWC(cellIdxStart).Set(ToolsWeaponsWarriorTypes.Shield, levetTW, ValuesChessy.MaxShieldProtection(levetTW));
                        }
                        else
                        {
                            _e.UnitExtraTWC(cellIdxStart).Set(ToolsWeaponsWarriorTypes.Sword, LevelTypes.Second, 0);
                        }
                    }
                }
            }
        }
    }
}