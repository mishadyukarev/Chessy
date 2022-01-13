using ECS;
using Game.Common;
using Photon.Pun;
using System;

namespace Game.Game
{
    public struct EntWhoseMove
    {
        static Entity _whoseMove;

        public static ref C WhoseMove<C>() where C : struct => ref _whoseMove.Get<C>();

        public EntWhoseMove(in EcsWorld gameW)
        {
            _whoseMove = gameW.NewEntity()
                .Add(new PlayerC());

            WhoseMove<PlayerC>().Player = PlayerTypes.First;
        }

        public static PlayerTypes CurPlayerI
        {
            get
            {
                switch (GameModeC.CurGameMode)
                {
                    case GameModes.TrainingOff: return PlayerTypes.First;
                    case GameModes.WithFriendOff:
                        return WhoseMove<PlayerC>().Player;

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
        public static bool IsMyMove => CurPlayerI == WhoseMove<PlayerC>().Player;
        public static PlayerTypes NextPlayerFrom(PlayerTypes player)
        {
            if (player == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }
    }
}
