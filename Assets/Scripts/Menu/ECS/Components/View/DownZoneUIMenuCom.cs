using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Menu.Down
{
    internal struct DownZoneUIMenuCom
    {
        private Button _help_Button;
        private TextMeshProUGUI _help_TextMP;

        private Button _exit_Button;
        private TextMeshProUGUI _exit_TextMP;

        internal DownZoneUIMenuCom(Transform downZone_Trans)
        {
            _help_Button = downZone_Trans.Find("Help_Button").GetComponent<Button>();
            _help_TextMP = _help_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _exit_Button = downZone_Trans.Find("QuitButton").GetComponent<Button>();
            _exit_TextMP = _exit_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void SetTextHelp(string text) => _help_TextMP.text = text;
        internal void SetTextExit(string text) => _exit_TextMP.text = text;

        internal void AddListHelp_Button(UnityAction unityAction) => _help_Button.onClick.AddListener(unityAction);
        internal void AddListQuit_Button(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}
