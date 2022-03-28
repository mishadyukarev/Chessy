using Chessy.Game.Entity.Model;

namespace Chessy.Game.View.System
{
    abstract class SystemViewGameAbs : SystemModelGameAbs
    {
        protected readonly EntitiesViewGame eV;

        protected SystemViewGameAbs(in EntitiesViewGame eV, in EntitiesModelGame eMGame) : base(eMGame)
        {
            this.eV = eV;
        }
    }
}