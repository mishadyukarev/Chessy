using ECS;

namespace Game.Game
{
    public sealed class ResourcesInInventorE : EntityAbstract
    {
        readonly ResourceTypes _resT;
        readonly PlayerTypes _playerT;

        public ref AmountC Resources => ref Ent.Get<AmountC>();

        public bool IsMinus => Resources.Amount < 0;

        public int Need(in BuildingTypes build) => ResourcesInInventorValues.ForBuild(build, _resT);
        public int Need(in UnitTypes unit) => ResourcesInInventorValues.ForBuy(unit, _resT);
        public int NeedForMelting() => ResourcesInInventorValues.ForMelting(_resT);

        public bool CanBuy(in BuildingTypes build) => Resources.Amount >= ResourcesInInventorValues.ForBuild(build, _resT);
        public bool CanBuy(in UnitTypes unit) => Resources.Amount >= ResourcesInInventorValues.ForBuy(unit, _resT);
        public bool CanBuyMelting() => Resources.Amount >= ResourcesInInventorValues.ForMelting(_resT);

        internal ResourcesInInventorE(in ResourceTypes res, in PlayerTypes player, in EcsWorld gameW) : base(gameW)
        {
            _resT = res;
            _playerT = player;

            Ent.Add(new AmountC(ResourcesInInventorValues.AmountResources(res)));
        }

        public void Buy(in BuildingTypes build) => Resources.Amount -= ResourcesInInventorValues.ForBuild(build, _resT);
        public void Buy(in UnitTypes unit) => Resources.Amount -= ResourcesInInventorValues.ForBuy(unit, _resT);
        public void BuyMelting()
        {
            Resources.Amount -= NeedForMelting();
            Resources.Amount += ResourcesInInventorValues.AfterMelting(_resT);
        }
        public void AddPawnExtractAdultForest(in CellUnitEs unitEs, in CellEnvAdultForestE adultForestE)
        {
            Resources.Amount += adultForestE.AmountExtractPawn(unitEs);
        }
        public void AddWoodcutterExtractAdultForest(in CellEnvAdultForestE adultForestE, in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            Resources.Amount += adultForestE.AmountExtractWoodcutter(buildUpgEs, buildEs);
        }
        public void AddFarmExtractFertilize(in CellEnvFertilizerE fertE, in BuildingUpgradeEs buildUpgEs,in CellBuildEs buildEs)
        {
            Resources.Amount += fertE.AmountExtractFarm(buildUpgEs, buildEs);
        }
        public void AddFarmExtractHill(in CellEnvHillE hillE, in BuildingUpgradeEs buildUpgEs, in CellBuildEs buildEs)
        {
            Resources.Amount += hillE.AmountExtractMine(buildUpgEs, buildEs);
        }
    }
}