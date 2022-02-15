namespace Game.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Entities Es;

        protected CellSpaceWorker CellWorker => Es.CellSpaceWorker;

        protected SystemAbstract(in Entities ents)
        {
            Es = ents;
        }
    }
}