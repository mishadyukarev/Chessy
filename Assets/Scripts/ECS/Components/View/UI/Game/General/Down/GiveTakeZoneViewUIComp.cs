using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General.Down
{
    internal struct GiveTakeZoneViewUIComp
    {
        private GameObject _giveTakeZone_GO;

        private Button _giveTake_Button;
        private TextMeshProUGUI _giveTake_TextMP;
        private Dictionary<ToolWeaponTypes, Button> _toolAndWeapon_Buttons;
        private Dictionary<ToolWeaponTypes, TextMeshProUGUI> _toolWeapon_texsMPs;

        internal GiveTakeZoneViewUIComp(GameObject downZone_GO)
        {
            _giveTakeZone_GO = downZone_GO.transform.Find("GiveTakeZone").gameObject;

            _giveTake_Button = _giveTakeZone_GO.transform.Find("GiveThing_Button").GetComponent<Button>();
            _giveTake_TextMP = _giveTake_Button.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

            _toolAndWeapon_Buttons = new Dictionary<ToolWeaponTypes, Button>();
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Axe, _giveTakeZone_GO.transform.Find("Axe_Button").GetComponent<Button>());
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Pick, _giveTakeZone_GO.transform.Find("Pick_Button").GetComponent<Button>());
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Sword, _giveTakeZone_GO.transform.Find("Sword_Button").GetComponent<Button>());
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Crossbow, _giveTakeZone_GO.transform.Find("Crossbow_Button").GetComponent<Button>());


            _toolWeapon_texsMPs = new Dictionary<ToolWeaponTypes, TextMeshProUGUI>();

            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Pick, _toolAndWeapon_Buttons[ToolWeaponTypes.Pick].transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Sword, _toolAndWeapon_Buttons[ToolWeaponTypes.Sword].transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Crossbow, _toolAndWeapon_Buttons[ToolWeaponTypes.Crossbow].transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>());
        }

        internal void Enable_ButtonImage(ToolWeaponTypes toolAndWeaponType) => _toolAndWeapon_Buttons[toolAndWeaponType].image.enabled = true;
        internal void Disable_ButtonImage(ToolWeaponTypes toolAndWeaponType) => _toolAndWeapon_Buttons[toolAndWeaponType].image.enabled = false;

        internal void AddListener_Button(ToolWeaponTypes toolAndWeaponType, UnityAction unityAction) => _toolAndWeapon_Buttons[toolAndWeaponType].onClick.AddListener(unityAction);
        internal void AddListenerToGive_Button(UnityAction unityAction) => _giveTake_Button.onClick.AddListener(unityAction);

        internal void SetColorToGiveTake_Button(Color color) => _giveTake_Button.image.color = color;

        internal void SetTextToGiveTake_Button(string text) => _giveTake_TextMP.text = text;
        internal void SetText(ToolWeaponTypes toolWeaponType, string text) => _toolWeapon_texsMPs[toolWeaponType].text = text;
    }
}
