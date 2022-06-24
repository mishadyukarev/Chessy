namespace Chessy.Model
{
    public struct EnergyC
    {
        public double Energy { get; internal set; }

        public bool HaveAnyEnergy => Energy > 0;
    }
}