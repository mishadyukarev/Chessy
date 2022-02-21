using ECS;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct Resources
    {
        readonly Dictionary<AbilityTypes, SpriteVC> _abilities;

        public SpriteVC Sprite(in AbilityTypes ability) => _abilities[ability];


        public Resources(in EcsWorld gameW)
        {
            var spriteName = "_Sprite";

            var folder = "Unique/";
            _abilities = new Dictionary<AbilityTypes, SpriteVC>();

            for (var unique = AbilityTypes.None + 1; unique < AbilityTypes.End; unique++)
            {
                _abilities.Add(unique, new SpriteVC(UnityEngine.Resources.Load<Sprite>(folder + unique + spriteName)));
            }
        }
    }
}