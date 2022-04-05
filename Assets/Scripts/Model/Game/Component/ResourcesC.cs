namespace Chessy.Game
{
    public struct ResourcesC
    {
        public float Resources
        {
            get; 
            internal set;
        }

        public bool HaveAnyResources => Resources > 0;

        internal ResourcesC(in float resources) => Resources = resources;

        internal void SetRandom(in float low, in float max) => Resources = UnityEngine.Random.Range(low, max);
    }
}