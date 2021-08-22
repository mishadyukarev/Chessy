using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.Data.UI.Game.General
{
    internal struct MistakeViewUICom
    {
        private GameObject _parent_GO;
        private TextMeshProUGUI _textMeshProUGUI;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal MistakeViewUICom(GameObject centerZone_GO)
        {
            _parent_GO = centerZone_GO.transform.Find("MistakeZone").gameObject;
            _textMeshProUGUI = _parent_GO.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void SetActiveParent(bool isActive) => _parent_GO.SetActive(isActive);
    }
}
