using Leopotam.Ecs;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class InputSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButtonDown(0)) InputC.IsClicked = true;
            else InputC.IsClicked = false;
        }
    }
}