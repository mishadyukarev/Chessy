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


            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)) && UnitEs(idx_0).MainE.OwnerC.Is(Es.WhoseMove.CurPlayerI))
                {
                    extracts[ResourceTypes.Food] -= EconomyValues.CostFood(UnitEs(idx_0).MainE.UnitTC.Unit);

                    if (UnitEs(idx_0).MainE.CanExtractPawnAdultForest(UnitStatEs(idx_0), EnvironmentEs(idx_0)))
                    {
                        extracts[EnvironmentEs(idx_0).AdultForest.ResourceT] += EnvironmentEs(idx_0).AdultForest.AmountExtractPawn(UnitEs(idx_0));
                    }
                }
                if (BuildEs(idx_0).BuildingE.CanExtractAdultForest(BuildEs(idx_0), EnvironmentEs(idx_0)))
                {
                    extracts[EnvironmentEs(idx_0).AdultForest.ResourceT] += EnvironmentEs(idx_0).AdultForest.AmountExtractWoodcutter(Es.BuildingUpgradeEs, BuildEs(idx_0));
                }
                if (BuildEs(idx_0).BuildingE.CanExtractFertilizer(EnvironmentEs(idx_0)))
                {
                    extracts[EnvironmentEs(idx_0).Fertilizer.ResourceT] += EnvironmentEs(idx_0).Fertilizer.AmountExtractFarm(Es.BuildingUpgradeEs, BuildEs(idx_0));
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