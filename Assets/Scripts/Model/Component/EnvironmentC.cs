namespace Chessy.Model.Component
{
    public struct EnvironmentC
    {
        internal readonly double[] ResourcesArray;

        internal ref double ResourcesRef(in EnvironmentTypes environmentT) => ref ResourcesArray[(byte)environmentT];
        public double Resources(in EnvironmentTypes environmentT) => ResourcesArray[(byte)environmentT];
        public bool HaveEnvironment(in EnvironmentTypes environmentT) => ResourcesArray[(byte)environmentT] > 0.01;

        internal EnvironmentC(in double[] resources) { ResourcesArray = resources; }

        internal void Set(in EnvironmentTypes environmentT, in double resources) => ResourcesArray[(byte)environmentT] = resources;
        internal void SetRandom(in EnvironmentTypes environmentT, in float low, in float max) => Set(environmentT, UnityEngine.Random.Range(low, max));

        internal void Dispose()
        {
            for (int i = 0; i < ResourcesArray.Length; i++) ResourcesArray[i] = default;
        }
    }
}