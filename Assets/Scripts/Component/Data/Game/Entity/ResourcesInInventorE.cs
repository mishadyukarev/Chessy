using ECS;

namespace Game.Game
{
    public sealed class ResourcesInInventorE : EntityAbstract
    {
        public readonly ResourceTypes ResT;
        public readonly PlayerTypes PlayerT;

        ref AmountC ResourcesRef => ref Ent.Get<AmountC>();
        public AmountC Resources => Ent.Get<AmountC>();

        public int AmountResource => Resources.Amount;
        public bool IsMinus => Resources.Amount < 0;

        public int Need(in BuildingTypes build) => ResourcesInInventorValues.ForBuild(build, ResT);
        public int NeedForBuy(in MarketBuyTypes marketBuyT) => ResourcesInInventorValues.ResourcesForBuyFromMarket(marketBuyT);

        public bool CanBuy(in BuildingTypes build) => Resources.Amount >= ResourcesInInventorValues.ForBuild(build, ResT);
        public bool CanBuyResourcesFromMarket(in BuildingTypes build) => Resources.Amount >= ResourcesInInventorValues.ForBuild(build, ResT);

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
        public void AddPawnExtractAdultForest(in CellUnitEs unitEs, in CellEnvAdultForestE adultForestE)
        {
            ResourcesRef.Amount += adultForestE.AmountExtractPawn(unitEs);
        }
        public void AddWoodcutterExtractAdultForest(in CellEnvAdultForestE adultForestE, in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            ResourcesRef.Amount += adultForestE.AmountExtractBuilding(buildUpgEs, buildEs);
        }
        public void AddFarmExtractFertilize(in CellEnvFertilizerE fertE, in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            ResourcesRef.Amount += fertE.AmountExtractBuilding(buildUpgEs, buildEs);
        }
        public void AddFarmExtractHill(in CellEnvHillE hillE, in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            ResourcesRef.Amount += hillE.AmountExtractBuilding(buildUpgEs, buildEs);
        }
    }
}