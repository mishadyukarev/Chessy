using Assets.Scripts.Workers;
using Leopotam.Ecs;
using UnityEngine;

internal sealed class InputSystem : IEcsRunSystem
{
    public void Run()
    {
        if (Input.GetMouseButtonDown(0)) SelectorWorker.IsClick = true;
        else SelectorWorker.IsClick = false;
    }
}