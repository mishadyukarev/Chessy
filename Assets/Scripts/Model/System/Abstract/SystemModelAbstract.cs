using Chessy.Model.Entity;

namespace Chessy.Model.System
{
    public abstract class SystemModelAbstract : SystemAbstract
    {
        protected readonly SystemsModel s;

        protected SystemModelAbstract(in SystemsModel sM, EntitiesModel eM) : base(eM)
        {
            s = sM;
        }
    }
}