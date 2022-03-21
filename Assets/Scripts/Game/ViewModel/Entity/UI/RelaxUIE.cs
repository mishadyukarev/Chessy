using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game.Entity.View.UI.Right
{
    public readonly struct RelaxUIE
    {
        readonly Dictionary<UnitTypes, Chessy.Common.Component.GameObjectVC> _zones;

        public readonly ButtonUIC ButtonC;
        public readonly ImageUIC ImageC;

        public Chessy.Common.Component.GameObjectVC Button(in UnitTypes unit) => _zones[unit];

        public RelaxUIE(in Transform condZone)
        {
            _zones = new Dictionary<UnitTypes, Chessy.Common.Component.GameObjectVC>();

            var button = condZone.Find("StandartAbilityButton2").GetComponent<Button>();

            ButtonC = new ButtonUIC(button);
            ImageC = new ImageUIC(button.transform.Find("Image").GetComponent<Image>());

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                _zones.Add(unit, new Chessy.Common.Component.GameObjectVC(button.transform.Find(unit.ToString()).gameObject));
            }
        }
    }
}