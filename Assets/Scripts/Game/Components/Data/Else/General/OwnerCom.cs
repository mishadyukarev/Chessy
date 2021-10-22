namespace Scripts.Game
{
    internal struct OwnerCom
    {
        internal PlayerTypes PlayerType;

        internal bool IsMine => IsPlayerType(WhoseMoveCom.CurPlayer);
        internal bool IsPlayerType(PlayerTypes playerType) => PlayerType == playerType;
    }
}
