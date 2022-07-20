namespace Chessy.Model.Component
{
    public sealed class HaveRiverAroundCellC
    {
        readonly bool[] _haveRives;

        internal bool[] HaveRives => (bool[])_haveRives.Clone();
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRives[(byte)dir];

        internal HaveRiverAroundCellC() => _haveRives = new bool[(byte)DirectTypes.End];

        internal void Sync(in bool[] haveRives)
        {
            for (var i = 0; i < haveRives.Length; i++)
            {
                _haveRives[i] = haveRives[i];
            }
        }
    }
}