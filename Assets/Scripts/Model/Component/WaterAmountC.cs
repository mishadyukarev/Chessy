namespace Chessy.Model
{
    public sealed class WaterAmountC
    {
        internal double Water;

        public double WaterP => Water;
        public bool HaveAnyWater => Water > 0;
    }
}