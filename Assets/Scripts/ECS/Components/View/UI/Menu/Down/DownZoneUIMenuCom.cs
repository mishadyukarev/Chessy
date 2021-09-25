using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Menu.Down
{
    internal struct DownZoneUIMenuCom
    {
        private Button _quit_Button;
        private TextMeshProUGUI _exit_TextMP;

        internal DownZoneUIMenuCom(Transform downZone_Trans)
        {
            _quit_Button = downZone_Trans.Find("QuitButton").GetComponent<Button>();
            _exit_TextMP = _quit_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        }

        internal void SetTextExit(string text) => _exit_TextMP.text = text;

        internal void AddListQuit_Button(UnityAction unityAction) => _quit_Button.onClick.AddListener(unityAction);
    }
}
