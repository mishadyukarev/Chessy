
using System;
using System.Collections.Generic;

public struct SelectorComponent : IDisposable
{
    internal List<int[]> AvailableCellsForShift;
    internal List<int[]> AvailableCellsSimpleAttack;
    internal List<int[]> AvailableCellsUniqueAttack;

    internal int[] XyCurrentCell;

    internal int[] XyPreviousCell;
    internal int[] XySelectedCell;

    internal bool IsSelected;


    internal bool IsGettedCell;

    internal Action SetterUnitDelegate;
    internal Action AttackUnitAction;
    internal Action ShiftUnitDelegate;

    internal SelectorComponent(int xy)
    {
        XyCurrentCell = new int[xy];
        XySelectedCell = new int[xy];
        XyPreviousCell = new int[xy];

        IsGettedCell = default;

        IsSelected = default;

        SetterUnitDelegate = default;
        AttackUnitAction = default;
        ShiftUnitDelegate = default;

        AvailableCellsForShift = new List<int[]>();
        AvailableCellsSimpleAttack = new List<int[]>();
        AvailableCellsUniqueAttack = new List<int[]>();
    }

    public void Dispose()
    {
        XySelectedCell[0] = default;
        XySelectedCell[1] = default;
        IsSelected = default;
    }
}
