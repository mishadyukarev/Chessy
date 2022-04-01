namespace Chessy.Game
{
    public struct CellRiverE
    {
        public RiverTC RiverTC;

        readonly bool[] _haveRive;
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRive[(byte)dir - 1];

        internal CellRiverE(in bool[] haveRive) : this()
        {
            _haveRive = haveRive;
        } 
    }
}