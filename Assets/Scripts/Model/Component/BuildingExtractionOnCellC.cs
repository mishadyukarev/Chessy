namespace Chessy.Model
{
    public sealed class BuildingExtractionOnCellC
    {
        public float HowManyWoodcutterCanExtractWood { get; internal set; }
        public float HowManyFarmCanExtractFood { get; internal set; }

        public bool CanWoodcutterExtact => HowManyWoodcutterCanExtractWood > 0;
        public bool CanFarmExtact => HowManyFarmCanExtractFood > 0;
    }
}