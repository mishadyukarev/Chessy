using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    public struct ShopZoneUICom
    {
        private static GameObject _shopZone_GO;
        private static TextMeshProUGUI _infoBuy_TextMP;
        private static Button _exit_Button;

        public ShopZoneUICom(Transform centerZone_Trans)
        {
            _shopZone_GO = centerZone_Trans.Find("ShopZone").gameObject;
            _infoBuy_TextMP = _shopZone_GO.transform.Find("BuyInfo_TextMP").GetComponent<TextMeshProUGUI>();
            _exit_Button = _shopZone_GO.transform.Find("Exit_Button").GetComponent<Button>();
        }

        public static void SetTextInfo(string text) => _infoBuy_TextMP.text = text;

        public static void EnableZone() => _shopZone_GO.SetActive(true);
        public static void DisableZone() => _shopZone_GO.SetActive(false);

        public static void AddListExit_Button(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}
