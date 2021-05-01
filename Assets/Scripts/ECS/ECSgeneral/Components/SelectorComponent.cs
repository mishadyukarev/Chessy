
using System;
using System.Collections.Generic;

public struct SelectorComponent
{
    private int[] _xyCurrentCell;
    private int[] _xyPreviousCell;
    private int[] _xySelectedCell;

    private bool _isGettedCell;

    private Action _setterUnitDelegate;
    private Action _attackUnitDelegate;
    private Action _shiftUnitDelegate;



    internal List<int[]> XYavailableCellsForShift;
    internal List<int[]> XYavailableCellsForAttack;

    internal int[] XYcurrentCell
    {
        get { return _xyCurrentCell; }
        set { _xyCurrentCell = value; }
    }

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


    internal bool IsGettedCell
    {
        get { return _isGettedCell; }
        set { _isGettedCell = value; }
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
        _xyCurrentCell = new int[nameValueManager.XY_FOR_ARRAY];
        _xySelectedCell = new int[nameValueManager.XY_FOR_ARRAY];
        _xyPreviousCell = new int[nameValueManager.XY_FOR_ARRAY];

        _isGettedCell = default;

        _setterUnitDelegate = default;
        _attackUnitDelegate = default;
        _shiftUnitDelegate = default;

        XYavailableCellsForAttack = new List<int[]>();
        XYavailableCellsForShift = new List<int[]>();
    }
}
