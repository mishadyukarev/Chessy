using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General
{
    internal struct ReadyViewUICom
    {
        private TextMeshProUGUI _wait_TextMP;

        private Button _ready_Button;
        private TextMeshProUGUI _ready_TextMP;

        private TextMeshProUGUI _joinForFinding_TextMP;

        private Button _joinDiscord_Button;


        internal ReadyViewUICom(GameObject readyZone_GO)
        {
            _wait_TextMP = readyZone_GO.transform.Find("Wait_TextMP").GetComponent<TextMeshProUGUI>();
            _ready_Button = readyZone_GO.transform.Find("ReadyButton").GetComponent<Button>();
            _ready_TextMP = _ready_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _joinForFinding_TextMP = readyZone_GO.transform.Find("JoinForFind_TextMP").GetComponent<TextMeshProUGUI>();
            _joinDiscord_Button = readyZone_GO.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _joinDiscord_Button.onClick.AddListener(delegate { Application.OpenURL(Main.DISCORD_REFERENCE); } );
        }

        internal void SetActiveParent(bool isActive) => _ready_Button.transform.parent.gameObject.SetActive(isActive);
        internal void SetColorReadyButton(Color color) => _ready_Button.image.color = color;
        internal void AddListenerToReadyButton(UnityAction unityAction) => _ready_Button.onClick.AddListener(unityAction);

        internal void SetTextWait(string text) => _wait_TextMP.text = text;
        internal void SetTextReady(string text) => _ready_TextMP.text = text;

        internal void SetTextJoinForFind(string text) => _joinForFinding_TextMP.text = text;
    }
}
