using System;

public struct DonerComponent
{
    private bool _isDone;
    private bool _isMistaked;

    internal bool IsDone
    {
        get { return _isDone; }
        set { _isDone = value; }
    }
    internal bool IsMistaked
    {
        get { return _isMistaked; }
        set { _isMistaked = value; }
    }
}
