namespace Chessy.Model
{
    public sealed class EnvironmentE
    {
        public ResourcesC FertilizeC;
        public ResourcesC YoungForestC;
        public ResourcesC HillC;
        public ResourcesC AdultForestC;
        public ResourcesC MountainC;

        internal void Dispose()
        {
            FertilizeC.Resources = 0;
            YoungForestC.Resources = 0;
            HillC.Resources = 0;
            AdultForestC.Resources = 0;
            MountainC.Resources = 0;
        }
    }
}