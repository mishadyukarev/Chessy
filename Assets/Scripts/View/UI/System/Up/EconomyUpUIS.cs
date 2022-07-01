using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.Enum;
using Chessy.Model.Values;
using Chessy.View.UI.Entity;
using System.Collections.Generic;
namespace Chessy.View.UI.System
{
    sealed class EconomyUpUIS : SystemUIAbstract
    {
        readonly UpEconomyUIE _economyUIE;
        readonly Dictionary<ResourceTypes, float> _extracts = new Dictionary<ResourceTypes, float>();
        readonly Dictionary<ResourceTypes, bool> _needActive = new Dictionary<ResourceTypes, bool>();

        internal EconomyUpUIS(in UpEconomyUIE economyUIE, in EntitiesModel eMG) : base(eMG)
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


            if (!_e.LessonT.HaveLesson())
            {
                activeResZone = true;
                for (var resT = ResourceTypes.None + 1; resT < ResourceTypes.End; resT++) _needActive[resT] = true;

            }
            else if (_e.LessonT >= LessonTypes.TryBuyingHouse)
            {
                activeResZone = true;

                _needActive[ResourceTypes.Wood] = true;

                if (_e.LessonT >= LessonTypes.Build3Farms)
                {
                    _needActive[ResourceTypes.Food] = true;

                    if (_e.LessonT >= LessonTypes.ExtractHill)
                    {
                        _needActive[ResourceTypes.Ore] = true;

                        if (_e.LessonT >= LessonTypes.NeedBuildSmelterAndMeltOre)
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


                    if (_e.UnitPlayerT(idx_0).Is(_e.CurrentPlayerIT))
                    {
                        if (_e.UnitT(idx_0).Is(UnitTypes.Pawn))
                        {
                            _extracts[ResourceTypes.Food] -= EconomyValues.FOOD_FOR_FEEDING_UNITS;

                            _extracts[ResourceTypes.Ore] += _e.ExtactionResourcesWithWarriorC(idx_0).HowManyWarriourCanExtractHill;
                            _extracts[ResourceTypes.Wood] += _e.ExtactionResourcesWithWarriorC(idx_0).HowManyWarriourCanExtractAdultForest;
                        }
                    }

                    if (_e.BuildingPlayerT(idx_0).Is(_e.CurrentPlayerIT))
                    {
                        _extracts[ResourceTypes.Wood] += _e.WoodcutterExtract(idx_0);
                        _extracts[ResourceTypes.Food] += _e.FarmExtract(idx_0);
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
                        name = _e.ResourcesInInventory(_e.CurrentPlayerIT, res).ToString();
                    }
                    else
                    {
                        name = ((int)(100 * _e.ResourcesInInventory(_e.CurrentPlayerIT, res))).ToString();
                    }

                    _economyUIE.Economy(res).TextUI.text = name;
                }
            }
        }
    }
}