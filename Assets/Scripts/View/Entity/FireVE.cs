﻿using UnityEngine;

namespace Chessy.Model.Entity.View.Cell
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