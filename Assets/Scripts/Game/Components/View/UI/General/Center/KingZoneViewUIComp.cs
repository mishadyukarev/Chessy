using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct KingZoneViewUIComp
    {
        private GameObject _kingZone_GO;
        private Button _setKing_Button;

        internal KingZoneViewUIComp(GameObject centerZone_GO)
        {
            _kingZone_GO = centerZone_GO.transform.Find("KingZone").gameObject;

            _setKing_Button = _kingZone_GO.transform.Find("SetKing_Button").GetComponent<Button>();
        }
        internal void EnableZone() => _kingZone_GO.SetActive(true);
        internal void DisableZone() => _kingZone_GO.SetActive(false);

        internal void AddListenerToSetKing_Button(UnityAction unityAction) => _setKing_Button.onClick.AddListener(unityAction);
    }
}
