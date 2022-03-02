using TMPro;
using UnityEngine;

namespace Chessy.Game
{
    public struct TextUIC
    {
        public TextMeshProUGUI TextUI;

        public Transform Transform => TextUI.transform;
        public Transform Parent_T => Transform.parent;
        public GameObject Parent_G => Parent_T.gameObject;

        public TextUIC(in TextMeshProUGUI text) => TextUI = text;

        public void SetActiveParent(in bool isActive) => Parent_T.gameObject.SetActive(isActive);
        public void SetActive(in bool needActive) => TextUI.gameObject.SetActive(needActive);
    }
}