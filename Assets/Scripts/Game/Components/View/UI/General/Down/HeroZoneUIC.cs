using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct HeroZoneUIC
    {
        private static Button _scout_Button;

        internal HeroZoneUIC(Transform downZone_Trans)
        {
            var heroZone_Trans = downZone_Trans.Find("HeroZone");

            _scout_Button = heroZone_Trans.Find("Scout_Button").GetComponent<Button>();
        }

        internal static void AddListScout(UnityAction unityAction) => _scout_Button.onClick.AddListener(unityAction);
        internal static void SetActiveScout(bool isActive) => _scout_Button.gameObject.SetActive(isActive);
    }
}