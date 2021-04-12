using Leopotam.Ecs;
using UnityEngine;

public class InputWindowsSystem : IEcsRunSystem
{
    private EcsComponentRef<InputComponent> _inputComponentRef = default;

    internal InputWindowsSystem(ECSmanager eCSmanager)
    {
        _inputComponentRef = eCSmanager.EntitiesGeneralManager.InputComponentRef;
    }

    public void Run()
    {
        if (Input.GetMouseButtonDown(0)) _inputComponentRef.Unref().SetClick(true);
        else _inputComponentRef.Unref().SetClick(false);

        if (Input.GetKeyDown(KeyCode.Z)) _inputComponentRef.Unref().SetClickBackMove(true);
        else _inputComponentRef.Unref().SetClickBackMove(false);
    }
}