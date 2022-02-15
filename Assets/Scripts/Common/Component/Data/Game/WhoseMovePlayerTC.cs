using Game.Common;
using Photon.Pun;
using System;

namespace Game.Game
{
    public sealed class WhoseMovePlayerTC : PlayerTC
    {
        public PlayerTypes CurPlayerI
        {
            get
            {
                switch (GameModeC.CurGameMode)
                {
                    case GameModes.TrainingOff: return PlayerTypes.First;
                    case GameModes.WithFriendOff:
                        return Player;

                    case GameModes.PublicOn:
                        if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                        else return PlayerTypes.Second;

                    case GameModes.WithFriendOn:
                        if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                        else return PlayerTypes.Second;

                    default: throw new Exception();
                }
            }

        }
        public bool IsMyMove => CurPlayerI == Player;
        public PlayerTypes NextPlayerFrom(PlayerTypes player)
        {
            if (player == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }

        public WhoseMovePlayerTC() { }
        public WhoseMovePlayerTC(in PlayerTypes playerT) : base(playerT) { }
    }
}