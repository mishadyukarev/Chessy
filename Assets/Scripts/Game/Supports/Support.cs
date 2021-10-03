using Photon.Pun;
using Photon.Realtime;
using System;

namespace Scripts.Game
{
    internal static class Support
    {
        internal static bool Is(this ToolWeaponTypes leftToolWeaponType, ToolWeaponTypes rightToolWeaponType) => leftToolWeaponType == rightToolWeaponType;
        internal static bool IsForArcher(this ToolWeaponTypes toolWeaponType)
        {
            switch (toolWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    return false;

                case ToolWeaponTypes.Axe:
                    return false;

                case ToolWeaponTypes.Pick:
                    return false;

                case ToolWeaponTypes.Sword:
                    return false;

                case ToolWeaponTypes.Bow:
                    return true;

                case ToolWeaponTypes.Crossbow:
                    return true;

                default:
                    throw new Exception();
            }
        }
        internal static bool IsForPawn(this ToolWeaponTypes toolWeaponType)
        {
            switch (toolWeaponType)
            {
                case ToolWeaponTypes.None:
                    throw new Exception();

                case ToolWeaponTypes.Hoe:
                    throw new Exception();

                case ToolWeaponTypes.Axe:
                    return true;

                case ToolWeaponTypes.Pick:
                    return true;

                case ToolWeaponTypes.Sword:
                    return true;

                case ToolWeaponTypes.Bow:
                    return false;

                case ToolWeaponTypes.Crossbow:
                    return false;

                default:
                    throw new Exception();
            }
        }


        internal static bool Is(this UnitTypes leftUnitType, UnitTypes rightUnitType) => leftUnitType == rightUnitType;
        internal static bool Is(this UnitTypes leftUnitType, UnitTypes[] rightUnitTypes)
        {
            foreach (var curUnitType in rightUnitTypes)
                if (Is(leftUnitType, curUnitType)) return true;
            return false;
        }


        internal static PlayerTypes GetPlayerType(this Player player)
        {
            if (player.IsMasterClient == true) return PlayerTypes.First;
            else return PlayerTypes.Second;
        }
        internal static Player GetPlayerType(this PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.First) return PhotonNetwork.PlayerList[0];//PhotonNetwork.PlayerList[0];
            else return PhotonNetwork.PlayerList[1];
        }
    }
}
