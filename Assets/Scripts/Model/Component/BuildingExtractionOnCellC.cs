namespace Chessy.Model
{
    public sealed class BuildingExtractionOnCellC
    {
        public double HowManyWoodcutterCanExtractWood { get; internal set; }
        public double HowManyFarmCanExtractFood { get; internal set; }

        public bool CanWoodcutterExtact => HowManyWoodcutterCanExtractWood > 0;
        public bool CanFarmExtact => HowManyFarmCanExtractFood > 0;
    }
}