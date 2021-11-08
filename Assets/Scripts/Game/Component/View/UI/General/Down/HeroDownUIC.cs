using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct HeroDownUIC
    {
        private static Button _button;

        public HeroDownUIC(Transform down)
        {
            _button = down.Find("Elffemale_Button").GetComponent<Button>();
        }

        public static void AddList(UnityAction action) => _button.onClick.AddListener(action);
        public static void SetActive(bool isActive) => _button.gameObject.SetActive(isActive);
    }
}