using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.UI.Game.General
{
    internal struct EndGameViewUIComponent
    {
        private TextMeshProUGUI _textMeshProUGUI;

        internal string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal EndGameViewUIComponent(GameObject centerZone_GO)
        {
            _textMeshProUGUI = centerZone_GO.transform.Find("TheEndGameZone").transform.Find("TheEndGameText").GetComponent<TextMeshProUGUI>();
        }

        internal void SetActiveZone(bool isActive) => _textMeshProUGUI.transform.parent.gameObject.SetActive(isActive);
    }
}
