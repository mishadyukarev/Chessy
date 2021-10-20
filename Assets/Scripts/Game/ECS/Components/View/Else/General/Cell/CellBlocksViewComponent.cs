using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    internal struct CellBlocksViewComponent
    {
        private Dictionary<CellBlockTypes, SpriteRenderer> _block_SRs;

        internal CellBlocksViewComponent(GameObject cell)
        {
            _block_SRs = new Dictionary<CellBlockTypes, SpriteRenderer>();

            _block_SRs.Add(CellBlockTypes.Condition, cell.transform.Find("ProtectRelax").GetComponent<SpriteRenderer>());
            _block_SRs.Add(CellBlockTypes.MaxSteps, cell.transform.Find("MaxSteps").GetComponent<SpriteRenderer>());
        }

        internal void EnableBlockSR(CellBlockTypes cellBlockType) => _block_SRs[cellBlockType].enabled = true;
        internal void DisableBlockSR(CellBlockTypes cellBlockType) => _block_SRs[cellBlockType].enabled = false;

        internal void SetColor(CellBlockTypes cellBlockType, Color color) => _block_SRs[cellBlockType].color = color;
    }
}
