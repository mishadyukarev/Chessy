namespace Chessy.Model.Component
{
    public struct VisibleToOtherPlayerOrNotC
    {
        readonly bool[] _isVisible;

        internal bool[] IsVisibleClone => (bool[])_isVisible.Clone();
        public ref bool IsVisible(in PlayerTypes playerT) => ref _isVisible[(byte)playerT];

        internal VisibleToOtherPlayerOrNotC(in bool def) => _isVisible = new bool[(byte)PlayerTypes.End];

        internal void Set(in PlayerTypes playerT, in bool isVisible) => _isVisible[(byte)playerT] = isVisible;

        internal void Sync(in bool[] isVisible)
        {
            for (var i = 0; i < isVisible.Length; i++)
            {
                _isVisible[i] = isVisible[i];
            }
        }
        internal void Dispose()
        {
            for (var i = 0; i < _isVisible.Length; i++) _isVisible[i] = default;
        }
    }
}