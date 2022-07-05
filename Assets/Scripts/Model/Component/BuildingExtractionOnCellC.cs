namespace Chessy.Model
{
    public struct BuildingExtractionOnCellC
    {
        public float HowManyWoodcutterCanExtractWood { get; internal set; }
        public float HowManyFarmCanExtractFood { get; internal set; }

        public bool CanWoodcutterExtact => HowManyWoodcutterCanExtractWood > 0;
        public bool CanFarmExtact => HowManyFarmCanExtractFood > 0;
    }
}