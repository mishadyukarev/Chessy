using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Entity.Model;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    public abstract class SystemModelGameAbs
    {
        protected readonly EntitiesModelCommon eMC;
        protected readonly SystemsModelCommon sMC;
        protected readonly EntitiesModelGame eMG;
        protected readonly SystemsModelGame sMG;

        protected SystemModelGameAbs(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            this.eMC = eMC;
            this.sMC = sMC;
            this.eMG = eMG;
            this.sMG = sMG;
        }
    }
}