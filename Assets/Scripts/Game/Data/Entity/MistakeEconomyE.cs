using ECS;

namespace Chessy.Game
{
    public sealed class MistakeEconomyE : EntityAbstract
    {
        readonly ResourceTypes _resourceT;

        ref ResourcesC ResourcesC => ref Ent.Get<ResourcesC>();

        public float NeedResources => ResourcesC.Resources;

        internal MistakeEconomyE(in ResourceTypes resT, in EcsWorld gameW) : base(gameW)
        {
            _resourceT = resT;
        }

        public void Set(in float needResources) => ResourcesC.Resources = needResources;
        public void SetZero() => Set(0);
    }
}