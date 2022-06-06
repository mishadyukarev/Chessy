using Chessy.Game.Model.Entity;

namespace Chessy.Game.Model.System
{
    sealed class ClearBuildingS_M : SystemModel
    {
        public ClearBuildingS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Clear(in byte cell_0)
        {
            eMG.BuildingTC(cell_0).BuildingT = BuildingTypes.None;
        }
    }
}