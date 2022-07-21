using Chessy.Model.Component;
namespace Chessy.Model
{
    public sealed class BuildingE
    {
        public readonly BuildingOnCellC BuildingMainC = new();
        public readonly BuildingExtractionOnCellC ExtractionC = new();

        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerC = new(default);

        internal void Dispose()
        {
            BuildingMainC.Dispose();
        }
    }
}