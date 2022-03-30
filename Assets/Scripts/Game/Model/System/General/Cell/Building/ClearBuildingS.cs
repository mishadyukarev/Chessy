using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;

namespace Chessy.Game.Model.System
{
    sealed class ClearBuildingS : SystemModelGameAbs
    {
        internal ClearBuildingS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        internal void Clear(in byte cell_0)
        {
            e.BuildingMainE(cell_0) = default;
        }
    }
}