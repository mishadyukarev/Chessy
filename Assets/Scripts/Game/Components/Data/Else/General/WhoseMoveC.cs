using Photon.Pun;
using Scripts.Common;
using System.Collections.Generic;

namespace Scripts.Game
{
    internal struct WhoseMoveC
    {
        private static Dictionary<bool, PlayerTypes> _whoseMove;

        internal static PlayerTypes CurPlayer
        {
            get
            {
                if (PhotonNetwork.OfflineMode) return _whoseMove[PhotonNetwork.OfflineMode];
                else
                {
                    if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                    else return PlayerTypes.Second;
                }
            }
        }
        internal static bool IsMyMove => _whoseMove[PhotonNetwork.OfflineMode] == CurPlayer;
        internal static PlayerTypes WhoseMove => _whoseMove[PhotonNetwork.OfflineMode];


        internal WhoseMoveC(PlayerTypes playerType)
        {
            _whoseMove = new Dictionary<bool, PlayerTypes>();

            _whoseMove.Add(true, playerType);
            _whoseMove.Add(false, playerType);
        }


        internal static PlayerTypes NextPlayerFrom(PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.First) return PlayerTypes.Second;
            else return PlayerTypes.First;
        }
        internal static void SetWhoseMove(PlayerTypes playerType)
        {
            if (_whoseMove.ContainsKey(PhotonNetwork.OfflineMode)) _whoseMove[PhotonNetwork.OfflineMode] = playerType;
            else throw new System.Exception();
        }
    }
}
