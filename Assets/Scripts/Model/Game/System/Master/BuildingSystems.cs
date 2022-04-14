using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;

namespace Chessy.Game
{
    sealed class BuildingSystems
    {
        internal readonly BuildS_M BuildS;
        internal readonly AttackBuildingS_M DestroyS;
        internal readonly ClearBuildingS_M ClearS;

        internal BuildingSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            BuildS = new BuildS_M(sMG, eMG);
            ClearS = new ClearBuildingS_M(sMG, eMG);
            DestroyS = new AttackBuildingS_M(sMG, eMG);
        }
    }
}