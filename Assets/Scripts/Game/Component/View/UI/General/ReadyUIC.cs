using Game.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct ReadyUIC
    {
        static Button _ready;
        static Button _joinDiscord;


        public ReadyUIC(GameObject readyZone_GO)
        {
            _ready = readyZone_GO.transform.Find("ReadyButton").GetComponent<Button>();

            _joinDiscord = readyZone_GO.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _joinDiscord.onClick.AddListener(delegate { Application.OpenURL(URLC.URL_DISCORD); });
        }

        public static void SetActiveParent(bool isActive) => _ready.transform.parent.gameObject.SetActive(isActive);
        public static void SetColorReadyButton(Color color) => _ready.image.color = color;
        public static void AddListenerToReadyButton(UnityAction unityAction) => _ready.onClick.AddListener(unityAction);
    }
}
