namespace Chessy.Game
{
    public struct CornerArcherC
    {
        private bool _isCornered;

        public bool IsCornered => _isCornered;

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