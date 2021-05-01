internal struct AnimationAttackUnitComponent
{
    private int[] _xyStartCell;
    private int[] _xyEndCell;

    internal int[] XYStartCell
    {
        get { return _xyStartCell; }
        set { _xyStartCell = value; }
    }
    internal int[] XYEndCell
    {
        get { return _xyEndCell; }
        set { _xyEndCell = value; }
    }
}
