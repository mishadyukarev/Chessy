using TMPro;
using UnityEngine;

namespace Game.Game
{
    public struct TextUIC
    {
        readonly internal TextMeshProUGUI TextUI;

        internal Transform Transform => TextUI.transform;
        internal Transform Parent_T => Transform.parent;
        internal GameObject Parent_G => Parent_T.gameObject;

        public string Text
        {
            get => TextUI.text;
            internal set => TextUI.text = value;
        }
        public Color Color
        {
            get => TextUI.color;
            internal set => TextUI.color = value;
        }

        internal TextUIC(in TextMeshProUGUI text) => TextUI = text;
    }
}