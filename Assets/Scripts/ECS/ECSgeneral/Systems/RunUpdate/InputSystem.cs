using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : SystemGeneralReduction, IEcsRunSystem
{
    internal InputSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public void Run()
    {
        if (Input.GetMouseButtonDown(0)) _eGM.InputEntityMouseClickComponent.IsClick = true;
        else _eGM.InputEntityMouseClickComponent.IsClick = false;
    }
}