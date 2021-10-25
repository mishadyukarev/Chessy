using Photon.Pun;
using Photon.Realtime;
using System;

namespace Scripts.Game
{
    internal static class Support
    {
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

        internal static byte AmountTypes(Type type) => (byte)Enum.GetNames(type).Length;

        internal static ResourceTypes MinResType => (ResourceTypes)1;
        internal static ResourceTypes MaxResType => (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length;

        internal static PlayerTypes MinPlayerType => (PlayerTypes)1;
        internal static PlayerTypes MaxPlayerType => (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length;
    }
}
