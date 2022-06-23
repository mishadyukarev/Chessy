using Chessy.Model.Model.Entity;

namespace Chessy.Model.View.System
{
    abstract class SystemViewAbstract : SystemAbstract
    {
        protected SystemViewAbstract(in EntitiesModel eMG) : base(eMG) { }

        internal abstract void Sync();
    }
}