using Chessy.Common.Entity;

namespace Chessy.Common.View.UI
{
    abstract class SyncUISystem
    {
        protected readonly EntitiesModelCommon e;

        protected SyncUISystem(in EntitiesModelCommon eMC)
        {
            e = eMC;
        }
        internal abstract void Sync();
    }
}