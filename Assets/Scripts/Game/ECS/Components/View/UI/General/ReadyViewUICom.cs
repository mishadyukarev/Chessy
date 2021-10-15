using Scripts.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct ReadyViewUICom
    {
        private Button _ready_Button;
        private Button _joinDiscord_Button;


        internal ReadyViewUICom(GameObject readyZone_GO)
        {
            _ready_Button = readyZone_GO.transform.Find("ReadyButton").GetComponent<Button>();

            _joinDiscord_Button = readyZone_GO.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _joinDiscord_Button.onClick.AddListener(delegate { Application.OpenURL(URL.URL_DISCORD); });
        }

        internal void SetActiveParent(bool isActive) => _ready_Button.transform.parent.gameObject.SetActive(isActive);
        internal void SetColorReadyButton(Color color) => _ready_Button.image.color = color;
        internal void AddListenerToReadyButton(UnityAction unityAction) => _ready_Button.onClick.AddListener(unityAction);
    }
}
