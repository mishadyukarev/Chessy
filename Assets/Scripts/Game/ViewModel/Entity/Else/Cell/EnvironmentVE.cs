using UnityEngine;

namespace Chessy.Game
{
    public readonly struct EnvironmentVE
    {
        public readonly SpriteRendererVC SR;

        internal EnvironmentVE(in SpriteRenderer sr)
        {
            SR = new SpriteRendererVC(sr);
        }
    }
}