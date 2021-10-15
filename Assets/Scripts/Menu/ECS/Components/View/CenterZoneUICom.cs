using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct CenterZoneUICom
    {
        private TextMeshProUGUI _log_TextMP;

        private Slider _musicSlider;
        private Toggle _hint_Toggle;

        private Button _help_Button;
        private Button _discord_But;

        private Button _likeGame_Button;
        private Button _exit_Button;


        internal float MusicVolume => _musicSlider.value;
        internal bool InteractableHint => _hint_Toggle.interactable;

        internal CenterZoneUICom(Transform centerZone_Trans, float value)
        {
            _log_TextMP = centerZone_Trans.Find("Log_TextMP").GetComponent<TextMeshProUGUI>();

            _musicSlider = centerZone_Trans.Find("Slider").GetComponent<Slider>();
            _musicSlider.value = value;
            _hint_Toggle = centerZone_Trans.Find("Hint_Toggle").GetComponent<Toggle>();

            _discord_But = centerZone_Trans.transform.Find("JoinDiscordButton").GetComponent<Button>();
            _help_Button = centerZone_Trans.Find("Help_Button").GetComponent<Button>();

            _likeGame_Button = centerZone_Trans.Find("LikeGame_Button").GetComponent<Button>();
            _exit_Button = centerZone_Trans.Find("QuitButton").GetComponent<Button>();
        }

        internal void SetLogText(string text) => _log_TextMP.text = text;

        internal void AddListDiscord_But(UnityAction unityAction) => _discord_But.onClick.AddListener(unityAction);
        internal void AddListHelp_But(UnityAction unityAction) => _help_Button.onClick.AddListener(unityAction);
        internal void AddListLikeGame_But(UnityAction unityAction) => _likeGame_Button.onClick.AddListener(unityAction);
        internal void AddListQuit_But(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}
