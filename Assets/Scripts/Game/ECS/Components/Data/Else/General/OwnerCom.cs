using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;

internal struct OwnerCom
{
    internal PlayerTypes PlayerType;

    internal bool IsMine => IsPlayerType(WhoseMoveCom.CurPlayer);
    internal bool IsPlayerType(PlayerTypes playerType) => PlayerType == playerType;
}
