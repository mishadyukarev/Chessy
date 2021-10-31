﻿using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Game
{
    public struct EffectsIUC
    {
        private static Dictionary<StatTypes, Image> _effects_Images;

        public EffectsIUC(Transform rightZone_Trans)
        {
            var effects_Trans = rightZone_Trans.Find("EffectsZone");

            _effects_Images = new Dictionary<StatTypes, Image>();
            _effects_Images.Add(StatTypes.Health, effects_Trans.Find("Hp_Image").Find("Arrow_Image").GetComponent<Image>());
            _effects_Images.Add(StatTypes.Damage, effects_Trans.Find("Damage_Image").Find("Arrow_Image").GetComponent<Image>());
            _effects_Images.Add(StatTypes.Steps, effects_Trans.Find("Step_Image").Find("Arrow_Image").GetComponent<Image>());
        }

        public static void SetColor(StatTypes statType, bool isActive)
        {
            if (isActive) _effects_Images[statType].color = Color.green;
            else _effects_Images[statType].color = Color.white;
        }
    }
}