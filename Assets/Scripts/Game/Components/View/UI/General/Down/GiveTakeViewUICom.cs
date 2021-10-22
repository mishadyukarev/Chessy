using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Scripts.Common;

namespace Scripts.Game
{
    internal struct GiveTakeViewUICom
    {
        private Button _upgradeUnit_But;
        private Dictionary<ToolWeaponTypes, Button> _toolAndWeapon_Buts;
        private Dictionary<ToolWeaponTypes, TextMeshProUGUI> _toolWeaponAmount_texsMPs;
        private Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, Image>> _toolWeapon_Images;

        internal GiveTakeViewUICom(GameObject downZone_GO)
        {
            var gTZone_Trans = downZone_GO.transform.Find("GiveTakeZone");


            _upgradeUnit_But = gTZone_Trans.Find("UpgradeUnit_Button").GetComponent<Button>();


            _toolAndWeapon_Buts = new Dictionary<ToolWeaponTypes, Button>();
            _toolAndWeapon_Buts.Add(ToolWeaponTypes.Pick, gTZone_Trans.Find("Pick_Button").GetComponent<Button>());
            _toolAndWeapon_Buts.Add(ToolWeaponTypes.Sword, gTZone_Trans.Find("Sword_Button").GetComponent<Button>());
            _toolAndWeapon_Buts.Add(ToolWeaponTypes.Shield, gTZone_Trans.Find("Shield_Button").GetComponent<Button>());


            _toolWeaponAmount_texsMPs = new Dictionary<ToolWeaponTypes, TextMeshProUGUI>();
            _toolWeaponAmount_texsMPs.Add(ToolWeaponTypes.Pick, _toolAndWeapon_Buts[ToolWeaponTypes.Pick].transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeaponAmount_texsMPs.Add(ToolWeaponTypes.Sword, _toolAndWeapon_Buts[ToolWeaponTypes.Sword].transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeaponAmount_texsMPs.Add(ToolWeaponTypes.Shield, _toolAndWeapon_Buts[ToolWeaponTypes.Shield].transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>());


            _toolWeapon_Images = new Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, Image>>();
            _toolWeapon_Images.Add(ToolWeaponTypes.Pick, new Dictionary<LevelTWTypes, Image>());
            _toolWeapon_Images.Add(ToolWeaponTypes.Sword, new Dictionary<LevelTWTypes, Image>());
            _toolWeapon_Images.Add(ToolWeaponTypes.Shield, new Dictionary<LevelTWTypes, Image>());



            _toolWeapon_Images[ToolWeaponTypes.Pick][LevelTWTypes.Wood] = _toolAndWeapon_Buts[ToolWeaponTypes.Pick].transform.Find("PickWood_Image").GetComponent<Image>();
            _toolWeapon_Images[ToolWeaponTypes.Pick][LevelTWTypes.Iron] = _toolAndWeapon_Buts[ToolWeaponTypes.Pick].transform.Find("PickIron_Image").GetComponent<Image>();
            _toolWeapon_Images[ToolWeaponTypes.Sword][LevelTWTypes.Wood] = _toolAndWeapon_Buts[ToolWeaponTypes.Sword].transform.Find("SwordWood_Image").GetComponent<Image>();
            _toolWeapon_Images[ToolWeaponTypes.Sword][LevelTWTypes.Iron] = _toolAndWeapon_Buts[ToolWeaponTypes.Sword].transform.Find("SwordIron_Image").GetComponent<Image>();
            _toolWeapon_Images[ToolWeaponTypes.Shield][LevelTWTypes.Wood] = _toolAndWeapon_Buts[ToolWeaponTypes.Shield].transform.Find("ShieldWood_Image").GetComponent<Image>();
            _toolWeapon_Images[ToolWeaponTypes.Shield][LevelTWTypes.Iron] = _toolAndWeapon_Buts[ToolWeaponTypes.Shield].transform.Find("ShieldIron_Image").GetComponent<Image>();
        }

        internal void SetView_ButtonImage(ToolWeaponTypes giveTakeType, bool isActive)
        {
            var image = _toolAndWeapon_Buts[giveTakeType].image;

            if (isActive) image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            else image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }

        internal void AddListUpgradeButton(UnityAction unityAction) => _upgradeUnit_But.onClick.AddListener(unityAction);
        internal void AddList_Button(ToolWeaponTypes giveTakeType, UnityAction unityAction) => _toolAndWeapon_Buts[giveTakeType].onClick.AddListener(unityAction);

        internal void SetText(ToolWeaponTypes giveTakeType, string text) => _toolWeaponAmount_texsMPs[giveTakeType].text = text;
        internal void SetImage(ToolWeaponTypes giveTakeType, LevelTWTypes levelTWType)
        {
            if(levelTWType == LevelTWTypes.Wood)
            {
                _toolWeapon_Images[giveTakeType][levelTWType].gameObject.SetActive(true);
                _toolWeapon_Images[giveTakeType][LevelTWTypes.Iron].gameObject.SetActive(false);
            }
            else
            {
                _toolWeapon_Images[giveTakeType][levelTWType].gameObject.SetActive(true);
                _toolWeapon_Images[giveTakeType][LevelTWTypes.Wood].gameObject.SetActive(false);
            }
        }
    }
}
