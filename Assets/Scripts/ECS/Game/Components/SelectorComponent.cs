﻿
using Assets.Scripts.Abstractions.Enums;
using System;
using System.Collections.Generic;
using static Assets.Scripts.Abstractions.ValuesConst;

public struct SelectorComponent
{
    internal List<int[]> AvailableCellsForShift;
    internal List<int[]> AvailableCellsSimpleAttack;
    internal List<int[]> AvailableCellsUniqueAttack;

    internal int[] XyCurrentCell;

    internal int[] XyPreviousCell;
    internal int[] XySelectedCell;

    internal bool IsSelected;

    internal UpgradeModTypes UpgradeModType;
    internal bool PickedFire;

    internal bool IsGettedCell;

    internal Action SetterUnitDelegate;
    internal Action AttackUnitAction;
    internal Action ShiftUnitDelegate;

    internal void StartFill()
    {
        XyCurrentCell = new int[XY_FOR_ARRAY];
        XySelectedCell = new int[XY_FOR_ARRAY];
        XyPreviousCell = new int[XY_FOR_ARRAY];

        IsGettedCell = default;

        IsSelected = default;

        UpgradeModType = default;

        SetterUnitDelegate = default;
        AttackUnitAction = default;
        ShiftUnitDelegate = default;

        AvailableCellsForShift = new List<int[]>();
        AvailableCellsSimpleAttack = new List<int[]>();
        AvailableCellsUniqueAttack = new List<int[]>();
    }
}