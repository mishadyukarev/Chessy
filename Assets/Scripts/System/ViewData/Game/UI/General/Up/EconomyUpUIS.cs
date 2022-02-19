using System;
using System.Collections.Generic;
using static Game.Game.EconomyUpUIE;

namespace Game.Game
{
    sealed class EconomyUpUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Dictionary<ResourceTypes, float> _extracts;

        internal EconomyUpUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
            _extracts = new Dictionary<ResourceTypes, float>();
            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts.Add(res, default);
        }

        public void Run()
        {
            var curPlayer = Es.CurPlayerI.Player;


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts[res] = default;

            _extracts[ResourceTypes.Food] += ResourcesEconomy_Values.ADDING_FOOD_AFTER_MOVE;


            for (byte idx_0 = 0; idx_0 < Es.LengthCells; idx_0++)
            {
                if (Es.UnitTC(idx_0).HaveUnit && Es.UnitPlayerTC(idx_0).Is(Es.CurPlayerI.Player))
                {
                    _extracts[ResourceTypes.Food] -= ResourcesEconomy_Values.CostFoodForFeedingThem(Es.UnitTC(idx_0).Unit);

                    //if (Es.AdultForestC(idx_0).CanExtractPawn)
                    //{
                    //    _extracts[Es.EnvironmentEs(idx_0).AdultForest.Resource] += Es.EnvironmentEs(idx_0).AdultForest.AmountExtractPawn(Es.UnitE(idx_0));
                    //}
                }
                //if (Es.AdultForestC(idx_0).CanExtractWoodcutter(Es.BuildEs(idx_0)))
                //{
                //    _extracts[Es.AdultForestC(idx_0).Resource] += Es.EnvironmentEs(idx_0).AdultForest.AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));
                //}
                //if (Es.BuildingE(idx_0).CanExtractFertilizer(Es.EnvironmentEs(idx_0)))
                //{
                //    //_extracts[ResourceTypes.Food] += Es.EnvironmentEs(idx_0).FertilizeC.AmountExtractBuilding(Es.BuildingUpgradeEs, Es.BuildingE(idx_0));
                //}

                //if (Es.HillC(idx_0).CanExtractPawn(Es.UnitEs(idx_0), Es.EnvironmentEs(idx_0)))
                //{
                //    _extracts[Es.HillC(idx_0).Resource] += Es.HillC(idx_0).AmountExtractPawnPick();
                //}
            }


            var multiple = 100;

            if (_extracts[ResourceTypes.Food] < 0) EconomyExtract<TextUIC>(ResourceTypes.Food).Text = Math.Round(_extracts[ResourceTypes.Food], 2).ToString();
            else EconomyExtract<TextUIC>(ResourceTypes.Food).Text = "+ " + Math.Round(_extracts[ResourceTypes.Food], 2).ToString();

            EconomyExtract<TextUIC>(ResourceTypes.Wood).Text = "+ " + Math.Round(_extracts[ResourceTypes.Wood], 2);
            EconomyExtract<TextUIC>(ResourceTypes.Ore).Text = "+ " + Math.Round(_extracts[ResourceTypes.Ore], 2);


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                Economy<TextUIC>(res).Text = Math.Round(Es.PlayerE(curPlayer).ResourcesC(res).Resources, 2).ToString();
            }
        }
    }
}