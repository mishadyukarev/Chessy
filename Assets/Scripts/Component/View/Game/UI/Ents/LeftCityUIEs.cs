using ECS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct LeftCityUIEs
    {
        readonly Dictionary<BuildingTypes, LeftCityBuildUIE> _ents;

        public LeftCityBuildUIE BuildE(in BuildingTypes buildT) => _ents[buildT];

        public LeftCityUIEs(in EcsWorld gameW, in Transform leftZone)
        {
            _ents = new Dictionary<BuildingTypes, LeftCityBuildUIE>();

            var buildZone = leftZone.transform.Find("City+");

            for (var buildT = BuildingTypes.House; buildT <= BuildingTypes.Smelter; buildT++)
            {
                _ents.Add(buildT, new LeftCityBuildUIE(buildZone.Find("Build" + buildT + "+").Find("Button+").GetComponent<Button>(), gameW));
            }
        }
    }
}