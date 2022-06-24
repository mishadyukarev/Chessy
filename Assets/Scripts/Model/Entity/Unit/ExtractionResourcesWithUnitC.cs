namespace Chessy.Model
{
    public struct ExtractionResourcesWithUnitC
    {
        public float HowManyWarriourCanExtractAdultForest { get; internal set; }
        public float HowManyWarriourCanExtractHill { get; internal set; }

        public bool CanExtractAdultForest => HowManyWarriourCanExtractAdultForest > 0;
        public bool CanExtractHill => HowManyWarriourCanExtractHill > 0;
    }
}