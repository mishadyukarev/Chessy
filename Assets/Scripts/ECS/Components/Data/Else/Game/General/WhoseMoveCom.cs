using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct WhoseMoveCom
    {
        internal static PlayerTypes CurOfflinePlayer;

        internal WhoseMoveCom(PlayerTypes playerType) => CurOfflinePlayer = playerType;

        internal static PlayerTypes CurOnlinePlayer
        {
            get
            {
                if (PhotonNetwork.LocalPlayer.IsMasterClient) return PlayerTypes.First;
                else return PlayerTypes.Second;
            }
        }

        internal static PlayerTypes CurPlayer
        {
            get
            {
                if (GameModesCom.IsOnMode) return CurOnlinePlayer;
                else return CurOfflinePlayer;
            }
        }
    }
}
