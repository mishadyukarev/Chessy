using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    public abstract class SystemModelGameAbs
    {
        protected readonly EntitiesModelGame eMGame;
        protected SystemModelGameAbs(in EntitiesModelGame eMGame) => this.eMGame = eMGame;
    }
}