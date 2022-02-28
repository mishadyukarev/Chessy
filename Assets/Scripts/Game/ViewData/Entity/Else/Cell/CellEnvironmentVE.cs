using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CellEnvironmentVE
    {
        readonly EnvironmentTypes EnvT;
        public readonly SpriteRendererVC SR;

        internal CellEnvironmentVE(in SpriteRenderer sr, in EnvironmentTypes envT, in byte idx)
        {
            EnvT = envT;
            SR = new SpriteRendererVC(sr);
        }
    }
}