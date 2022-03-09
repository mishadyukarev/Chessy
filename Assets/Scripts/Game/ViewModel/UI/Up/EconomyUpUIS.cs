using Chessy.Game.Values;
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

            _extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_UPDATE;


            for (byte idx_0 = 0; idx_0 < E.LengthCells; idx_0++)
            {


                if (E.UnitPlayerTC(idx_0).Is(curPlayer))
                {
                    if (E.UnitTC(idx_0).HaveUnit)
                    {
                        _extracts[ResourceTypes.Food] -= EconomyValues.FOOD_FOR_FEEDING_UNITS;
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

            if (_extracts[ResourceTypes.Food] < 0) UIE.UpEs.EconomyE.EconomyExtract(ResourceTypes.Food).TextUI.text = (Math.Truncate(10 * _extracts[ResourceTypes.Food]) / 10).ToString();
            else UIE.UpEs.EconomyE.EconomyExtract(ResourceTypes.Food).TextUI.text = "+ " + Math.Truncate(100 * _extracts[ResourceTypes.Food]) / 10;

            UIE.UpEs.EconomyE.EconomyExtract(ResourceTypes.Wood).TextUI.text = "+ " + Math.Truncate(10 * _extracts[ResourceTypes.Wood]) / 10;
            UIE.UpEs.EconomyE.EconomyExtract(ResourceTypes.Ore).TextUI.text = "+ " + Math.Truncate(10 * _extracts[ResourceTypes.Ore]) / 10;


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                UIE.UpEs.EconomyE.Economy(res).TextUI.text = (Math.Truncate(10 * E.PlayerE(curPlayer).ResourcesC(res).Resources) / 10).ToString();
            }
        }
    }
}