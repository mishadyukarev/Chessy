using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    internal struct EndGameViewUIC
    {
        private static TextMeshProUGUI _textMeshProUGUI;

        internal static string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        internal EndGameViewUIC(GameObject centerZone_GO)
        {
            _textMeshProUGUI = centerZone_GO.transform.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>();
        }

        internal static void SetActiveZone(bool isActive) => _textMeshProUGUI.transform.parent.gameObject.SetActive(isActive);
    }
}
