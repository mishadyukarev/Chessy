using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct EffectsIUC
    {
        private static Dictionary<UnitStatTypes, Image> _effects_Images;

        public EffectsIUC(Transform rightZone_Trans)
        {
            var effects_Trans = rightZone_Trans.Find("EffectsZone");

            _effects_Images = new Dictionary<UnitStatTypes, Image>();
            _effects_Images.Add(UnitStatTypes.Hp, effects_Trans.Find("Hp_Image").Find("Arrow_Image").GetComponent<Image>());
            _effects_Images.Add(UnitStatTypes.Damage, effects_Trans.Find("Damage_Image").Find("Arrow_Image").GetComponent<Image>());
            _effects_Images.Add(UnitStatTypes.Steps, effects_Trans.Find("Step_Image").Find("Arrow_Image").GetComponent<Image>());
        }

        public static void SetColor(UnitStatTypes statType, bool isActive)
        {
            if (isActive) _effects_Images[statType].color = Color.green;
            else _effects_Images[statType].color = Color.white;
        }
    }
}