namespace Chessy.Common
{
    public struct WaterC
    {
        public double Water { get; internal set; }

        public bool HaveAnyWater => Water > 0;
    }
}