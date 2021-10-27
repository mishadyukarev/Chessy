using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct FriendZoneViewUIC
    {
        private static GameObject _parent_GO;
        private static TextMeshProUGUI _playerMotion_TextMP;
        private static Button _ready_Button;
        private static Image _background_Image;

        internal FriendZoneViewUIC(Transform center_Trans)
        {
            var friendZone_Trans = center_Trans.Find("FriendZone");

            _parent_GO = friendZone_Trans.gameObject;

            _playerMotion_TextMP = friendZone_Trans.Find("WhoseMotion_TextMP").GetComponent<TextMeshProUGUI>();
            _ready_Button = friendZone_Trans.Find("Ready_Button").GetComponent<Button>();
            _background_Image = friendZone_Trans.Find("Image").GetComponent<Image>();
        }

        internal static void SetTextPlayerMotion(string text) => _playerMotion_TextMP.text = text;

        internal static void SetActiveParent(bool isActive) => _parent_GO.SetActive(isActive);

        internal static void AddListenerReady(UnityAction unityAction) => _ready_Button.onClick.AddListener(unityAction);
    }
}
