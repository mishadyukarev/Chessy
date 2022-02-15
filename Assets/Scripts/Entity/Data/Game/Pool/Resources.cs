using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct Resources
    {
        readonly Dictionary<AbilityTypes, Sprite> _abilities;

        public Sprite Sprite(in AbilityTypes ability) => _abilities[ability];


        public Resources(in EcsWorld gameW)
        {
            var spriteName = "_Sprite";

            var folder = "Unique/";
            _abilities = new Dictionary<AbilityTypes, Sprite>();

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                _abilities.Add(unique, UnityEngine.Resources.Load<Sprite>(folder + unique + spriteName));
            }
        }
    }
}