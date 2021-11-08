using TMPro;
using UnityEngine;

namespace Chessy.Game
{
    public struct EndGameViewUIC
    {
        private static TextMeshProUGUI _textMeshProUGUI;

        public static string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        public EndGameViewUIC(GameObject centerZone_GO)
        {
            _textMeshProUGUI = centerZone_GO.transform.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>();
        }

        public static void SetActiveZone(bool isActive) => _textMeshProUGUI.transform.parent.gameObject.SetActive(isActive);
    }
}
