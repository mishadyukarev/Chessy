using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct HeroesViewUIC
    {
        private static GameObject _parent;
        private static Button _elffemale;
        private static Button _premium;

        public HeroesViewUIC(Transform centerZone)
        {
            _parent = centerZone.transform.Find("HeroesZone").gameObject;

            _elffemale = _parent.transform.Find("Elffemale_But").GetComponent<Button>();
            _premium = _parent.transform.Find("Premium_Button").GetComponent<Button>();
        }

        public static void AddListElf(UnityAction action) => _elffemale.onClick.AddListener(action);
        public static void AddListPremium(UnityAction action) => _premium.onClick.AddListener(action);
        public static void SetActiveZone(bool isActive) => _parent.SetActive(isActive);
    }
}