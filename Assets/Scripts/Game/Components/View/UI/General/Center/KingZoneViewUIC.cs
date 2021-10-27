using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct KingZoneViewUIC
    {
        private static GameObject _kingZone_GO;
        private static Button _setKing_Button;

        internal KingZoneViewUIC(GameObject centerZone_GO)
        {
            _kingZone_GO = centerZone_GO.transform.Find("KingZone").gameObject;

            _setKing_Button = _kingZone_GO.transform.Find("SetKing_Button").GetComponent<Button>();
        }
        internal static void EnableZone() => _kingZone_GO.SetActive(true);
        internal static void DisableZone() => _kingZone_GO.SetActive(false);

        internal static void AddListenerToSetKing_Button(UnityAction unityAction) => _setKing_Button.onClick.AddListener(unityAction);
    }
}
