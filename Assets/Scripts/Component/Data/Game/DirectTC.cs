namespace Game.Game
{
    public struct DirectTC
    {
        public DirectTypes Direct;

        public DirectTC(in DirectTypes dir) => Direct = dir;
    }
}