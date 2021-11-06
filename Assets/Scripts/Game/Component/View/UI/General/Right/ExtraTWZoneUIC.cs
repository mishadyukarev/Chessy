using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct ExtraTWZoneUIC
    {
        private static Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, Image>> _tw_Images;

        public ExtraTWZoneUIC(Transform right_Trans)
        {
            var additionZone_Trans = right_Trans.Find("AdditionZone");

            _tw_Images = new Dictionary<ToolWeaponTypes, Dictionary<LevelTWTypes, Image>>();
            _tw_Images.Add(ToolWeaponTypes.Pick, new Dictionary<LevelTWTypes, Image>());
            _tw_Images.Add(ToolWeaponTypes.Sword, new Dictionary<LevelTWTypes, Image>());
            _tw_Images.Add(ToolWeaponTypes.Shield, new Dictionary<LevelTWTypes, Image>());

            _tw_Images[ToolWeaponTypes.Pick].Add(LevelTWTypes.Iron, additionZone_Trans.Find("PickIron_Image").GetComponent<Image>());
            _tw_Images[ToolWeaponTypes.Sword].Add(LevelTWTypes.Iron, additionZone_Trans.Find("SwordIron_Image").GetComponent<Image>());
            _tw_Images[ToolWeaponTypes.Shield].Add(LevelTWTypes.Wood, additionZone_Trans.Find("ShieldWood_Image").GetComponent<Image>());
            _tw_Images[ToolWeaponTypes.Shield].Add(LevelTWTypes.Iron, additionZone_Trans.Find("ShieldIron_Image").GetComponent<Image>());
        }

        public static void Toggle(ToolWeaponTypes tWType, LevelTWTypes level, bool isActive) => _tw_Images[tWType][level].gameObject.SetActive(isActive);
        public static void DisableAll()
        {
            foreach (var ss in _tw_Images.Values)
                foreach (var image in ss.Values)
                    image.gameObject.SetActive(false);
        }
    }
}