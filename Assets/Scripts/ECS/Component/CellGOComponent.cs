using UnityEngine;

namespace Assets.Scripts.ECS.Components
{
    internal struct CellGOComponent
    {
        internal GameObject CellGO { get; private set; }

        internal CellGOComponent(GameObject cellGO) => CellGO = cellGO;
    }
}
