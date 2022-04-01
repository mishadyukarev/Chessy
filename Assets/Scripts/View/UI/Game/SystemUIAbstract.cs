using Chessy.Game.Entity.Model;

namespace Chessy.Game
{
    internal abstract class SystemUIAbstract
    {
        protected readonly EntitiesModelGame e;

        protected SystemUIAbstract(in EntitiesModelGame eMG)
        {
            e = eMG;
        }
    }
}