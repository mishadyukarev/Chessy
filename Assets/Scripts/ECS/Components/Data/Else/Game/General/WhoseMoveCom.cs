using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
{
    internal struct WhoseMoveCom
    {
        internal static PlayerTypes WhoseMoveOnline;
        internal static PlayerTypes WhoseMoveOffline;



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
                if (GameModesCom.IsOnMode) return CurOnlinePlayer;
                else return WhoseMoveOffline;
            }
        }



        internal WhoseMoveCom(PlayerTypes playerType) => WhoseMoveOffline = playerType;
    }
}
