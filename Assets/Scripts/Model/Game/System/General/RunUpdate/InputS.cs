using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class InputS : SystemModelGameAbs, IEcsRunSystem
    {
        internal InputS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        public void Run()
        {
            eMG.IsClicked = Input.GetMouseButtonDown(0);
        }
    }
}