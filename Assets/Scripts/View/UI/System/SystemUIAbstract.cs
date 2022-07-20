using Chessy.Model.Entity;
using Chessy.View.UI.Entity; namespace Chessy.Model
{
    internal abstract class SystemUIAbstract : SystemAbstract
    {
        protected SystemUIAbstract(in EntitiesModel eM) : base(eM) { }

        internal abstract void Sync();
    }
}