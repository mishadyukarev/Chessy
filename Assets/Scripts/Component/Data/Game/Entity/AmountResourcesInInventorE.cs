using ECS;

namespace Game.Game
{
    public sealed class AmountResourcesInInventorE : EntityAbstract
    {
        public ref AmountC Resources => ref Ent.Get<AmountC>();

        public bool IsMinus => Resources.Amount < 0;

        public AmountResourcesInInventorE(in int resources, in EcsWorld gameW) : base(gameW)
        {
            Ent.Add(new AmountC(resources));
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
    }
}