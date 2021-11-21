using TMPro;
using UnityEngine;

namespace Game.Game
{
    public struct MotionsUIC
    {
        private static GameObject _parent;
        private static TextMeshProUGUI _text;

        public static string Text
        {
            get => _text.text;
            set => _text.text = value;
        }

        public MotionsUIC(GameObject center_GO)
        {
            _parent = center_GO.transform.Find("MotionZone").gameObject;
            _text = _parent.transform.Find("MotionText").GetComponent<TextMeshProUGUI>();
        }

        public static void SetActiveParent(bool isActive) => _parent.SetActive(isActive);
    }
}
