using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public struct TwGiveTakeUIC
    {

        private static Dictionary<TWTypes, Button> _toolAndWeapon_Buts;
        private static Dictionary<TWTypes, TextMeshProUGUI> _toolWeaponAmount_texsMPs;
        private static Dictionary<TWTypes, Dictionary<LevelTypes, Image>> _toolWeapon_Images;

        public TwGiveTakeUIC(GameObject downZone_GO)
        {
            var gTZone_Trans = downZone_GO.transform.Find("GiveTakeZone");


            _toolAndWeapon_Buts = new Dictionary<TWTypes, Button>();
            _toolAndWeapon_Buts.Add(TWTypes.Pick, gTZone_Trans.Find("Pick_Button").GetComponent<Button>());
            _toolAndWeapon_Buts.Add(TWTypes.Sword, gTZone_Trans.Find("Sword_Button").GetComponent<Button>());
            _toolAndWeapon_Buts.Add(TWTypes.Shield, gTZone_Trans.Find("Shield_Button").GetComponent<Button>());


            _toolWeaponAmount_texsMPs = new Dictionary<TWTypes, TextMeshProUGUI>();
            _toolWeaponAmount_texsMPs.Add(TWTypes.Pick, _toolAndWeapon_Buts[TWTypes.Pick].transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeaponAmount_texsMPs.Add(TWTypes.Sword, _toolAndWeapon_Buts[TWTypes.Sword].transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>());
            _toolWeaponAmount_texsMPs.Add(TWTypes.Shield, _toolAndWeapon_Buts[TWTypes.Shield].transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>());


            _toolWeapon_Images = new Dictionary<TWTypes, Dictionary<LevelTypes, Image>>();
            _toolWeapon_Images.Add(TWTypes.Pick, new Dictionary<LevelTypes, Image>());
            _toolWeapon_Images.Add(TWTypes.Sword, new Dictionary<LevelTypes, Image>());
            _toolWeapon_Images.Add(TWTypes.Shield, new Dictionary<LevelTypes, Image>());



            _toolWeapon_Images[TWTypes.Pick][LevelTypes.First] = _toolAndWeapon_Buts[TWTypes.Pick].transform.Find("PickWood_Image").GetComponent<Image>();
            _toolWeapon_Images[TWTypes.Pick][LevelTypes.Second] = _toolAndWeapon_Buts[TWTypes.Pick].transform.Find("PickIron_Image").GetComponent<Image>();
            _toolWeapon_Images[TWTypes.Sword][LevelTypes.First] = _toolAndWeapon_Buts[TWTypes.Sword].transform.Find("SwordWood_Image").GetComponent<Image>();
            _toolWeapon_Images[TWTypes.Sword][LevelTypes.Second] = _toolAndWeapon_Buts[TWTypes.Sword].transform.Find("SwordIron_Image").GetComponent<Image>();
            _toolWeapon_Images[TWTypes.Shield][LevelTypes.First] = _toolAndWeapon_Buts[TWTypes.Shield].transform.Find("ShieldWood_Image").GetComponent<Image>();
            _toolWeapon_Images[TWTypes.Shield][LevelTypes.Second] = _toolAndWeapon_Buts[TWTypes.Shield].transform.Find("ShieldIron_Image").GetComponent<Image>();
        }

        public static void SetView_ButtonImage(TWTypes giveTakeType, bool isActive)
        {
            var image = _toolAndWeapon_Buts[giveTakeType].image;

            if (isActive) image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
            else image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }

        
        public static void AddList_Button(TWTypes giveTakeType, UnityAction unityAction) => _toolAndWeapon_Buts[giveTakeType].onClick.AddListener(unityAction);

        public static void SetText(TWTypes giveTakeType, string text) => _toolWeaponAmount_texsMPs[giveTakeType].text = text;
        public static void SetImage(TWTypes giveTakeType, LevelTypes levelTWType)
        {
            if (levelTWType == LevelTypes.First)
            {
                _toolWeapon_Images[giveTakeType][levelTWType].gameObject.SetActive(true);
                _toolWeapon_Images[giveTakeType][LevelTypes.Second].gameObject.SetActive(false);
            }
            else
            {
                _toolWeapon_Images[giveTakeType][levelTWType].gameObject.SetActive(true);
                _toolWeapon_Images[giveTakeType][LevelTypes.First].gameObject.SetActive(false);
            }
        }
    }
}
