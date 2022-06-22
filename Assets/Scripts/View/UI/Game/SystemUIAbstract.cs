using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    internal abstract class SystemUIAbstract
    {
        protected readonly EntitiesModelGame _e;

        protected SystemUIAbstract(in EntitiesModelGame eMG)
        {
            _e = eMG;
        }

        internal abstract void Sync();
    }
}