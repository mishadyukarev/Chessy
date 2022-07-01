using Chessy.View.Component;
using UnityEngine;

namespace Chessy.View.Entity
{
    public readonly struct FireVE
    {
        public readonly SpriteRendererVC SRC;

        public FireVE(in GameObject cell)
        {
            SRC = new SpriteRendererVC(cell.transform.Find("Fire").GetComponent<SpriteRenderer>());
        }
    }
}