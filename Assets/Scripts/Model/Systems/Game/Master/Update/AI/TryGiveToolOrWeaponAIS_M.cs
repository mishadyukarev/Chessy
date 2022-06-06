using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit;
using UnityEngine;

namespace Chessy.Game
{
    sealed class TryGiveToolOrWeaponAIS_M : SystemModel
    {
        internal TryGiveToolOrWeaponAIS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {

        }

        internal void TryGive()
        {
            var playerBotT = PlayerTypes.Second;

            for (byte cellIdxStart = 0; cellIdxStart < StartValues.CELLS; cellIdxStart++)
            {
                if (eMG.UnitT(cellIdxStart) == UnitTypes.Pawn && eMG.UnitPlayerT(cellIdxStart) == playerBotT)
                {
                    if (!eMG.ExtraToolWeaponTC(cellIdxStart).HaveToolWeapon)
                    {
                        if (Random.Range(0f, 1f) <= 0.50f)
                        {
                            var levetTW = Random.Range(0f, 1f) <= 0.70f ? LevelTypes.First : LevelTypes.Second;

                            sMG.UnitSs.SetExtraToolWeapon(cellIdxStart, ToolWeaponTypes.Shield, levetTW, ToolWeaponValues.ShieldProtection(levetTW));
                        }
                        else
                        {
                            sMG.UnitSs.SetExtraToolWeapon(cellIdxStart, ToolWeaponTypes.Sword, LevelTypes.Second, 0);
                        }
                    }
                }
            }
        }
    }
}