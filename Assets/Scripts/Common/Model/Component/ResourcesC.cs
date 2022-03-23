using Chessy.Common;

namespace Chessy.Game
{
    public struct ResourcesC
    {
        public float Resources;

        public bool HaveAnyResources => Resources > 0;

        public ResourcesC(in float resources) => Resources = resources;

        public void SetRandom(in float low, in float max) => Resources = UnityEngine.Random.Range(low, max);
    }
}