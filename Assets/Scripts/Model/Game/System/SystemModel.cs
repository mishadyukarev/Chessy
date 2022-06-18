using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    public abstract class SystemModel
    {
        protected readonly EntitiesModelGame _eMG;
        protected readonly SystemsModelGame _sMG;

        protected SystemModel(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            this._eMG = eMG;
            this._sMG = sMG;
        }
    }
}