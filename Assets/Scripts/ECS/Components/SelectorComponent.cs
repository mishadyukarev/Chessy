using Assets.Scripts.Abstractions.Enums;
using System;
using static Assets.Scripts.Abstractions.ValuesConsts.CellValues;

public struct SelectorComponent
{
    internal bool IsSelected { get; set; }

    internal bool CanShiftUnit { get; set; }
    internal bool CanExecuteStartClick { get; set; }
    internal bool IsStartSelectedDirect { get; set; }


    internal void StartFill()
    {
        IsSelected = default;

        CanShiftUnit = default;
        CanExecuteStartClick = default;
        IsStartSelectedDirect = default;
    }
}
