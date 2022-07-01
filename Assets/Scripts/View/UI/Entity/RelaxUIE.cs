using Chessy.Model;
using Chessy.Model.Component;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct RelaxUIE
    {
        readonly GameObjectVC[] _zones;

        public readonly AnimationVC AnimationC;
        public readonly ButtonUIC ButtonC;
        public readonly ImageUIC ImageC;

        public GameObjectVC Button(in UnitTypes unit) => _zones[(byte)unit];

        public RelaxUIE(in Transform condZone)
        {
            _zones = new GameObjectVC[(byte)UnitTypes.End];

            var relax = condZone.Find("Relax+");

            AnimationC = new AnimationVC(relax.GetComponent<Animation>());

            var button = relax.Find("Button+").GetComponent<Button>();

            ButtonC = new ButtonUIC(button);
            ImageC = new ImageUIC(relax.Find("Image+").GetComponent<Image>());

            for (var unit = UnitTypes.None + 1; unit < UnitTypes.End; unit++)
            {
                _zones[(byte)unit] = new GameObjectVC(relax.Find(unit.ToString()).gameObject);
            }
        }
    }
}