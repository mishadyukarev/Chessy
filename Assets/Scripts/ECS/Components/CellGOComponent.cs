using UnityEngine;

namespace Assets.Scripts.ECS.Components
{
    internal struct CellGOComponent
    {
        private GameObject _cellGO;

        internal GameObject CellGO => _cellGO;

        internal CellGOComponent(GameObject cellGO) => _cellGO = cellGO;
    }
}
