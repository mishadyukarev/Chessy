﻿using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.Enums.Cell;
using Assets.Scripts.Abstractions.Enums.Cell.Pawn;
using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
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


    internal GiveTakeTypes GiveTakeType { get; set; }
    internal bool IsActivatedGiveTakeMod => GiveTakeType != default;

    internal UnitSlotTypes UnitSlotTypeForGiveTake { get; set; }
    internal ToolWeaponTypes ToolWeaponTypeForGiveTake { get; set; }


    internal byte IdxCurrentCell { get; set; }
    internal byte IdxSelectedCell { get; set; }
    internal byte IdxPreviousCell { get; set; }
    internal byte IdxPreviousVisionCell { get; set; }

    internal bool IsSelectedCell => IdxSelectedCell != 0;
    internal bool IsStartDirectToCell => IdxCurrentCell == default;
    internal void ResetSelectedCell() => IdxSelectedCell = 0;


    internal SelectorComponent(ToolWeaponTypes toolAndWeaponType) : this() => ToolWeaponTypeForGiveTake = toolAndWeaponType;
}
