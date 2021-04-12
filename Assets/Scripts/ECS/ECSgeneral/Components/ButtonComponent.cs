using System;
using UnityEngine.UI;

public struct ButtonComponent
{
    private bool _isDone;


    internal bool IsDone { get { return _isDone; } set { _isDone = value; } }

    internal Action<bool, bool> DonerDelegate;
    internal Action<bool, bool> Button1Delegate;
}
