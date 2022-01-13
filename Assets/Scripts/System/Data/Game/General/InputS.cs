using UnityEngine;

namespace Game.Game
{
    struct InputS : IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButtonDown(0)) EntityPool.Input<IsClickedC>().IsClicked = true;
            else EntityPool.Input<IsClickedC>().IsClicked = false;
        }
    }
}