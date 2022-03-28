namespace Chessy.Game
{
    public sealed class CellRiverE
    {
        public RiverTC RiverTC;

        readonly bool[] _haveRive = new bool[(byte)DirectTypes.End - 1];
        public ref bool HaveRive(in DirectTypes dir) => ref _haveRive[(byte)dir - 1];
    }
}