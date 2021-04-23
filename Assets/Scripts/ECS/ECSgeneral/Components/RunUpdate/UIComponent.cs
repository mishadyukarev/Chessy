using System;

public struct UIComponent
{
    private bool _isDone;


    internal bool IsDone
    {
        get { return _isDone; }
        set { _isDone = value; }
    }

    internal Action<bool, bool> DonerDelegate;
    internal Action<UnitTypes, bool, bool> Button1Delegate;
}
