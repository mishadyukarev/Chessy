using System.Collections.Generic;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    sealed class EconomyUpUIS : SystemViewAbstract, IEcsRunSystem
    {
        public EconomyUpUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var curPlayer = Es.WhoseMove.CurPlayerI;


            var extracts = new Dictionary<ResourceTypes, int>();
            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                extracts.Add(res, default);
            }
            extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_MOVE;


            for (byte idx_0 = 0; idx_0 < CellEs.Count; idx_0++)
            {
                if (UnitEs.Main(idx_0).HaveUnit(UnitEs.StatEs) && UnitEs.Main(idx_0).OwnerC.Is(Es.WhoseMove.CurPlayerI))
                {
                    extracts[ResourceTypes.Food] -= EconomyValues.CostFood(UnitEs.Main(idx_0).UnitTC.Unit);

                    if (UnitEs.Main(idx_0).CanExtractPawnAdultForest(UnitEs.StatEs, EnvironmentEs))
                    {
                        extracts[EnvironmentEs.AdultForest(idx_0).ResourceT] += EnvironmentEs.AdultForest(idx_0).AmountExtractPawn(UnitEs);
                    }
                }
                if (BuildEs.BuildingE(idx_0).CanExtractAdultForest(BuildEs, EnvironmentEs))
                {
                    extracts[EnvironmentEs.AdultForest(idx_0).ResourceT] += EnvironmentEs.AdultForest(idx_0).AmountExtractWoodcutter(Es.BuildingUpgradeEs, BuildEs);
                }
                if (BuildEs.BuildingE(idx_0).CanExtractFertilizer(EnvironmentEs))
                {
                    extracts[EnvironmentEs.Fertilizer(idx_0).ResourceT] += EnvironmentEs.Fertilizer(idx_0).AmountExtractFarm(Es.BuildingUpgradeEs, BuildEs);
                }
            }


            if (extracts[ResourceTypes.Food] < 0) EconomyExtract<TextUIC>(ResourceTypes.Food).Text = extracts[ResourceTypes.Food].ToString();
            else EconomyExtract<TextUIC>(ResourceTypes.Food).Text = "+ " + extracts[ResourceTypes.Food].ToString();

            EconomyExtract<TextUIC>(ResourceTypes.Wood).Text = "+ " + extracts[ResourceTypes.Wood];
            EconomyExtract<TextUIC>(ResourceTypes.Ore).Text = "+ " + extracts[ResourceTypes.Ore];


            for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
            {
                Economy<TextUIC>(res).Text = Es.InventorResourcesEs.Resource(res, curPlayer).Resources.Amount.ToString();
            }
        }
    }
}