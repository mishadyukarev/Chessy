namespace Chessy.Model
{
    public struct ResourcesC
    {
        public float Resources
        {
            get;
            internal set;
        }

        public bool HaveAnyResources => Resources > 0;

        internal void SetRandom(in float low, in float max) => Resources = UnityEngine.Random.Range(low, max);
    }
}