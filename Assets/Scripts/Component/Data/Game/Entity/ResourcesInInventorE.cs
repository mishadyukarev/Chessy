using ECS;

namespace Game.Game
{
    public sealed class ResourcesInInventorE : EntityAbstract
    {
        public readonly ResourceTypes ResT;
        public readonly PlayerTypes PlayerT;

        ref AmountC ResourcesRef => ref Ent.Get<AmountC>();

        public int Resources
        {
            get => ResourcesRef.Amount;
            set => ResourcesRef.Amount = value;
        }

        public bool IsMinus => Resources < 0;

        public int Need(in BuildingTypes build) => ResourcesInInventorValues.ForBuild(build, ResT);
        public int NeedForBuy(in MarketBuyTypes marketBuyT) => ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);

        public bool CanBuy(in BuildingTypes build) => Resources >= ResourcesInInventorValues.ForBuild(build, ResT);
        public bool CanBuyResourcesFromMarket(in BuildingTypes build) => Resources >= ResourcesInInventorValues.ForBuild(build, ResT);

        internal ResourcesInInventorE(in ResourceTypes res, in PlayerTypes player, in EcsWorld gameW) : base(gameW)
        {
            ResT = res;
            PlayerT = player;

            Ent.Add(new AmountC(ResourcesInInventorValues.AmountResourcesOnStartGame(res)));
        }

        public void Buy(in BuildingTypes build) => ResourcesRef.Amount -= ResourcesInInventorValues.ForBuild(build, ResT);

        public void Add(in int adding = 1)
        {
            ResourcesRef.Amount += adding;
        }
        public void Take(in int taking = 1)
        {
            ResourcesRef.Amount -= taking;
        }
        public void Set(in int amount)
        {
            ResourcesRef.Amount = amount;
        }
        public void Reset()
        {
            ResourcesRef.Amount = 0;
        }
    }
}