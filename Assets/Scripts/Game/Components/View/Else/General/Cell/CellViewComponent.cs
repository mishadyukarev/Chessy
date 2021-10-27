using System;
using UnityEngine;

namespace Scripts.Game
{
    internal struct CellViewComponent
    {
        private GameObject _cell_GO;

        internal int InstanceID => _cell_GO.GetInstanceID();
        internal bool IsActiveParent => _cell_GO.transform.parent.gameObject.activeSelf;

        internal CellViewComponent(GameObject cellView_GO) => _cell_GO = cellView_GO;

        internal void SetRotForClient(PlayerTypes playerType)
        {
            if (playerType == PlayerTypes.None) throw new Exception();
            _cell_GO.transform.parent.rotation = playerType == PlayerTypes.First ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
        }
    }
}
