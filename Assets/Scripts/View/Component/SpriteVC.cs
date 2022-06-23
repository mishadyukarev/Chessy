using UnityEngine;

namespace Chessy.Model
{
    public struct SpriteVC
    {
        public Sprite Sprite;

        public SpriteVC(in Sprite sprite) => Sprite = sprite;
    }
}