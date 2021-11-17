using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct ExtraTWZoneUIC
    {
        private static Dictionary<TWTypes, Dictionary<LevelTypes, Image>> _tw_Images;

        public ExtraTWZoneUIC(Transform right_Trans)
        {
            var additionZone_Trans = right_Trans.Find("AdditionZone");

            _tw_Images = new Dictionary<TWTypes, Dictionary<LevelTypes, Image>>();
            _tw_Images.Add(TWTypes.Pick, new Dictionary<LevelTypes, Image>());
            _tw_Images.Add(TWTypes.Sword, new Dictionary<LevelTypes, Image>());
            _tw_Images.Add(TWTypes.Shield, new Dictionary<LevelTypes, Image>());

            _tw_Images[TWTypes.Pick].Add(LevelTypes.Second, additionZone_Trans.Find("PickIron_Image").GetComponent<Image>());
            _tw_Images[TWTypes.Sword].Add(LevelTypes.Second, additionZone_Trans.Find("SwordIron_Image").GetComponent<Image>());
            _tw_Images[TWTypes.Shield].Add(LevelTypes.First, additionZone_Trans.Find("ShieldWood_Image").GetComponent<Image>());
            _tw_Images[TWTypes.Shield].Add(LevelTypes.Second, additionZone_Trans.Find("ShieldIron_Image").GetComponent<Image>());
        }

        public static void Toggle(TWTypes tWType, LevelTypes level, bool isActive) => _tw_Images[tWType][level].gameObject.SetActive(isActive);
        public static void DisableAll()
        {
            foreach (var ss in _tw_Images.Values)
                foreach (var image in ss.Values)
                    image.gameObject.SetActive(false);
        }
    }
}