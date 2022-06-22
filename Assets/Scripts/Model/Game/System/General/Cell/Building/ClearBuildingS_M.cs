namespace Chessy.Game
{
    sealed partial class BuildingSystems
    {
        internal void Clear(in byte cell_0)
        {
            _e.SetBuildingOnCellT(cell_0, BuildingTypes.None);
        }
    }
}