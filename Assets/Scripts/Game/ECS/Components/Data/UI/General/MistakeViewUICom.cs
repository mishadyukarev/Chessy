using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    internal struct MistakeViewUICom
    {
        private GameObject _backgroud_GO;

        private GameObject _textZone_GO;
        private TextMeshProUGUI _textMeshProUGUI;

        private GameObject _needStepZone_GO;
        private GameObject _needMoreHealth_GO;
        private GameObject _needOtherPlace_GO;
        private GameObject _needCity_GO;
        private GameObject _thatsForOtherUnit_GO;
        private GameObject _nearBorder_GO;
        private GameObject _needMoreResources_GO;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal MistakeViewUICom(GameObject centerZone_GO)
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
            _needMoreResources_GO = mistakeZone_GO.transform.Find("NeedMoreResourcesZone").gameObject;
        }

        internal void ActiveBackgroud(bool isActive) => _backgroud_GO.SetActive(isActive);

        internal void ActiveTextZone(bool isActive) => _textZone_GO.SetActive(isActive);
        internal void ActiveNeedSteps(bool isActive) => _needStepZone_GO.SetActive(isActive);
        internal void ActiveNeedMoreHealth(bool isActive) => _needMoreHealth_GO.SetActive(isActive);
        internal void ActiveNeedOtherPlace(bool isActive) => _needOtherPlace_GO.SetActive(isActive);
        internal void ActiveNeedCity(bool isActive) => _needCity_GO.SetActive(isActive);
        internal void ActiveThatsForOtherUnit(bool isActive) => _thatsForOtherUnit_GO.SetActive(isActive);
        internal void ActiveNearBorderZone(bool isActive) => _nearBorder_GO.SetActive(isActive);
        internal void ActiveNeedMoreResources(bool isActive) => _needMoreResources_GO.SetActive(isActive);
    }
}
