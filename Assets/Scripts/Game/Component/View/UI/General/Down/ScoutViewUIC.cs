using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct ScoutViewUIC
    {
        private static Button _button;

        public ScoutViewUIC(Transform down)
        {
            //var heroZone_Trans = downZone_Trans.Find("HeroZone");

            _button = down.Find("Scout_Button").GetComponent<Button>();
        }

        public static void AddListScout(UnityAction unityAction) => _button.onClick.AddListener(unityAction);
        public static void SetActiveScout(bool isActive) => _button.gameObject.SetActive(isActive);
    }
}