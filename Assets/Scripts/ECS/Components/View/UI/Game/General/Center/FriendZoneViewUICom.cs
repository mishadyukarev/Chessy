using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Components.View.UI.Game.General.Center
{
    internal struct FriendZoneViewUICom
    {
        private GameObject _parent_GO;
        private TextMeshProUGUI _playerMotion_TextMP;
        private Button _ready_Button;
        private Image _background_Image;

        internal FriendZoneViewUICom(Transform center_Trans)
        {
            var friendZone_Trans = center_Trans.Find("FriendZone");

            _parent_GO = friendZone_Trans.gameObject;

            _playerMotion_TextMP = friendZone_Trans.Find("WhoseMotion_TextMP").GetComponent<TextMeshProUGUI>();
            _ready_Button = friendZone_Trans.Find("Ready_Button").GetComponent<Button>();
            _background_Image = friendZone_Trans.Find("Image").GetComponent<Image>();
        }

        internal void SetTextPlayerMotion(string text) => _playerMotion_TextMP.text = text;

        internal void SetActiveParent(bool isActive) => _parent_GO.SetActive(isActive);

        internal void AddListenerReady(UnityAction unityAction) => _ready_Button.onClick.AddListener(unityAction);
    }
}
