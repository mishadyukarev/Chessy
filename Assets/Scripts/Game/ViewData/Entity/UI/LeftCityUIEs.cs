using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct LeftCityUIEs
    {
        readonly Dictionary<BuildingTypes, ButtonUIC> _ents;
        public ButtonUIC BuildE(in BuildingTypes buttonT) => _ents[buttonT];

        public readonly GameObjectVC Zone;

        internal LeftCityUIEs(in Transform leftZone)
        {
            _ents = new Dictionary<BuildingTypes, ButtonUIC>();


            var buildZone = leftZone.transform.Find("City+");

            Zone = new GameObjectVC(buildZone.gameObject);

            for (var buildT = BuildingTypes.House; buildT <= BuildingTypes.Smelter; buildT++)
            {
                _ents.Add(buildT, new ButtonUIC(buildZone.Find(buildT + "+").Find("Button+").GetComponent<Button>()));
            }
        }
    }
}