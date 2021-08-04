using Assets.Scripts.Abstractions.Enums;
using UnityEngine;

public struct SelectorComponent
{
    internal RaycastHit2D RaycastHit2D { get; set; }
    internal RaycastGettedTypes RaycastGettedType { get; set; }

    internal SelectorTypes SelectorType { get; set; }


    internal UnitTypes SelectedUnitType { get; set; }
    internal bool HaveAnySelectorUnit => SelectedUnitType != default;


    internal bool IsSelectedCell { get; set; }

    internal bool IsActPickingFire { get; set; }

    internal bool CanShiftUnit { get; set; }
    internal bool IsStartSelectedDirect { get; set; }


    private int[] _xyCurrentCell;
    internal int[] XyCurrentCell
    {
        get => (int[])_xyCurrentCell.Clone();
        set => _xyCurrentCell = (int[])value.Clone();
    }


    private int[] _xySelectedCell;
    internal int[] XySelectedCell
    {
        get => (int[])_xySelectedCell.Clone();
        set => _xySelectedCell = (int[])value.Clone();
    }


    private int[] _xyPreviousCell;
    internal int[] XyPreviousCell
    {
        get => (int[])_xyPreviousCell.Clone();
        set => _xyPreviousCell = (int[])value.Clone();
    }


    private int[] _xyPreviousVisionCell;
    internal int[] XyPreviousVisionCell
    {
        get => (int[])_xyPreviousVisionCell.Clone();
        set => _xyPreviousVisionCell = (int[])value.Clone();
    }


    internal SelectorComponent(int[] xy)
    {
        RaycastHit2D = default;
        RaycastGettedType = default;

        SelectorType = SelectorTypes.StartClick;

        SelectedUnitType = default;

        IsSelectedCell = default;
        IsActPickingFire = default;
        CanShiftUnit = default;
        IsStartSelectedDirect = true;

        _xyCurrentCell = (int[])xy.Clone();
        _xySelectedCell = (int[])xy.Clone();
        _xyPreviousCell = (int[])xy.Clone();
        _xyPreviousVisionCell = (int[])xy.Clone();
    }
}
