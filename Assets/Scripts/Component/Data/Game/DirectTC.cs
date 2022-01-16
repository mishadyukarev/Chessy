namespace Game.Game
{
    public struct DirectTC : IDirectWindE
    {
        public DirectTypes Direct;

        public DirectTC(in DirectTypes dir) => Direct = dir;
    }
}