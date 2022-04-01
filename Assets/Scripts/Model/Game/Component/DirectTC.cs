namespace Chessy.Game
{
    public struct DirectTC
    {
        public DirectTypes DirectT { get; internal set; }

        public bool Have => DirectT != DirectTypes.None;
    }
}