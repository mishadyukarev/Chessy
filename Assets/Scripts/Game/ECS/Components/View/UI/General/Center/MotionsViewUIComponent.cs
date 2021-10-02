using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct MotionsViewUIComponent
    {
        private GameObject _parent_GO;
        private TextMeshProUGUI _textMeshProUGUI;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal MotionsViewUIComponent(GameObject center_GO)
        {
            _parent_GO = center_GO.transform.Find("MotionZone").gameObject;
            _textMeshProUGUI = _parent_GO.transform.Find("MotionText").GetComponent<TextMeshProUGUI>();
        }

        internal void SetActiveParent(bool isActive) => _parent_GO.SetActive(isActive);
    }
}
