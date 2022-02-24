using ECS;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct CellBuildingVEs
    {
        readonly Dictionary<BuildingTypes, SpriteRendererVC> _ents;

        public SpriteRendererVC Main(in BuildingTypes buildT) => _ents[buildT];

        public CellBuildingVEs(in GameObject cell)
        {
            _ents = new Dictionary<BuildingTypes, SpriteRendererVC>();

            var build = cell.transform.Find("Building+");

            for (var buildT = BuildingTypes.None + 1; buildT < BuildingTypes.End; buildT++)
            {
                _ents.Add(buildT, new SpriteRendererVC(build.transform.Find(buildT.ToString() + "_SR+").GetComponent<SpriteRenderer>()));
            }
        }
    }
}