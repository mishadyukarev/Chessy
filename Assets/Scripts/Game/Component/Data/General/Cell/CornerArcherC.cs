namespace Game.Game
{
    public struct CornerArcherC : IUnitCell
    {
        private bool _isCornered;

        public bool IsCornered => _isCornered;


        public void Set(CornerArcherC cornerAC)
        {
            _isCornered = cornerAC._isCornered;
        }
        public void ChangeCorner()
        {
            _isCornered = !_isCornered;
        }
        public void Sync(bool isCorned)
        {
            _isCornered = isCorned;
        }
    }
}