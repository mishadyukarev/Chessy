using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct RightProtectUIE
    {
        readonly Dictionary<UnitTypes, Chessy.Common.Component.GameObjectVC> _zones;

        public ButtonUIC ButtonC;
        public ImageUIC ImageUIC;

        public Chessy.Common.Component.GameObjectVC Button(in UnitTypes unit) => _zones[unit];

        public RightProtectUIE(in Transform condZone)
        {
            _zones = new Dictionary<UnitTypes, Chessy.Common.Component.GameObjectVC>();

            var button = condZone.Find("StandartAbilityButton1").GetComponent<Button>();

            ButtonC = new ButtonUIC(button);
            ImageUIC = new ImageUIC(button.transform.Find("Image").GetComponent<Image>());


            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                _zones.Add(unit, new Chessy.Common.Component.GameObjectVC(button.transform.Find(unit.ToString()).gameObject));
            }
        }
    }
}