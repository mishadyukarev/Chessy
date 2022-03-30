using Chessy.Game.Entity.Model;
using Chessy.Game.System.Model;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class InputS : SystemModelGameAbs, IEcsRunSystem
    {
        internal InputS(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(sMGame, eMGame) { }

        public void Run()
        {
            e.IsClicked = Input.GetMouseButtonDown(0);
        }
    }
}