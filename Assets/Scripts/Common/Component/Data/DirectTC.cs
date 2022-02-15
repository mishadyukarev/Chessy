namespace Game.Game
{
    public abstract class DirectTC
    {
        public DirectTypes Direct;

        public bool Have => Direct != DirectTypes.None;

        public DirectTC() { }
        public DirectTC(in DirectTypes dir) => Direct = dir;
    }
}