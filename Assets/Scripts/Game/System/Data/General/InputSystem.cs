using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class InputSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButtonDown(0)) InputC.IsClicked = true;
            else InputC.IsClicked = false;
        }
    }
}