using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General.Center
{
    internal struct KingZoneViewUIComp
    {
        private GameObject _kingZone_GO;
        private Button _setKing_Button;
        private TextMeshProUGUI _setKing_TextMP;

        internal KingZoneViewUIComp(GameObject centerZone_GO)
        {
            _kingZone_GO = centerZone_GO.transform.Find("KingZone").gameObject;

            _setKing_Button = _kingZone_GO.transform.Find("SetKing_Button").GetComponent<Button>();
            _setKing_TextMP = _setKing_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }
        internal void EnableZone() => _kingZone_GO.SetActive(true);
        internal void DisableZone() => _kingZone_GO.SetActive(false);

        internal void AddListenerToSetKing_Button(UnityAction unityAction) => _setKing_Button.onClick.AddListener(unityAction);

        internal void SetTextKingBut(string text) => _setKing_TextMP.text = text;
    }
}
