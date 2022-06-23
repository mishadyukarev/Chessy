using Chessy.Model.Model.Entity;
using Chessy.Model;

namespace Chessy.Common.View.UI
{
    abstract class SyncUISystem : SystemAbstract
    {
        protected SyncUISystem(in EntitiesModel eM) : base(eM) { }

        internal abstract void Sync();
    }
}