namespace Game.Game
{
    public struct CanMoveC
    {
        public bool CanMove;

        public CanMoveC(in bool canMove) => CanMove = canMove;
    }
}