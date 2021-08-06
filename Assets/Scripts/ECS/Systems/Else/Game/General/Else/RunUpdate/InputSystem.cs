using Leopotam.Ecs;
using UnityEngine;

internal sealed class InputSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> _inputFilter = default;

    public void Run()
    {
        if (Input.GetMouseButtonDown(0)) _inputFilter.Get1(0).IsClicked = true;
        else _inputFilter.Get1(0).IsClicked = false;
    }
}