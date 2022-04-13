using Chessy.Game.Model.Entity;

namespace Chessy.Game.View.System
{
    abstract class SystemViewGameAbs
    {
        protected readonly EntitiesModelGame e;

        protected SystemViewGameAbs(in EntitiesModelGame eMG)
        {
            e = eMG;
        }
        internal abstract void Sync();
    }
}