using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct LeftCityUIEs
    {
        readonly Dictionary<BuildingTypes, LeftCityBuildUIE> _ents;
        public LeftCityBuildUIE BuildE(in BuildingTypes buildT) => _ents[buildT];

        public readonly GameObjectVC Zone;

        internal LeftCityUIEs(in Transform leftZone)
        {
            _ents = new Dictionary<BuildingTypes, LeftCityBuildUIE>();


            var buildZone = leftZone.transform.Find("City+");

            Zone = new GameObjectVC(buildZone.gameObject);

            for (var buildT = BuildingTypes.House; buildT <= BuildingTypes.Smelter; buildT++)
            {
                _ents.Add(buildT, new LeftCityBuildUIE(buildZone.Find("Build" + buildT + "+").Find("Button+").GetComponent<Button>()));
            }
        }
    }
}