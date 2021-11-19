using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct KingZoneViewUIC
    {
        private static GameObject _kingZone_GO;
        private static Button _setKing_Button;

        public static bool IsActiveZone => _kingZone_GO.activeSelf;

        public KingZoneViewUIC(GameObject centerZone_GO)
        {
            _kingZone_GO = centerZone_GO.transform.Find("KingZone").gameObject;

            _setKing_Button = _kingZone_GO.transform.Find("SetKing_Button").GetComponent<Button>();
        }
        public static void EnableZone() => _kingZone_GO.SetActive(true);
        public static void DisableZone() => _kingZone_GO.SetActive(false);

        public static void AddListenerToSetKing_Button(UnityAction unityAction) => _setKing_Button.onClick.AddListener(unityAction);
    }
}
