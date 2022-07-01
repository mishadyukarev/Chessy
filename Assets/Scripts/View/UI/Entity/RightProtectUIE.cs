using Chessy.Model;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct RightProtectUIE
    {
        readonly Dictionary<UnitTypes, GameObjectVC> _zones;

        public ButtonUIC ButtonC;
        public ImageUIC ImageUIC;

        public GameObjectVC Button(in UnitTypes unit) => _zones[unit];

        public RightProtectUIE(in Transform condZone)
        {
            _zones = new Dictionary<UnitTypes, GameObjectVC>();

            var defendZone = condZone.Find("Defend+");

            var button = defendZone.Find("Button+").GetComponent<Button>();

            ButtonC = new ButtonUIC(button);
            ImageUIC = new ImageUIC(defendZone.Find("Image+").GetComponent<Image>());


            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                _zones.Add(unit, new GameObjectVC(defendZone.Find(unit.ToString()).gameObject));
            }
        }
    }
}