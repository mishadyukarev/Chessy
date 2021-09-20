using Assets.Scripts.Abstractions.Enums;

internal struct OwnerCom
{
    internal PlayerTypes PlayerType;

    internal bool IsPlayer => PlayerType != default;
    internal bool IsPlayerType(PlayerTypes playerType) => PlayerType == playerType;
}
