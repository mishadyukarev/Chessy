using Chessy.Game.Entity.Model;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class InputS : SystemModelGameAbs, IEcsRunSystem
    {
        internal InputS(in EntitiesModelGame eMGame) : base(eMGame)
        {
        }

        public void Run()
        {
            e.IsClicked = Input.GetMouseButtonDown(0);
        }
    }
}