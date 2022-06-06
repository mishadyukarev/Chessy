using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class TryDestroyAllTrailsOnCellS_M : SystemModel
    {
        internal TryDestroyAllTrailsOnCellS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryDestroy(in byte cell)
        {
            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                if (eMG.HealthTrail(cell).IsAlive(dirT))
                {
                    eMG.HealthTrail(cell).Health(dirT) = 0;
                }
            }
        }
    }
}