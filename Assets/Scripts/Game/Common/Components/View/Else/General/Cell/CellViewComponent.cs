using System;
using UnityEngine;

namespace Scripts.Game
{
    public struct CellViewComponent
    {
        private GameObject _cell_GO;

        public int InstanceID => _cell_GO.GetInstanceID();
        public bool IsActiveParent => _cell_GO.transform.parent.gameObject.activeSelf;

        public CellViewComponent(GameObject cellView_GO) => _cell_GO = cellView_GO;

        public void SetRotForClient(PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.None) throw new Exception();
            _cell_GO.transform.parent.rotation = playerType == PlayerTypes.First ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
        }
    }
}
