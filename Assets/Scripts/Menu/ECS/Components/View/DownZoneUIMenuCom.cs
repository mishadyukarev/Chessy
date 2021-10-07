using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Menu
{
    internal struct DownZoneUIMenuCom
    {
        private Button _help_Button;

        private Button _exit_Button;

        internal DownZoneUIMenuCom(Transform downZone_Trans)
        {
            _help_Button = downZone_Trans.Find("Help_Button").GetComponent<Button>();

            _exit_Button = downZone_Trans.Find("QuitButton").GetComponent<Button>();
        }
        internal void AddListHelp_Button(UnityAction unityAction) => _help_Button.onClick.AddListener(unityAction);
        internal void AddListQuit_Button(UnityAction unityAction) => _exit_Button.onClick.AddListener(unityAction);
    }
}
