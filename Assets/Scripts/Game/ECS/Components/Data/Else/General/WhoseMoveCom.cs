using Photon.Pun;
using Scripts.Common;

namespace Scripts.Game
{
    internal struct WhoseMoveCom
    {
        internal static PlayerTypes WhoseMoveOnline { get; set; }
        internal static PlayerTypes WhoseMoveOffline { get; set; }

        internal static bool IsMyOnlineMove => CurOnlinePlayer == WhoseMoveOnline;

        internal static PlayerTypes CurOnlinePlayer
        {
            get
            {
                if (PhotonNetwork.IsMasterClient) return PlayerTypes.First;
                else return PlayerTypes.Second;
            }
        }

        internal static PlayerTypes CurPlayer
        {
            get
            {
                if (GameModesCom.IsOnlineMode) return CurOnlinePlayer;
                else return WhoseMoveOffline;
            }
        }

        internal static PlayerTypes NextPLayer
        {
            get
            {
                if (CurPlayer == PlayerTypes.First) return PlayerTypes.Second;
                else return PlayerTypes.First;
            }
        }


        internal WhoseMoveCom(PlayerTypes playerType) => WhoseMoveOffline = playerType;
    }
}
