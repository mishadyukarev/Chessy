using Chessy.Game.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Game
{
    public sealed class EconomyUpUIS
    {
        readonly Dictionary<ResourceTypes, float> _extracts;

        internal EconomyUpUIS(in Dictionary<ResourceTypes, float> dict)
        {
            _extracts = dict;
            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts.Add(res, default);
        }

        public void Run(in EntitiesViewUIGame eUI, in Chessy.Game.Entity.Model.EntitiesModelGame e)
        {
            if (e.LessonTC.LessonT >= Enum.LessonTypes.BuyingHouse || !e.LessonTC.HaveLesson)
            {
                eUI.UpEs.EconomyE.ParenGOC.SetActive(true);


                var curPlayer = e.CurPlayerITC.PlayerT;


                for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts[res] = default;

                _extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_UPDATE;


                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {


                    if (e.UnitPlayerTC(idx_0).Is(curPlayer))
                    {
                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            _extracts[ResourceTypes.Food] -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                            _extracts[ResourceTypes.Ore] += e.PawnExtractHillE(idx_0).Resources;
                            _extracts[ResourceTypes.Wood] += e.PawnExtractAdultForestE(idx_0).Resources;
                        }
                    }

                    if (e.BuildingPlayerTC(idx_0).Is(curPlayer))
                    {
                        _extracts[ResourceTypes.Wood] += e.WoodcutterExtractE(idx_0).Resources;
                        _extracts[ResourceTypes.Food] += e.FarmExtractFertilizeE(idx_0).Resources;
                    }
                }


                var v = 100 * _extracts[ResourceTypes.Food];
                var vv = (int)v;


                if (_extracts[ResourceTypes.Food] < 0) eUI.UpEs.EconomyE.EconomyExtract(ResourceTypes.Food).TextUI.text = ((int)(100 * _extracts[ResourceTypes.Food])).ToString();
                else eUI.UpEs.EconomyE.EconomyExtract(ResourceTypes.Food).TextUI.text = "+ " + ((int)(100 * _extracts[ResourceTypes.Food]));

                eUI.UpEs.EconomyE.EconomyExtract(ResourceTypes.Wood).TextUI.text = "+ " + ((int)(100 * _extracts[ResourceTypes.Wood]));
                eUI.UpEs.EconomyE.EconomyExtract(ResourceTypes.Ore).TextUI.text = "+ " + ((int)(100 * _extracts[ResourceTypes.Ore]));


                for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                {
                    string name = default;
                    if (res == ResourceTypes.Iron || res == ResourceTypes.Gold)
                    {
                        name = e.PlayerInfoE(curPlayer).ResourcesC(res).Resources.ToString();
                    }
                    else
                    {
                        name = ((int)(100 * e.PlayerInfoE(curPlayer).ResourcesC(res).Resources)).ToString();
                    }

                    eUI.UpEs.EconomyE.Economy(res).TextUI.text = name;
                }
            }
            else
            {
                eUI.UpEs.EconomyE.ParenGOC.SetActive(false);
            }
        }
    }
}