using UnityEngine;

namespace Game.Game
{
    public readonly struct SpriteVC
    {
        public readonly Sprite Sprite;

        public SpriteVC(in Sprite sprite) => Sprite = sprite;

    }
}