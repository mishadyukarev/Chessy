using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Common
{
    public sealed class SpritesResources
    {
        private static Dictionary<SpriteTypes, Sprite> _sprites;


        internal SpritesResources()
        {
            _sprites = new Dictionary<SpriteTypes, Sprite>();

            var sectionName = "Sprites/";

            _sprites.Add(SpriteTypes.BlackCell, Resources.Load<Sprite>(sectionName + "Black_Spr"));
            _sprites.Add(SpriteTypes.WhiteCell, Resources.Load<Sprite>(sectionName + "White_Spr"));
        }

        public static Sprite Sprite(SpriteTypes spriteType) => _sprites[spriteType];
    }
}