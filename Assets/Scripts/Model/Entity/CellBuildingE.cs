using Chessy.Model.Component;
namespace Chessy.Model
{
    public sealed class BuildingE
    {
        public BuildingOnCellC BuildingMainC = new();
        public HealthC HealthC = new();
        public BuildingExtractionOnCellC ExtractionC = new();

        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerC = new(default);

        internal void Dispose()
        {
            BuildingMainC = default;
        }
    }
}