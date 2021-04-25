using UnityEngine.UI;

internal struct SelectorUnitComponent
{
    private Button _button0;
    private Button _button1;

    internal Button Button0
    {
        get { return _button0; }
        set { _button0 = value; }
    }
    internal Button Button1
    {
        get { return _button1; }
        set { _button1 = value; }
    }
}
