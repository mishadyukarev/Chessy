using UnityEngine;

namespace Chessy.Game
{
    public sealed class CellBuildingVE
    {
        public SpriteRendererVC SR;

        internal CellBuildingVE(in SpriteRenderer sr)
        {
            SR = new SpriteRendererVC(sr);
        }


    }
}