using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct ResourceSpriteVEs
    {
        readonly Dictionary<AbilityTypes, ResourceSpriteVE> _abilities;

        public ResourceSpriteVE Sprite(in AbilityTypes ability) => _abilities[ability];


        public ResourceSpriteVEs(in EcsWorld gameW)
        {
            var spriteName = "_Sprite";

            var folder = "Unique/";
            _abilities = new Dictionary<AbilityTypes, ResourceSpriteVE>();

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                _abilities.Add(unique, new ResourceSpriteVE(gameW, folder + unique + spriteName));
            }
        }
    }
}