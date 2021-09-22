using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
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



        internal WhoseMoveCom(PlayerTypes playerType) => WhoseMoveOffline = playerType;
    }
}
