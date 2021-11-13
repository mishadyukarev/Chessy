using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game
{
    public struct BlocksVC
    {
        private Dictionary<CellBlockTypes, SpriteRenderer> _block_SRs;

        public BlocksVC(GameObject cell)
        {
            _block_SRs = new Dictionary<CellBlockTypes, SpriteRenderer>();

            _block_SRs.Add(CellBlockTypes.Condition, cell.transform.Find("ProtectRelax").GetComponent<SpriteRenderer>());
            _block_SRs.Add(CellBlockTypes.MaxSteps, cell.transform.Find("MaxSteps").GetComponent<SpriteRenderer>());
            _block_SRs.Add(CellBlockTypes.NeedWater, cell.transform.Find("NeedWater_SR").GetComponent<SpriteRenderer>());
        }

        public void EnableBlockSR(CellBlockTypes cellBlockType) => _block_SRs[cellBlockType].enabled = true;
        public void DisableBlockSR(CellBlockTypes cellBlockType) => _block_SRs[cellBlockType].enabled = false;

        public void SetColor(CellBlockTypes cellBlockType, Color color) => _block_SRs[cellBlockType].color = color;
    }
}
