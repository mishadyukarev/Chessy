using Leopotam.Ecs;
using UnityEngine;

internal sealed class InputSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _gameWorld;

    private static EcsEntity _inputEnt;
    internal static bool IsClick
    {
        get => _inputEnt.Get<InputComponent>().IsClick;
        set => _inputEnt.Get<InputComponent>().IsClick = value;
    }

    public void Init()
    {
        _inputEnt = _gameWorld.NewEntity()
            .Replace(new InputComponent());
    }

    public void Run()
    {
        if (Input.GetMouseButtonDown(0)) IsClick = true;
        else IsClick = false;
    }
}