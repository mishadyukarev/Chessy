using ECS;
using Game.Common;
using Photon.Pun;
using System;

namespace Game.Game
{
    public struct WhoseMoveE
    {
        static Entity _whoseMove;

        public static ref C WhoseMove<C>() where C : struct, IWhoseMoveE => ref _whoseMove.Get<C>();

        public WhoseMoveE(in EcsWorld gameW)
        {
            _whoseMove = gameW.NewEntity()
                .Add(new PlayerTC());

            WhoseMove<PlayerTC>().Player = PlayerTypes.First;
        }

        public static PlayerTypes CurPlayerI
        {
            get
            {
                switch (GameModeC.CurGameMode)
                {
                    case GameModes.TrainingOff: return PlayerTypes.First;
                    case GameModes.WithFriendOff:
                        return WhoseMove<PlayerTC>().Player;

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
        public static bool IsMyMove => CurPlayerI == WhoseMove<PlayerTC>().Player;
        public static PlayerTypes NextPlayerFrom(PlayerTypes player)
        {
            if (player == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }
    }

    public interface IWhoseMoveE { }
}
