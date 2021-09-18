using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using UnityEngine;

public struct SelectorCom
{
    internal RaycastHit2D RaycastHit2D { get; set; }
    internal RaycastGettedTypes RaycastGettedType { get; set; }

    internal CellClickTypes CellClickType { get; set; }
    internal bool IsCellClickType(CellClickTypes cellClickType) => cellClickType == CellClickType;
    internal void DefCellClickType() => CellClickType = default;


    internal UnitTypes SelUnitType { get; set; }
    internal bool IsSelectedUnit => SelUnitType != default;
    internal void DefSelectedUnit() => SelUnitType = default;

    internal ToolWeaponTypes ToolWeaponTypeForGiveTake { get; set; }



    internal byte IdxCurCell { get; set; }
    internal byte IdxSelCell { get; set; }
    internal byte IdxPreCell { get; set; }
    internal byte IdxPreviousVisionCell { get; set; }

    internal bool IsSelectedCell => IdxSelCell != 0;
    internal bool IsStartDirectToCell => IdxCurCell == default;
    internal void DefSelectedCell() => IdxSelCell = 0;


    internal SelectorCom(ToolWeaponTypes toolAndWeaponType) : this() => ToolWeaponTypeForGiveTake = toolAndWeaponType;
}
