using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct DonerViewUIComponent
    {
        private Button _doner_Button;
        private TextMeshProUGUI _waitPlayer_TextMP;

        internal DonerViewUIComponent(GameObject downZone_GO)
        {
            _doner_Button = downZone_GO.transform.Find("DonerButton").GetComponent<Button>();
            _waitPlayer_TextMP = downZone_GO.transform.Find("WaitPlayer_TextMP").GetComponent<TextMeshProUGUI>();
        }

        internal void AddListener(UnityAction unityAction) => _doner_Button.onClick.AddListener(unityAction);
        internal void SetColor(Color color) => _doner_Button.image.color = color;


        internal void EnableWait() => _waitPlayer_TextMP.gameObject.SetActive(true);
        internal void DisableWait() => _waitPlayer_TextMP.gameObject.SetActive(false);
    }
}
