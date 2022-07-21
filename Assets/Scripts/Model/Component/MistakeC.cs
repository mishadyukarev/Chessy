namespace Chessy.Model
{
    public sealed class MistakeC
    {
        internal readonly float[] NeedResourcesArray;

        public MistakeTypes MistakeT { get; internal set; }
        public float Timer { get; internal set; }

        internal ref float NeedResourcesRef(in ResourceTypes resourceT) => ref NeedResourcesArray[(byte)resourceT];
        public float NeedResources(in ResourceTypes resourceT) => NeedResourcesArray[(byte)resourceT];

        internal MistakeC(in float[] needResources)
        {
            NeedResourcesArray = needResources;
        }

        internal void Dispose()
        {
            MistakeT = default;
            Timer = default;
        }
    }
}