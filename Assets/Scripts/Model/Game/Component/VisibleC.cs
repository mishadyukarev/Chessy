namespace Chessy.Game.Model.Component
{
    public struct VisibleC
    {
        readonly bool[] _isVisible;

        internal bool[] IsVisibleClone => (bool[])_isVisible.Clone();
        public bool IsVisible(in PlayerTypes playerT) => _isVisible[(byte)playerT];

        internal VisibleC(in bool def) => _isVisible = new bool[(byte)PlayerTypes.End];

        internal void Set(in PlayerTypes playerT, in bool isVisible) => _isVisible[(byte)playerT] = isVisible;

        internal void Sync(in bool[] isVisible)
        {
            for (var i = 0; i < isVisible.Length; i++)
            {
                _isVisible[i] = isVisible[i];
            }
        }
    }
}