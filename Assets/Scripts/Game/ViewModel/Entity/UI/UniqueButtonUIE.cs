using Chessy.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.View.UI.Entity.Right
{
    public readonly struct UniqueButtonUIE
    {
        readonly Dictionary<AbilityTypes, Chessy.Common.Component.GameObjectVC> _zones;

        public readonly Chessy.Common.Component.GameObjectVC ParenC;
        public readonly ButtonUIC ButtonC;
        public readonly TextUIC CooldonwTextC;
        public readonly ImageUIC AbilityImageC;

        public readonly TextUIC StepsTextC;
        public readonly TextUIC WaterTextC;
        public readonly TextUIC WoodTextC;

        public Chessy.Common.Component.GameObjectVC Zone(in AbilityTypes abilityT) => _zones[abilityT];

        public UniqueButtonUIE(in ButtonTypes buttonT, in Transform button)
        {
            ParenC = new Chessy.Common.Component.GameObjectVC(button.gameObject);
            ButtonC = new ButtonUIC(button.Find("Button").GetComponent<Button>());
            CooldonwTextC = new TextUIC(button.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            AbilityImageC = new ImageUIC(button.Find("Ability_Image").GetComponent<Image>());

            StepsTextC = new TextUIC(button.Find("Steps+").Find("Steps_TMP+").GetComponent<TextMeshProUGUI>());
            WaterTextC = new TextUIC(button.Find("Water+").Find("Water_TMP+").GetComponent<TextMeshProUGUI>());
            WoodTextC = new TextUIC(button.Find("Wood+").Find("Wood_TMP+").GetComponent<TextMeshProUGUI>());


            _zones = new Dictionary<AbilityTypes, Chessy.Common.Component.GameObjectVC>();

            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _zones.Add(ability, new Chessy.Common.Component.GameObjectVC(button.Find("Zones").Find(ability.ToString()).gameObject));
            }
        }
    }
}