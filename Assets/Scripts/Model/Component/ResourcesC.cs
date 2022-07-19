namespace Chessy.Model
{
    public struct ResourcesC
    {
        internal float Resources;

        public float ResourcesP => Resources;
        public bool HaveAnyResources => Resources > 0.009;

        internal void SetRandom(in float low, in float max) => Resources = UnityEngine.Random.Range(low, max);
    }
}