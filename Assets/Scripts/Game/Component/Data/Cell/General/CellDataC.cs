using UnityEngine;

namespace Scripts.Game
{
    public readonly struct CellDataC
    {
        public readonly bool IsActiveCell;
        public readonly int InstanceID;

        public CellDataC(GameObject cell_GO)
        {
            IsActiveCell = cell_GO.transform.parent.gameObject.activeSelf;
            InstanceID = cell_GO.GetInstanceID();
        }
    }
}