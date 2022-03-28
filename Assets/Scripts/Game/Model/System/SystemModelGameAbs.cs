using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    public abstract class SystemModelGameAbs
    {
        protected readonly EntitiesModelGame e;
        protected SystemModelGameAbs(in EntitiesModelGame eMGame) => e = eMGame;
    }
}