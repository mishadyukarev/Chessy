using ECS;
using Game.Common;
using Photon.Pun;
using System;

namespace Game.Game
{
    public sealed class WhoseMoveE : EntityAbstract
    {
        public ref PlayerTC WhoseMove => ref Ent.Get<PlayerTC>();

        public WhoseMoveE(in PlayerTypes whoseMove, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new PlayerTC(whoseMove));
        }

        public PlayerTypes CurPlayerI
        {
            get
            {
                switch (GameModeC.CurGameMode)
                {
                    case GameModes.TrainingOff: return PlayerTypes.First;
                    case GameModes.WithFriendOff:
                        return WhoseMove.Player;

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
        public bool IsMyMove => CurPlayerI == WhoseMove.Player;
        public PlayerTypes NextPlayerFrom(PlayerTypes player)
        {
            if (player == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }
    }
}
