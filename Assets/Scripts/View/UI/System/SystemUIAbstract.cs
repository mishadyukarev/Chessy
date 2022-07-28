using Chessy.Model.Entity;
namespace Chessy.Model
{
    internal abstract class SystemUIAbstract : SystemAbstract
    {
        protected SystemUIAbstract(in EntitiesModel eM) : base(eM) { }

        internal abstract void Sync();
    }
}