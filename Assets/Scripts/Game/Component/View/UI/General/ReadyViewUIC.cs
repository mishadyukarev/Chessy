using Scripts.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct ReadyViewUIC
    {
        private static Button _ready_Button;
        private static Button _joinDiscord_Button;


        public ReadyViewUIC(GameObject readyZone_GO)
        {
            _ready_Button = readyZone_GO.transform.Find("ReadyButton").GetComponent<Button>();

            _joinDiscord_Button = readyZone_GO.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _joinDiscord_Button.onClick.AddListener(delegate { Application.OpenURL(URL.URL_DISCORD); });
        }

        public static void SetActiveParent(bool isActive) => _ready_Button.transform.parent.gameObject.SetActive(isActive);
        public static void SetColorReadyButton(Color color) => _ready_Button.image.color = color;
        public static void AddListenerToReadyButton(UnityAction unityAction) => _ready_Button.onClick.AddListener(unityAction);
    }
}
