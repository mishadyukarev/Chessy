using Chessy.Model;
using Chessy.Model;

namespace Chessy.Model
{
    public abstract class SystemModel
    {
        protected readonly EntitiesModel _e;
        protected readonly SystemsModel _s;

        protected SystemModel(in SystemsModel sMG, in EntitiesModel eMG)
        {
            this._e = eMG;
            this._s = sMG;
        }
    }
}