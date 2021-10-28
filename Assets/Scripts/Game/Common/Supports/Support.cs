using Photon.Pun;
using Photon.Realtime;
using System;

namespace Scripts.Game
{
    public static class Support
    {
        public static bool Is(this UnitTypes leftUnitType, UnitTypes rightUnitType) => leftUnitType == rightUnitType;
        public static bool Is(this UnitTypes leftUnitType, UnitTypes[] rightUnitTypes)
        {
            foreach (var curUnitType in rightUnitTypes)
                if (Is(leftUnitType, curUnitType)) return true;
            return false;
        }


        public static PlayerTypes GetPlayerType(this Player player)
        {
            if (player.IsMasterClient == true) return PlayerTypes.First;
            else return PlayerTypes.Second;
        }
        public static Player GetPlayerType(this PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.First) return PhotonNetwork.PlayerList[0];//PhotonNetwork.PlayerList[0];
            else return PhotonNetwork.PlayerList[1];
        }

        public static byte AmountTypes(Type type) => (byte)Enum.GetNames(type).Length;

        public static ResourceTypes MinResType => (ResourceTypes)1;
        public static ResourceTypes MaxResType => (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length;

        public static PlayerTypes MinPlayerType => (PlayerTypes)1;
        public static PlayerTypes MaxPlayerType => (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length;

        public static EnvirTypes MinEnvironType => (EnvirTypes)1;
        public static EnvirTypes MaxEnvironType => (EnvirTypes)Enum.GetNames(typeof(EnvirTypes)).Length;
    }
}
