namespace Game.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Entities E;
        protected SystemAbstract(in Entities ents)
        {
            E = ents;
        }
    }
}