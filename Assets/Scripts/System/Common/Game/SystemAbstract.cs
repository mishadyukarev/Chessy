namespace Game.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Entities Es;

        protected SystemAbstract(in Entities ents)
        {
            Es = ents;
        }
    }
}