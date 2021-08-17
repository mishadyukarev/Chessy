using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ECS.Component.View.UI.Game.General.Down
{
    internal struct GiveThingZoneViewUIComp
    {
        private GameObject _giveZone_GO;

        private Button _give_Button;
        private Dictionary<ToolWeaponTypes, Button> _toolAndWeapon_Buttons;

        internal GiveThingZoneViewUIComp(GameObject downZone_GO)
        {
            _giveZone_GO = downZone_GO.transform.Find("GiveZone").gameObject;

            _give_Button = _giveZone_GO.transform.Find("GiveThing_Button").GetComponent<Button>();

            _toolAndWeapon_Buttons = new Dictionary<ToolWeaponTypes, Button>();
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Pick, _giveZone_GO.transform.Find("Pick_Button").GetComponent<Button>());
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Sword, _giveZone_GO.transform.Find("Sword_Button").GetComponent<Button>());
            _toolAndWeapon_Buttons.Add(ToolWeaponTypes.Crossbow, _giveZone_GO.transform.Find("Crossbow_Button").GetComponent<Button>());
        }

        internal void Enable_ButtonImage(ToolWeaponTypes toolAndWeaponType) => _toolAndWeapon_Buttons[toolAndWeaponType].image.enabled = true;
        internal void Disable_ButtonImage(ToolWeaponTypes toolAndWeaponType) => _toolAndWeapon_Buttons[toolAndWeaponType].image.enabled = false;

        internal void AddListener_Button(ToolWeaponTypes toolAndWeaponType, UnityAction unityAction) => _toolAndWeapon_Buttons[toolAndWeaponType].onClick.AddListener(unityAction);
        internal void AddListenerToGive_Button(UnityAction unityAction) => _give_Button.onClick.AddListener(unityAction);
    }
}
