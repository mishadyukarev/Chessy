namespace Chessy.Model
{
    public sealed class ExtractionResourcesWithUnitC
    {
        public float HowManyWarriourCanExtractAdultForest { get; internal set; }
        public float HowManyWarriourCanExtractHill { get; internal set; }

        public bool CanExtractAdultForest => HowManyWarriourCanExtractAdultForest > 0;
        public bool CanExtractHill => HowManyWarriourCanExtractHill > 0;

        internal void Clone(in ExtractionResourcesWithUnitC extractionResourcesWithUnitC)
        {
            HowManyWarriourCanExtractAdultForest = extractionResourcesWithUnitC.HowManyWarriourCanExtractAdultForest;
            HowManyWarriourCanExtractHill = extractionResourcesWithUnitC.HowManyWarriourCanExtractHill;
        }
    }
}