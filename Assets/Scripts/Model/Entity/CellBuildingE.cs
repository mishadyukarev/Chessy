using Chessy.Model.Component;
namespace Chessy.Model
{
    public struct BuildingE
    {
        public BuildingOnCellC BuildingMainC;
        public HealthC HealthC;
        public BuildingExtractionOnCellC ExtractionC;

        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerC;

        internal BuildingE(in bool def)
        {
            BuildingMainC = default;
            HealthC = default;
            ExtractionC = default;

            VisibleToOtherPlayerC = new VisibleToOtherPlayerOrNotC(default);
        }

        internal void Dispose()
        {
            BuildingMainC = default;
        }
    }
}