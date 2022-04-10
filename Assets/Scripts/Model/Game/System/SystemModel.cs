using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    public abstract class SystemModel
    {
        protected readonly EntitiesModelGame eMG;
        protected readonly SystemsModelGame sMG;

        protected SystemModel(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            this.eMG = eMG;
            this.sMG = sMG;
        }
    }
}