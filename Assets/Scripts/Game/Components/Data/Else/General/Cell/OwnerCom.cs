namespace Scripts.Game
{
    internal struct OwnerCom
    {
        internal PlayerTypes PlayerType;

        internal bool IsMine => Is(WhoseMoveC.CurPlayer);
        internal bool Is(PlayerTypes playerType) => PlayerType == playerType;
    }
}
