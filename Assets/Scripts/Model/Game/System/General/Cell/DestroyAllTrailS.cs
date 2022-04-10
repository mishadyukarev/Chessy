using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class DestroyAllTrailS : SystemModel
    {
        internal DestroyAllTrailS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Destroy(in byte cell)
        {
            for (var dirT = DirectTypes.None + 1; dirT < DirectTypes.End; dirT++)
            {
                eMG.HealthTrail(cell).Health(dirT) = 0;
            }
        }
    }
}