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

                case GameModes.WithFriendOff: curPlayer = Es.WhoseMove.Player;
                    break;

                case GameModes.PublicOn:
                    curPlayer = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                case GameModes.WithFriendOn:
                    curPlayer = PhotonNetwork.IsMasterClient ? PlayerTypes.First : PlayerTypes.Second;
                    break;

                default: throw new Exception();
            }

            Es.CurPlayerI.Player = curPlayer;
            Es.IsMyMove = Es.CurPlayerI.Player == Es.WhoseMove.Player;
        }
    }
}