using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class EconomyUpUIS : SystemUIAbstract, IEcsRunSystem
    {
        readonly Dictionary<ResourceTypes, float> _extracts;

        internal EconomyUpUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
            _extracts = new Dictionary<ResourceTypes, float>();
            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts.Add(res, default);
        }

        public void Run()
        {
            var curPlayer = E.CurPlayerITC.Player;


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts[res] = default;

            _extracts[ResourceTypes.Food] += ResourcesEconomy_Values.ADDING_FOOD_AFTER_MOVE;


            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {


                if (E.UnitPlayerTC(idx_0).Is(curPlayer))
                {
                    if (E.UnitTC(idx_0).HaveUnit)
                    {
                        _extracts[ResourceTypes.Food] -= ResourcesEconomy_Values.CostFoodForFeedingThem(E.UnitTC(idx_0).Unit);
                    }


                    _extracts[ResourceTypes.Ore] += E.PawnExtractHillE(idx_0).Resources;
                    _extracts[ResourceTypes.Wood] += E.PawnExtractAdultForestE(idx_0).Resources;
                }

                if (E.BuildingPlayerTC(idx_0).Is(curPlayer))
                {
                    _extracts[ResourceTypes.Wood] += E.WoodcutterExtractE(idx_0).Resources;
                    _extracts[ResourceTypes.Food] += E.FarmExtractFertilizeE(idx_0).Resources;
                }
            }

            if (_extracts[ResourceTypes.Food] < 0) UIE.UpEs.EconomyE.Economy(ResourceTypes.Food).TextUI.text = Math.Round(_extracts[ResourceTypes.Food], 2).ToString();
            else UIE.UpEs.EconomyE.Economy(ResourceTypes.Food).TextUI.text = "+ " + Math.Round(_extracts[ResourceTypes.Food], 2);

            UIE.UpEs.EconomyE.Economy(ResourceTypes.Wood).TextUI.text = "+ " + Math.Round(_extracts[ResourceTypes.Wood], 2);
            UIE.UpEs.EconomyE.Economy(ResourceTypes.Ore).TextUI.text = "+ " + Math.Round(_extracts[ResourceTypes.Ore], 2);


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                UIE.UpEs.EconomyE.Economy(res).TextUI.text = Math.Round(E.PlayerE(curPlayer).ResourcesC(res).Resources, 1).ToString();
            }
        }
    }
}