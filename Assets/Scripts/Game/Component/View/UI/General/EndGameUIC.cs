using TMPro;
using UnityEngine;

namespace Game.Game
{
    public struct EndGameUIC
    {
        private static TextMeshProUGUI _text;

        public static string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public EndGameUIC(GameObject centerZone_GO)
        {
            _text = centerZone_GO.transform.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>();
        }

        public static void SetActiveZone(bool isActive) => _text.transform.parent.gameObject.SetActive(isActive);
    }
}
