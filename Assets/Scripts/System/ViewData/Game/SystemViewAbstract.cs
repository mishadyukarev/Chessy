namespace Game.Game
{
    public abstract class SystemViewAbstract : SystemAbstract
    {
        protected readonly EntitiesView VEs;

        public SystemViewAbstract(in Entities ents, in EntitiesView entsView) : base(ents)
        {
            VEs = entsView;
        }
    }
}