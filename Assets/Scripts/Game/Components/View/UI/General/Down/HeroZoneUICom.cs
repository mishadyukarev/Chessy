using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct HeroZoneUICom
    {
        private Button _scout_Button;

        internal HeroZoneUICom(Transform downZone_Trans)
        {
            var heroZone_Trans = downZone_Trans.Find("HeroZone");

            _scout_Button = heroZone_Trans.Find("Scout_Button").GetComponent<Button>();
        }

        internal void AddListScout(UnityAction unityAction) => _scout_Button.onClick.AddListener(unityAction);
        internal void SetActiveScout(bool isActive) => _scout_Button.gameObject.SetActive(isActive);
    }
}