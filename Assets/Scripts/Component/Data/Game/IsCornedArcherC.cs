namespace Game.Game
{
    public struct IsCornedArcherC : IUnitCellE
    {
        public bool IsCornered;


        public void Set(IsCornedArcherC cornerAC)
        {
            IsCornered = cornerAC.IsCornered;
        }
        public void ChangeCorner()
        {
            IsCornered = !IsCornered;
        }
    }
}