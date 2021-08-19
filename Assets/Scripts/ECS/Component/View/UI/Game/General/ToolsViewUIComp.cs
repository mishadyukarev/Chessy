//using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//namespace Assets.Scripts.ECS.Component.View.UI.Game.General
//{
//    internal struct ToolsViewUIComp
//    {
//        private Dictionary<ToolWeaponTypes, TextMeshProUGUI> _toolWeapon_texsMPs;

//        internal ToolsViewUIComp(GameObject downZone_GO)
//        {
//            var toolsZone_GO = downZone_GO.transform.Find("ToolsZone").gameObject;

//            _toolWeapon_texsMPs = new Dictionary<ToolWeaponTypes, TextMeshProUGUI>();

//            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Pick, toolsZone_GO.transform.Find("AmountPicks_TextMP").GetComponent<TextMeshProUGUI>());
//            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Sword, toolsZone_GO.transform.Find("AmountSwords_TextMP").GetComponent<TextMeshProUGUI>());
//            _toolWeapon_texsMPs.Add(ToolWeaponTypes.Crossbow , toolsZone_GO.transform.Find("AmountCrossbows_TextMP").GetComponent<TextMeshProUGUI>());
//        }

//        internal void SetText(ToolWeaponTypes toolWeaponType, string text) => _toolWeapon_texsMPs[toolWeaponType].text = text;
//    }
//}
