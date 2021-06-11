
using System;
using System.Collections.Generic;

public struct SelectorComponent
{
    internal List<int[]> AvailableCellsForShift;
    internal List<int[]> AvailableCellsSimpleAttack;
    internal List<int[]> AvailableCellsUniqueAttack;

    internal int[] XYcurrentCell;

    internal int[] XYpreviousCell;
    internal int[] XySelectedCell;

    internal bool IsSelected;


    internal bool IsGettedCell;

    internal Action SetterUnitDelegate;
    internal Action AttackUnitAction;
    internal Action ShiftUnitDelegate;

    internal SelectorComponent(int xy)
    {
        XYcurrentCell = new int[xy];
        XySelectedCell = new int[xy];
        XYpreviousCell = new int[xy];

        IsGettedCell = default;

        IsSelected = default;

        SetterUnitDelegate = default;
        AttackUnitAction = default;
        ShiftUnitDelegate = default;

        AvailableCellsForShift = new List<int[]>();
        AvailableCellsSimpleAttack = new List<int[]>();
        AvailableCellsUniqueAttack = new List<int[]>();
    }
}
