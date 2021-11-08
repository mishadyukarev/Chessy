using TMPro;
using UnityEngine;

namespace Chessy.Game
{
    public struct MotionsViewUIC
    {
        private static GameObject _parent_GO;
        private static TextMeshProUGUI _textMeshProUGUI;

        public static string Text
        {
            get => _textMeshProUGUI.text;
            set => _textMeshProUGUI.text = value;
        }

        public MotionsViewUIC(GameObject center_GO)
        {
            _parent_GO = center_GO.transform.Find("MotionZone").gameObject;
            _textMeshProUGUI = _parent_GO.transform.Find("MotionText").GetComponent<TextMeshProUGUI>();
        }

        public static void SetActiveParent(bool isActive) => _parent_GO.SetActive(isActive);
    }
}
