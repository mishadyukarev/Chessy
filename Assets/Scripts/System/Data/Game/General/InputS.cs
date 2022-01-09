using UnityEngine;

namespace Game.Game
{
    sealed class InputS : IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButtonDown(0)) EntityPool.Input<ClickC>().IsClicked = true;
            else EntityPool.Input<ClickC>().IsClicked = false;
        }
    }
}