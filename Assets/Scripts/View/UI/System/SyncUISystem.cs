using Chessy.Model;
using Chessy.Model.Entity;
namespace Chessy.View.UI
{
    abstract class SyncUISystem : SystemAbstract
    {
        protected SyncUISystem(in EntitiesModel eM) : base(eM) { }

        internal abstract void Sync();
    }
}