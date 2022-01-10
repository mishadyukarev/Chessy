namespace Game.Game
{
    public struct CornerArcherC : IUnitCellE
    {
        public bool IsCornered { get; private set; }


        public void Set(CornerArcherC cornerAC)
        {
            IsCornered = cornerAC.IsCornered;
        }
        public void ChangeCorner()
        {
            IsCornered = !IsCornered;
        }
        public void Sync(bool isCorned)
        {
            IsCornered = isCorned;
        }
    }
}