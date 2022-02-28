using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Common
{
    public struct ShopUIC
    {
        private static GameObject _shopZone_GO;
        private static Button _buy_Button;
        private static Button _exit_Button;

        public ShopUIC(Transform shopZone_Trans)
        {
            _shopZone_GO = shopZone_Trans.gameObject;
            _buy_Button = _shopZone_GO.transform.Find("Buy_Button").GetComponent<Button>();
            _exit_Button = _shopZone_GO.transform.Find("Exit_Button").GetComponent<Button>();
        }


        public static void EnableZone() => _shopZone_GO.SetActive(true);
        public static void DisableZone() => _shopZone_GO.SetActive(false);

        public static void AddListExit(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
        public static void AddListBuy(UnityAction unityAction) => _buy_Button.onClick.AddListener(unityAction);
    }
}
