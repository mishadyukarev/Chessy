using Chessy.Model;

namespace Chessy.Model
{
    public abstract class SystemAbstract
    {
        protected readonly EntitiesModel _e;

        protected SystemAbstract(in EntitiesModel eM) { _e = eM; }
    }
}