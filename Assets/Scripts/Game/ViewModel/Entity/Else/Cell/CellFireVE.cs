using UnityEngine;

namespace Chessy.Game
{
    public struct CellFireVE
    {
        public SpriteRendererVC SR;

        public CellFireVE(in GameObject cell)
        {
            SR = new SpriteRendererVC(cell.transform.Find("Fire").GetComponent<SpriteRenderer>());
        }
    }
}