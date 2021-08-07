using Assets.Scripts.Abstractions.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
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

        private void ActiveBlockSR(CellBlockTypes cellBlockType, bool isActive) => _block_SRs[cellBlockType].enabled = isActive;

        internal void EnableBlockSR(CellBlockTypes cellBlockType) => ActiveBlockSR(cellBlockType, true);
        internal void DisableBlockSR(CellBlockTypes cellBlockType) => ActiveBlockSR(cellBlockType, false);
    }
}
