using TMPro;
using UnityEngine;

namespace Game.Game
{
    public struct TextMPUGUIC : EntityLeftEnvUIPool.ILeftEnvResTextUIE
    {
        readonly internal TextMeshProUGUI TextUI;

        public Transform Transform => TextUI.transform;
        public Transform Parent_T => Transform.parent;
        public GameObject Parent_G => Parent_T.gameObject;

        public string Text
        {
            get => TextUI.text;
            set => TextUI.text = value;
        }
        public Color Color
        {
            get => TextUI.color;
            set => TextUI.color = value;
        }

        public TextMPUGUIC(in TextMeshProUGUI text) => TextUI = text;

        public void SetActiveParent(in bool isActive) => Parent_T.gameObject.SetActive(isActive);
        public void SetActive(in bool needActive) => TextUI.gameObject.SetActive(needActive);
    }
}