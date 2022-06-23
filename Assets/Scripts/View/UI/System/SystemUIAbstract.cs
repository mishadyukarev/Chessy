using Chessy.Model.Model.Entity;

namespace Chessy.Model
{
    internal abstract class SystemUIAbstract
    {
        protected readonly EntitiesModel _e;

        protected SystemUIAbstract(in EntitiesModel eMG)
        {
            _e = eMG;
        }

        internal abstract void Sync();
    }
}