using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chessy.Game
{
    public struct EconomyViewUIC
    {
        private static Dictionary<ResTypes, TextMeshProUGUI> _amountRes_TextMP;
        private static Dictionary<ResTypes, TextMeshProUGUI> _amountAddRes_TextMP;

        public EconomyViewUIC(GameObject upZone_GO)
        {
            var resZone_Trans = upZone_GO.transform.Find("ResourcesZone");

            _amountRes_TextMP = new Dictionary<ResTypes, TextMeshProUGUI>();
            _amountAddRes_TextMP = new Dictionary<ResTypes, TextMeshProUGUI>();


            var curZone_Trans = resZone_Trans.Find("FoodZone");
            var curRes = ResTypes.Food;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("FoodAmount_TMP").GetComponent<TextMeshProUGUI>());
            _amountAddRes_TextMP.Add(curRes, curZone_Trans.Find("FoodAdding_TMP").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("WoodZone");
            curRes = ResTypes.Wood;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("WoodAmount").GetComponent<TextMeshProUGUI>());
            _amountAddRes_TextMP.Add(curRes, curZone_Trans.Find("WoodAdding_TMP").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("OreZone");
            curRes = ResTypes.Ore;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("OreAmount").GetComponent<TextMeshProUGUI>());
            _amountAddRes_TextMP.Add(curRes, curZone_Trans.Find("OreAdding_TMP").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("IronZone");
            curRes = ResTypes.Iron;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("IronAmount").GetComponent<TextMeshProUGUI>());

            curZone_Trans = resZone_Trans.Find("GoldZone");
            curRes = ResTypes.Gold;
            _amountRes_TextMP.Add(curRes, curZone_Trans.Find("GoldAmount").GetComponent<TextMeshProUGUI>());
        }

        public static void SetMainText(ResTypes resType, string text) => _amountRes_TextMP[resType].text = text;
        public static void SetMainColor(ResTypes resType, Color color) => _amountRes_TextMP[resType].color = color;

        public static void SetAddText(ResTypes resType, string text) => _amountAddRes_TextMP[resType].text = text;
    }
}

