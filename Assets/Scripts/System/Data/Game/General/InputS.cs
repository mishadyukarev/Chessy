using UnityEngine;

namespace Game.Game
{
    struct InputS : IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetMouseButtonDown(0)) Entities.InputE.IsClickedC.IsClicked = true;
            else Entities.InputE.IsClickedC.IsClicked = false;
        }
    }
}