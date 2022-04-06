using Chessy.Game.Enum;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using System.Collections.Generic;

namespace Chessy.Game
{
    sealed class EconomyUpUIS : SystemUIAbstract
    {
        readonly UpEconomyUIE _economyUIE;
        readonly Dictionary<ResourceTypes, float> _extracts = new Dictionary<ResourceTypes, float>();
        readonly Dictionary<ResourceTypes, bool> _needActive = new Dictionary<ResourceTypes, bool>();

        internal EconomyUpUIS(in UpEconomyUIE economyUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _economyUIE = economyUIE;
            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                _extracts.Add(res, 0);
                _needActive.Add(res, false);
            }
        }

        internal override void Sync()
        {
            var activeResZone = false;
            for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++) _needActive[resT] = false;


            if (!e.LessonTC.HaveLesson)
            {
                activeResZone = true;
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++) _needActive[resT] = true;

            }
            else if (e.LessonT >= LessonTypes.BuyingHouse)
            {
                activeResZone = true;

                _needActive[ResourceTypes.Wood] = true;

                if (e.LessonT >= LessonTypes.BuildingFarmHere)
                {
                    _needActive[ResourceTypes.Food] = true;

                    if (e.LessonT >= LessonTypes.ExtractHillPawnHere)
                    {
                        _needActive[ResourceTypes.Ore] = true;

                        if (e.LessonT >= LessonTypes.ClickBuyMelterInTown)
                        {
                            _needActive[ResourceTypes.Iron] = true;
                            _needActive[ResourceTypes.Gold] = true;
                        }
                    }
                }
            }

            _economyUIE.ParenGOC.SetActive(activeResZone);



            if (activeResZone)
            {

                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++)
                {
                    _economyUIE.Economy(resT).SetActiveParent(_needActive[resT]);
                }





                    for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++) _extracts[res] = default;

                _extracts[ResourceTypes.Food] += EconomyValues.ADDING_FOOD_AFTER_UPDATE;


                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {


                    if (e.UnitPlayerTC(idx_0).Is(e.CurPlayerIT))
                    {
                        if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                        {
                            _extracts[ResourceTypes.Food] -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                            _extracts[ResourceTypes.Ore] += e.PawnExtractHillC(idx_0).Resources;
                            _extracts[ResourceTypes.Wood] += e.PawnExtractAdultForestC(idx_0).Resources;
                        }
                    }

                    if (e.BuildingPlayerTC(idx_0).Is(e.CurPlayerIT))
                    {
                        _extracts[ResourceTypes.Wood] += e.WoodcutterExtractC(idx_0).Resources;
                        _extracts[ResourceTypes.Food] += e.FarmExtractFertilizeC(idx_0).Resources;
                    }
                }


                if (_extracts[ResourceTypes.Food] < 0) _economyUIE.EconomyExtract(ResourceTypes.Food).TextUI.text = ((int)(100 * _extracts[ResourceTypes.Food])).ToString();
                else _economyUIE.EconomyExtract(ResourceTypes.Food).TextUI.text = "+ " + ((int)(100 * _extracts[ResourceTypes.Food]));

                _economyUIE.EconomyExtract(ResourceTypes.Wood).TextUI.text = "+ " + ((int)(100 * _extracts[ResourceTypes.Wood]));
                _economyUIE.EconomyExtract(ResourceTypes.Ore).TextUI.text = "+ " + ((int)(100 * _extracts[ResourceTypes.Ore]));


                for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                {
                    string name = default;
                    if (res == ResourceTypes.Iron || res == ResourceTypes.Gold)
                    {
                        name = e.PlayerInfoE(e.CurPlayerIT).ResourcesC(res).Resources.ToString();
                    }
                    else
                    {
                        name = ((int)(100 * e.PlayerInfoE(e.CurPlayerIT).ResourcesC(res).Resources)).ToString();
                    }

                    _economyUIE.Economy(res).TextUI.text = name;
                }
            }
        }
    }
}