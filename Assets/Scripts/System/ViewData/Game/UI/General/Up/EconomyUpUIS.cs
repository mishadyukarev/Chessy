using System.Collections.Generic;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    sealed class EconomyUpUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Dictionary<ResourceTypes, int> _extracts;

        internal EconomyUpUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _extracts = new Dictionary<ResourceTypes, int>();
            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts.Add(res, default);
        }

        public void Run()
        {
            var curPlayer = Es.WhoseMoveE.CurPlayerI;


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts[res] = default;

            _extracts[ResourceTypes.Food] += ResourcesInInventorValues.ADDING_FOOD_AFTER_MOVE;


            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitE(idx_0).HaveUnit && Es.UnitE(idx_0).Is(Es.WhoseMoveE.CurPlayerI))
                {
                    _extracts[ResourceTypes.Food] -= ResourcesInInventorValues.CostFoodForFeedingThem(Es.UnitE(idx_0).Unit);

                    if (Es.AdultForestE(idx_0).CanExtractPawn(UnitEs(idx_0)))
                    {
                        _extracts[EnvironmentEs(idx_0).AdultForest.Resource] += EnvironmentEs(idx_0).AdultForest.AmountExtractPawn(Es.UnitE(idx_0));
                    }
                }
                if (Es.AdultForestE(idx_0).CanExtractWoodcutter(BuildEs(idx_0)))
                {
                    _extracts[Es.AdultForestE(idx_0).Resource] += EnvironmentEs(idx_0).AdultForest.AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));
                }
                if (BuildEs(idx_0).BuildingE.CanExtractFertilizer(EnvironmentEs(idx_0)))
                {
                    _extracts[EnvironmentEs(idx_0).Fertilizer.Resource] += EnvironmentEs(idx_0).Fertilizer.AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));
                }

                if (Es.HillE(idx_0).CanExtractPawn(Es.UnitEs(idx_0), Es.EnvironmentEs(idx_0)))
                {
                    _extracts[Es.HillE(idx_0).Resource] += Es.HillE(idx_0).AmountExtractPawnPick();
                }
            }


            if (_extracts[ResourceTypes.Food] < 0) EconomyExtract<TextUIC>(ResourceTypes.Food).Text = _extracts[ResourceTypes.Food].ToString();
            else EconomyExtract<TextUIC>(ResourceTypes.Food).Text = "+ " + _extracts[ResourceTypes.Food].ToString();

            EconomyExtract<TextUIC>(ResourceTypes.Wood).Text = "+ " + _extracts[ResourceTypes.Wood];
            EconomyExtract<TextUIC>(ResourceTypes.Ore).Text = "+ " + _extracts[ResourceTypes.Ore];


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                Economy<TextUIC>(res).Text = Es.InventorResourcesEs.Resource(res, curPlayer).Resources.ToString();
            }
        }
    }
}