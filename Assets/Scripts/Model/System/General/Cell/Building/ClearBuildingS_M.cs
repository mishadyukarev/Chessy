using Chessy.Model;

namespace Chessy.Model
{
    static partial class BuildingSystems
    {
        internal static void Clear(this EntitiesModel e, in byte cell_0)
        {
            e.SetBuildingOnCellT(cell_0, BuildingTypes.None);
        }
    }
}