namespace Scripts.Game
{
    public struct OwnerCom
    {
        public PlayerTypes PlayerType;

        public bool IsMine => Is(WhoseMoveC.CurPlayer);
        public bool Is(PlayerTypes playerType) => PlayerType == playerType;
    }
}
