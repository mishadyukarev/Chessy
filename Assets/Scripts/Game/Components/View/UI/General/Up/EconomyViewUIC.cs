using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    internal struct EconomyViewUIC
    {
        private static Dictionary<ResourceTypes, TextMeshProUGUI> _amountRes_TextMP;
        private static Dictionary<ResourceTypes, TextMeshProUGUI> _amountAddRes_TextMP;

        internal EconomyViewUIC(GameObject upZone_GO)
        {
            var resZone_Trans = upZone_GO.transform.Find("ResourcesZone");

            _amountRes_TextMP = new Dictionary<ResourceTypes, TextMeshProUGUI>();
            _amountAddRes_TextMP = new Dictionary<ResourceTypes, TextMeshProUGUI>();


            var curZone_Trans = resZone_Trans.Find("FoodZone");
            var curRes = ResourceTypes.Food;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("FoodAmount_TMP").GetComponent<TextMeshProUGUI>());
            _amountAddRes_TextMP.Add(curRes, curZone_Trans.Find("FoodAdding_TMP").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("WoodZone");
            curRes = ResourceTypes.Wood;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("WoodAmount").GetComponent<TextMeshProUGUI>());
            _amountAddRes_TextMP.Add(curRes, curZone_Trans.Find("WoodAdding_TMP").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("OreZone");
            curRes = ResourceTypes.Ore;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("OreAmount").GetComponent<TextMeshProUGUI>());
            _amountAddRes_TextMP.Add(curRes, curZone_Trans.Find("OreAdding_TMP").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("IronZone");
            curRes = ResourceTypes.Iron;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("IronAmount").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("GoldZone");
            curRes = ResourceTypes.Gold;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("GoldAmount").GetComponent<TextMeshProUGUI>());
        }

        internal static void SetMainText(ResourceTypes resType, string text) => _amountRes_TextMP[resType].text = text;
        internal static void SetMainColor(ResourceTypes resType, Color color) => _amountRes_TextMP[resType].color = color;

        internal static void SetAddText(ResourceTypes resType, string text) => _amountAddRes_TextMP[resType].text = text;
    }
}

