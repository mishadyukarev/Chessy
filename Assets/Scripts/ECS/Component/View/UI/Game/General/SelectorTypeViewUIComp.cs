using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct SelectorTypeViewUIComp
    {
        private GameObject _parent_GO;
        private TextMeshProUGUI _selectorType_textMP;

        internal string Text
        {
            get => _selectorType_textMP.text;
            set => _selectorType_textMP.text = value;
        }

        internal SelectorTypeViewUIComp(GameObject centerZone_GO)
        {
            _parent_GO = centerZone_GO.transform.Find("SelectorTypeZone").gameObject;
            _selectorType_textMP = _parent_GO.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void EnableZone() => _parent_GO.SetActive(true);
        internal void DisableZone() => _parent_GO.SetActive(false);
    }
}
