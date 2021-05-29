using UnityEngine;

internal sealed class InputSystem : SystemGeneralReduction
{
    internal InputSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();

        if (Input.GetMouseButtonDown(0)) _eGM.InputEntityMouseClickComponent.IsClick = true;
        else _eGM.InputEntityMouseClickComponent.IsClick = false;
    }
}