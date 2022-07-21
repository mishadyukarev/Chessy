namespace Chessy.Model.Component
{
    public sealed class EnvironmentC
    {
        internal readonly float[] ResourcesArray;

        internal ref float ResourcesRef(in EnvironmentTypes environmentT) => ref ResourcesArray[(byte)environmentT];
        public float Resources(in EnvironmentTypes environmentT) => ResourcesArray[(byte)environmentT];
        public bool HaveEnvironment(in EnvironmentTypes environmentT) => ResourcesArray[(byte)environmentT] > 0.009;

        internal EnvironmentC(in float[] resources) { ResourcesArray = resources; }

        internal void Set(in EnvironmentTypes environmentT, in float resources) => ResourcesArray[(byte)environmentT] = resources;
        internal void SetRandom(in EnvironmentTypes environmentT, in float low, in float max) => Set(environmentT, UnityEngine.Random.Range(low, max));

        internal void Dispose()
        {
            for (int i = 0; i < ResourcesArray.Length; i++) ResourcesArray[i] = default;
        }
    }
}