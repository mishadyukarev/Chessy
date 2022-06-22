using Chessy.Game.Model.Entity;

namespace Chessy.Game
{
    static partial class BuildingSystems
    {
        internal static void Clear(this EntitiesModelGame e, in byte cell_0)
        {
            e.SetBuildingOnCellT(cell_0, BuildingTypes.None);
        }
    }
}