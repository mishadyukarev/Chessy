
using System;
using System.Collections.Generic;

public struct SelectorComponent
{
    private int[] _xySelectedCell;
    private int[] _xyPreviousCell;

    private Action _setterUnitDelegate;
    private Action _attackUnitDelegate;
    private Action _shiftUnitDelegate;



    internal List<int[]> XYavailableCellsForShift;
    internal List<int[]> XYavailableCellsForAttack;

    internal int[] XYpreviousCell
    {
        get { return _xyPreviousCell; }
        set { _xyPreviousCell = value; }
    }
    internal int[] XYselectedCell
    {
        get { return _xySelectedCell; }
        set { _xySelectedCell = value; }
    }

    internal Action SetterUnitDelegate
    {
        get { return _setterUnitDelegate; }
        set { if (_setterUnitDelegate == default) _setterUnitDelegate = value; }
    }
    internal Action AttackUnitDelegate
    {
        get { return _attackUnitDelegate; }
        set { if (_attackUnitDelegate == default) _attackUnitDelegate = value; }
    }
    internal Action ShiftUnitDelegate
    {
        get { return _shiftUnitDelegate; }
        set { if (_shiftUnitDelegate == default) _shiftUnitDelegate = value; }
    }

    internal SelectorComponent(StartValuesGameConfig nameValueManager)
    {
        _xySelectedCell = new int[nameValueManager.XY_FOR_ARRAY];
        _xyPreviousCell = new int[nameValueManager.XY_FOR_ARRAY];

        _setterUnitDelegate = default;
        _attackUnitDelegate = default;
        _shiftUnitDelegate = default;

        XYavailableCellsForAttack = new List<int[]>();
        XYavailableCellsForShift = new List<int[]>();
    }
}
