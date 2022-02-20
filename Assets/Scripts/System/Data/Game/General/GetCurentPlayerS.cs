using Game.Common;
using Photon.Pun;
using System;

namespace Game.Game
{
    sealed class GetCurentPlayerS : SystemAbstract, IEcsRunSystem
    {
        internal GetCurentPlayerS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var curPlayer = PlayerTypes.None;

            switch (GameModeC.CurGameMode)
            {
                case GameModes.TrainingOff: curPlayer = PlayerTypes.First; 
                    break;

                case GameModes.WithFriendOff: curPlayer = E.WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    curPlayer = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    curPlayer = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }

            E.CurPlayerI.Player = curPlayer;
            E.IsMyMove = E.CurPlayerI.Player == E.WhoseMove.Player;
        }
    }
}