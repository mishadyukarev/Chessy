using TMPro;
using UnityEngine;

namespace Assets.Scripts.ECS.Components.View.UI.Game.General.Center
{
    internal struct SelectorTypeViewUIComp
    {
        private GameObject _parent_GO;
        private TextMeshProUGUI _text_MP;

        internal string Text
        {
            get => _text_MP.text;
            set => _text_MP.text = value;
        }

        internal SelectorTypeViewUIComp(GameObject centerZone_GO)
        {
            _parent_GO = centerZone_GO.transform.Find("SelectorTypeZone").gameObject;
            _text_MP = _parent_GO.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }


        internal void EnableParent() => _parent_GO.SetActive(true);
        internal void DisableParent() => _parent_GO.SetActive(false);
    }
}
