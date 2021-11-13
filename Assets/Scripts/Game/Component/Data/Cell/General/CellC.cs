using UnityEngine;

namespace Chessy.Game
{
    public readonly struct CellC
    {
        public readonly bool IsActiveCell;
        public readonly int InstanceID;

        public CellC(GameObject cell_GO)
        {
            IsActiveCell = cell_GO.transform.parent.gameObject.activeSelf;
            InstanceID = cell_GO.GetInstanceID();
        }
    }
}