
using System;

public struct SelectorComponent
{
    private int[] _xySelectedCell;

    private Action<bool> _setterUnitDelegate;
    private Action<bool> _attackUnitDelegate;
    private Action<bool> _shiftUnitDelegate;


    internal int[] XYselectedCell { get { return _xySelectedCell; } set { _xySelectedCell = value; } }

    internal Action<bool> SetterUnitDelegate
    {
        get { return _setterUnitDelegate; }
        set { if (_setterUnitDelegate == default) _setterUnitDelegate = value; }
    }
    internal Action<bool> AttackUnitDelegate
    {
        get { return _attackUnitDelegate; }
        set { if (_attackUnitDelegate == default) _attackUnitDelegate = value; }
    }
    internal Action<bool> ShiftUnitDelegate
    {
        get { return _shiftUnitDelegate; }
        set { if (_shiftUnitDelegate == default) _shiftUnitDelegate = value; }
    }

    internal SelectorComponent(StartValuesConfig nameValueManager)
    {
        _xySelectedCell = new int[nameValueManager.XYforArray];

        _setterUnitDelegate = default;
        _attackUnitDelegate = default;
        _shiftUnitDelegate = default;
    }
}
