using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Component.View.Else.Game.General.Cell
{
    internal struct CellViewComponent
    {
        private GameObject _cell_GO;

        internal int InstanceID => _cell_GO.GetInstanceID();
        internal bool IsActiveParent => _cell_GO.transform.parent.gameObject.activeSelf;

        internal CellViewComponent(GameObject cellView_GO) => _cell_GO = cellView_GO;

        internal void SetRotForClient(bool isMaster) => _cell_GO.transform.rotation = isMaster ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);
    }
}
