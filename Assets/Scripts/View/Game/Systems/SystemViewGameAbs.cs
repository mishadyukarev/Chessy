using Chessy.Game.Model.Entity;

namespace Chessy.Game.View.System
{
    abstract class SystemViewGameAbs
    {
        protected readonly EntitiesModelGame _e;

        protected SystemViewGameAbs(in EntitiesModelGame eMG)
        {
            _e = eMG;
        }
        internal abstract void Sync();
    }
}