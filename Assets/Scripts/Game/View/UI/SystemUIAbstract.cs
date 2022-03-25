using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    public abstract class SystemUIAbstract : SystemModelGameAbs
    {
        protected readonly EntitiesViewUIGame eUI;

        protected SystemUIAbstract( in EntitiesViewUIGame entsUI, in EntitiesModelGame ents) : base(ents)
        {
            eUI = entsUI;
        }
    }
}