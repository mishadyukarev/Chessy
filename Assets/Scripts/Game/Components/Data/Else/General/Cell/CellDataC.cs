using UnityEditor;
using UnityEngine;

namespace Scripts.Game
{
    public readonly struct CellDataC
    {
        public readonly bool IsActiveCell;

        public CellDataC(bool isActiveCell) => IsActiveCell = isActiveCell;
    }
}