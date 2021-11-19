using Photon.Pun;
using Photon.Realtime;
using System;

namespace Game.Game
{
    public static class Support
    {
        public static PlayerTypes GetPlayerType(this Player player)
        {
            if (player.IsMasterClient == true) return PlayerTypes.First;
            else return PlayerTypes.Second;
        }
        public static Player GetPlayerType(this PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.First) return PhotonNetwork.PlayerList[0];
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
    }
}
