using UnityEngine;

namespace Game.Game
{
    public struct SpriteVC
    {
        public Sprite Sprite;

        public SpriteVC(in Sprite sprite) => Sprite = sprite;
    }
}