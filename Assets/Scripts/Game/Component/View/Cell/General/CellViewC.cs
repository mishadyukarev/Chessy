using System;
using UnityEngine;

namespace Chessy.Game
{
    public struct CellViewC
    {
        private GameObject _cellParent_GO;
        private GameObject _cell_GO;

        public CellViewC(GameObject cellView_GO)
        {
            _cellParent_GO = cellView_GO.transform.parent.gameObject;
            _cell_GO = cellView_GO;
        }

        public void SetRotForClient(PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.None) throw new Exception();
            _cellParent_GO.transform.rotation = playerType == PlayerTypes.First ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
        }
    }
}
