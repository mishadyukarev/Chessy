using Chessy.Game.Model.Entity;

namespace Chessy.Game.View.System
{
    abstract class SystemViewGameAbs
    {
        protected readonly EntitiesModelGame e;
        protected readonly EntitiesViewGame eV;

        protected SystemViewGameAbs(in EntitiesViewGame eV, in EntitiesModelGame eMGame)
        {
            e = eMGame;
            this.eV = eV;
        }
    }
}