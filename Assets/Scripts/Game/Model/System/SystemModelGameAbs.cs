using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game
{
    public abstract class SystemModelGameAbs
    {
        protected readonly EntitiesModelGame e;
        protected readonly SystemsModelGame s;

        protected SystemModelGameAbs(in SystemsModelGame sMGame, in EntitiesModelGame eMGame)
        {
            e = eMGame;
            s = sMGame;
        }
    }
}