using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    public abstract class SystemModel
    {
        protected readonly EntitiesModelGame _e;
        protected readonly SystemsModelGame _s;

        protected SystemModel(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            this._e = eMG;
            this._s = sMG;
        }
    }
}