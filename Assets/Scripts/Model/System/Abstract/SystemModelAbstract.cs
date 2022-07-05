using Chessy.Model.Entity;

namespace Chessy.Model.System
{
    public abstract class SystemModelAbstract
    {
        protected readonly EntitiesModel _e;
        protected readonly SystemsModel _s;

        protected SystemModelAbstract(in SystemsModel sM, in EntitiesModel eM)
        {
            _e = eM;
            _s = sM;
        }
    }
}