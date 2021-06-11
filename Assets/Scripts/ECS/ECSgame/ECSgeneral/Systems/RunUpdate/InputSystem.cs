using UnityEngine;

internal sealed class InputSystem : SystemGeneralReduction
{

    public override void Run()
    {
        base.Run();

        if (Input.GetMouseButtonDown(0)) _eGM.InputEnt_InputCom.IsClick = true;
        else _eGM.InputEnt_InputCom.IsClick = false;
    }
}