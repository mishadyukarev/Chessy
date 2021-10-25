namespace Scripts.Game
{
    internal struct OwnerCom
    {
        internal PlayerTypes PlayerType;

        internal bool IsMine => Is(WhoseMoveCom.CurPlayer);
        internal bool Is(PlayerTypes playerType) => PlayerType == playerType;
    }
}
