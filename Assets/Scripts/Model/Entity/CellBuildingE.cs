using Chessy.Model.Component;
namespace Chessy.Model
{
    public struct BuildingE
    {
        public BuildingC BuildingMainC;
        public HealthC HealthC;
        public BuildingExtractionC ExtractionC;

        public readonly VisibleToOtherPlayerOrNotC VisibleToOtherPlayerC;

        internal BuildingE(in bool def)
        {
            BuildingMainC = default;
            HealthC = default;
            ExtractionC = default;

            VisibleToOtherPlayerC = new VisibleToOtherPlayerOrNotC(default);
        }
    }
}