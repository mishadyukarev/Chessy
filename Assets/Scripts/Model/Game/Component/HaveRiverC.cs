namespace Chessy.Game.Model.Component
{
    public struct HaveRiverC
    {
        readonly bool[] _haveRive;
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRive[(byte)dir - 1];

        internal HaveRiverC(in bool[] haveRive) => _haveRive = haveRive;
    }
}