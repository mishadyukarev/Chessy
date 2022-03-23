using UnityEngine;

namespace Chessy.Game.Entity.View.Cell
{
    public readonly struct FireVE
    {
        public readonly SpriteRendererVC SR;

        public FireVE(in GameObject cell)
        {
            SR = new SpriteRendererVC(cell.transform.Find("Fire").GetComponent<SpriteRenderer>());
        }
    }
}