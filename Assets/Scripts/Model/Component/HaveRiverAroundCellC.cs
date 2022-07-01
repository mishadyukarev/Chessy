namespace Chessy.Model.Component
{
    public struct HaveRiverAroundCellC
    {
        readonly bool[] _haveRives;

        internal bool[] HaveRives => (bool[])_haveRives.Clone();
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRives[(byte)dir];

        internal HaveRiverAroundCellC(in bool[] haveRive) => _haveRives = haveRive;

        internal void Sync(in bool[] haveRives)
        {
            for (var i = 0; i < haveRives.Length; i++)
            {
                _haveRives[i] = haveRives[i];
            }
        }
    }
}