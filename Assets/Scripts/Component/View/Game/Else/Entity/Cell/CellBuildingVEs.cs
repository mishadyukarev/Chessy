using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellBuildingVEs
    {
        readonly Dictionary<BuildingTypes, CellBuildingVE> _ents;

        public CellBuildingVE Main(in BuildingTypes buildT) => _ents[buildT];

        public CellBuildingVEs(in GameObject cell, in EcsWorld gameW)
        {
            _ents = new Dictionary<BuildingTypes, CellBuildingVE>();

            var build = cell.transform.Find("Building");

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _ents.Add(buildT, new CellBuildingVE(build.transform.Find(buildT.ToString()).GetComponent<SpriteRenderer>(), gameW));
            }
        }
    }
}