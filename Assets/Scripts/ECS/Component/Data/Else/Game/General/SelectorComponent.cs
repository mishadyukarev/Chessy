using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using UnityEngine;

public struct SelectorComponent
{
    internal RaycastHit2D RaycastHit2D { get; set; }
    internal RaycastGettedTypes RaycastGettedType { get; set; }

    internal CellClickTypes CellClickType { get; set; }
    internal bool IsCellClickType(CellClickTypes cellClickType) => cellClickType == CellClickType;


    internal UnitTypes SelectedUnitType { get; set; }
    internal bool IsSelectedUnit => SelectedUnitType != default;
    internal void ResetSelectedUnit() => SelectedUnitType = default;


    internal PawnExtraToolTypes PawnToolTypeForUpgrade { get; set; }


    internal bool IsActPickingFire { get; set; }

    internal bool CanShiftUnit { get; set; }
    internal bool IsStartSelectedDirect { get; set; }


    internal byte IdxCurrentCell { get; set; }
    internal byte IdxSelectedCell { get; set; }
    internal byte IdxPreviousCell { get; set; }
    internal byte IdxPreviousVisionCell { get; set; }

    internal bool IsSelectedCell => IdxSelectedCell != 0;
    internal void ResetSelectedCell() => IdxSelectedCell = 0;

    internal SelectorComponent(CellClickTypes selectorType) : this()
    {
        CellClickType = selectorType;
    }
}
