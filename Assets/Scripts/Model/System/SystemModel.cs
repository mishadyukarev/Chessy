using Chessy.Model.Entity;
using Chessy.Model.System;
namespace Chessy.Model
{
    public abstract class SystemModel
    {
        protected readonly EntitiesModel _e;
        protected readonly SystemsModel _s;

        protected SystemModel(in SystemsModel sMG, in EntitiesModel eMG)
        {
            _e = eMG;
            _s = sMG;
        }
    }
}