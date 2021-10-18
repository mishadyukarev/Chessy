using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.Game
{
    internal struct GiveTakeZoneViewUIComp
    {
        private GameObject _giveTakeZone_GO;

        private Button _upgradeUnit_But;
        private Dictionary<ToolWeaponTypes, Button> _toolAndWeapon_Buts;
        private Dictionary<ToolWeaponTypes, TextMeshProUGUI> _toolWeapon_texsMPs;

        internal GiveTakeZoneViewUIComp(GameObject downZone_GO)
        {
            _giveTakeZone_GO = downZone_GO.transform.Find("GiveTakeZone").gameObject;


            _upgradeUnit_But = _giveTakeZone_GO.transform.Find("UpgradeUnit_Button").GetComponent<Button>();

            _toolAndWeapon_Buts = new Dictionary<ToolWeaponTypes, Button>();
            _toolAndWeapon_Buts.Add(ToolWeaponTypes.Pick, _giveTakeZone_GO.transform.Find("Pick_Button").GetComponent<Button>());
            _toolAndWeapon_Buts.Add(ToolWeaponTypes.Sword, _giveTakeZone_GO.transform.Find("Sword_Button").GetComponent<Button>());
            _toolAndWeapon_Buts.Add(ToolWeaponTypes.Shield, _giveTakeZone_GO.transform.Find("Shield_Button").GetComponent<Button>());


            _toolWeapon_texsMPs = new Dictionary<ToolWeaponTypes, TextMeshProUGUI>();

            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Pick, _toolAndWeapon_Buts[ToolWeaponTypes.Pick].transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Sword, _toolAndWeapon_Buts[ToolWeaponTypes.Sword].transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Shield, _toolAndWeapon_Buts[ToolWeaponTypes.Shield].transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>());
        }

        internal void Enable_ButtonImage(ToolWeaponTypes toolWeaponType) => _toolAndWeapon_Buts[toolWeaponType].image.enabled = true;
        internal void Disable_ButtonImage(ToolWeaponTypes toolWeaponType) => _toolAndWeapon_Buts[toolWeaponType].image.enabled = false;

        internal void AddListUpgradeButton(UnityAction unityAction) => _upgradeUnit_But.onClick.AddListener(unityAction);
        internal void AddList_Button(ToolWeaponTypes toolWeaponType, UnityAction unityAction) => _toolAndWeapon_Buts[toolWeaponType].onClick.AddListener(unityAction);

        internal void SetText(ToolWeaponTypes toolWeaponType, string text) => _toolWeapon_texsMPs[toolWeaponType].text = text;
    }
}
