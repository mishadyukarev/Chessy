using Leopotam.Ecs;
using UnityEngine;

public class InputSystem : IEcsRunSystem
{
    private EcsComponentRef<InputComponent> _inputComponentRef = default;

    internal InputSystem(ECSmanager eCSmanager)
    {
        _inputComponentRef = eCSmanager.EntitiesGeneralManager.InputComponentRef;
    }

    public void Run()
    {
        if (Input.GetMouseButtonDown(0)) _inputComponentRef.Unref().IsClick =true;
        else _inputComponentRef.Unref().IsClick = false;
    }
}