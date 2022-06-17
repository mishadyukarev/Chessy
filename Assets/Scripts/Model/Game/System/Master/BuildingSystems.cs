using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        readonly EntitiesModelGame _eMG;
        readonly SystemsModelGame _sMG;

        internal readonly ClearBuildingS_M ClearS;

        internal BuildingSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            _eMG = eMG;
            _sMG = sMG;

            ClearS = new ClearBuildingS_M(sMG, eMG);
        }
    }
}