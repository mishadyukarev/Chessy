using Chessy.Game.Entity.Model;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    public class InputS : SystemModelGameAbs, IEcsRunSystem
    {
        public InputS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Run()
        {
            eMGame.IsClicked = Input.GetMouseButtonDown(0);
        }
    }
}