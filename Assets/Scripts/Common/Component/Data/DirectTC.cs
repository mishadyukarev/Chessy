namespace Chessy.Game
{
    public struct DirectTC
    {
        public DirectTypes Direct;

        public bool Have => Direct != DirectTypes.None;

        public DirectTC(in DirectTypes dir) => Direct = dir;
    }
}