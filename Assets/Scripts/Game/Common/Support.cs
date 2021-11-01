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
        public static DirectTypes Invert(this DirectTypes dir)
        {
            switch (dir)
            {
                case DirectTypes.None: throw new Exception();
                case DirectTypes.Up: return DirectTypes.Down;
                case DirectTypes.UpRight: return DirectTypes.DownLeft;
                case DirectTypes.Right: return DirectTypes.Left;
                case DirectTypes.DownRight: return DirectTypes.UpLeft;
                case DirectTypes.Down: return DirectTypes.Up;
                case DirectTypes.DownLeft: return DirectTypes.UpRight;
                case DirectTypes.Left: return DirectTypes.Right;
                case DirectTypes.UpLeft: return DirectTypes.DownRight;
                default: throw new Exception();
            }
        }

        public static byte AmountTypes(Type type) => (byte)Enum.GetNames(type).Length;

        public static ResTypes MinResType => (ResTypes)1;
        public static ResTypes MaxResType => (ResTypes)Enum.GetNames(typeof(ResTypes)).Length;

        public static PlayerTypes MinPlayerType => (PlayerTypes)1;
        public static PlayerTypes MaxPlayerType => (PlayerTypes)Enum.GetNames(typeof(PlayerTypes)).Length;

        public static EnvTypes MinEnvironType => (EnvTypes)1;
        public static EnvTypes MaxEnvironType => (EnvTypes)Enum.GetNames(typeof(EnvTypes)).Length;

        public static AttackTypes MinAttackType => (AttackTypes)1;
        public static AttackTypes MaxAttackType => (AttackTypes)Enum.GetNames(typeof(AttackTypes)).Length;

        public static UnitTypes MinUnitType => (UnitTypes)1;
        public static UnitTypes MaxUnitType => (UnitTypes)Enum.GetNames(typeof(UnitTypes)).Length;

        public static BuildTypes MinBuildType => (BuildTypes)1;
        public static BuildTypes MaxBuildType => (BuildTypes)Enum.GetNames(typeof(BuildTypes)).Length;
    }
}
