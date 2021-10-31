using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    public struct MistakeViewUIC
    {
        private static GameObject _backgroud_GO;

        private static GameObject _textZone_GO;
        private static TextMeshProUGUI _textMeshProUGUI;

        private static GameObject _needStepZone_GO;
        private static GameObject _needMoreHealth_GO;
        private static GameObject _needOtherPlace_GO;
        private static GameObject _needCity_GO;
        private static GameObject _thatsForOtherUnit_GO;
        private static GameObject _nearBorder_GO;

        private static Dictionary<ResTypes, TextMeshProUGUI> _needAmountRes_TextMP;

        public static string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        public MistakeViewUIC(GameObject centerZone_GO)
        {
            var mistakeZone_GO = centerZone_GO.transform.Find("MistakeZone").gameObject;

            _backgroud_GO = mistakeZone_GO.transform.Find("BackgroudZone").gameObject;

            _textZone_GO = mistakeZone_GO.transform.Find("TextZone").gameObject;
            _textMeshProUGUI = _textZone_GO.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _needStepZone_GO = mistakeZone_GO.transform.Find("NeedStepsZone").gameObject;
            _needMoreHealth_GO = mistakeZone_GO.transform.Find("NeedMoreHealthZone").gameObject;
            _needOtherPlace_GO = mistakeZone_GO.transform.Find("NeedOtherPlaceZone").gameObject;
            _needCity_GO = mistakeZone_GO.transform.Find("NeedCityZone").gameObject;
            _thatsForOtherUnit_GO = mistakeZone_GO.transform.Find("ThatsForOtherUnitZone").gameObject;
            _nearBorder_GO = mistakeZone_GO.transform.Find("NearBorderZone").gameObject;



            _needAmountRes_TextMP = new Dictionary<ResTypes, TextMeshProUGUI>();
            var economyZone_trans = mistakeZone_GO.transform.Find("NeedMoreResourcesZone");
            _needAmountRes_TextMP.Add(ResTypes.Food, economyZone_trans.Find("NeedFood_Image").Find("TMP").GetComponent<TextMeshProUGUI>());
            _needAmountRes_TextMP.Add(ResTypes.Wood, economyZone_trans.Find("NeedWood_Image").Find("TMP").GetComponent<TextMeshProUGUI>());
            _needAmountRes_TextMP.Add(ResTypes.Ore, economyZone_trans.Find("NeedOre_Image").Find("TMP").GetComponent<TextMeshProUGUI>());
            _needAmountRes_TextMP.Add(ResTypes.Iron, economyZone_trans.Find("NeedIron_Image").Find("TMP").GetComponent<TextMeshProUGUI>());
            _needAmountRes_TextMP.Add(ResTypes.Gold, economyZone_trans.Find("NeedGold_Image").Find("TMP").GetComponent<TextMeshProUGUI>());
        }

        public static void ActiveBackgroud(bool isActive) => _backgroud_GO.SetActive(isActive);

        public static void ActiveTextZone(bool isActive) => _textZone_GO.SetActive(isActive);
        public static void ActiveNeedSteps(bool isActive) => _needStepZone_GO.SetActive(isActive);
        public static void ActiveNeedMoreHealth(bool isActive) => _needMoreHealth_GO.SetActive(isActive);
        public static void ActiveNeedOtherPlace(bool isActive) => _needOtherPlace_GO.SetActive(isActive);
        public static void ActiveNeedCity(bool isActive) => _needCity_GO.SetActive(isActive);
        public static void ActiveThatsForOtherUnit(bool isActive) => _thatsForOtherUnit_GO.SetActive(isActive);
        public static void ActiveNearBorderZone(bool isActive) => _nearBorder_GO.SetActive(isActive);

        public static void SetActiveRes(ResTypes resType, bool isActive) => _needAmountRes_TextMP[resType].transform.parent.gameObject.SetActive(isActive);
        public static void SetText(ResTypes resType, string text) => _needAmountRes_TextMP[resType].text = text;
    }
}
