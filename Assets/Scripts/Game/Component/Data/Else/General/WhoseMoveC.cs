using Photon.Pun;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public struct WhoseMoveC
    {
        private static Dictionary<bool, PlayerTypes> _whoseMove;

        private static bool IsOffline => PhotonNetwork.OfflineMode;
        public static PlayerTypes CurPlayerI
        {
            get
            {
                if (IsOffline)
                {
                    return WhoseMove;
                }
                else
                {
                    if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                    else return PlayerTypes.Second;
                }
            }

        }
        public static PlayerTypes WhoseMove => _whoseMove[IsOffline];
        public static bool IsMyMove => CurPlayerI == WhoseMove;
        public static PlayerTypes NextPlayerFrom(PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }

        public WhoseMoveC(bool start)
        {
            if (start)
            {
                if (_whoseMove == default)
                {
                    _whoseMove = new Dictionary<bool, PlayerTypes>();

                    _whoseMove.Add(true, PlayerTypes.First);
                    _whoseMove.Add(false, PlayerTypes.First);
                }
                _whoseMove[true] = PlayerTypes.First;
                _whoseMove[false] = PlayerTypes.First;
            }

            else throw new Exception();
        }


        public static void SetWhoseMove(PlayerTypes playerType)
        {
            if (_whoseMove.ContainsKey(IsOffline)) _whoseMove[IsOffline] = playerType;
            else throw new System.Exception();
        }
    }
}
