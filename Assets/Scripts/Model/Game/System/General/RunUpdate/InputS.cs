using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class InputS : SystemModel
    {
        internal InputS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG) { }

        internal void Update()
        {
            eMG.IsClicked = Input.GetMouseButtonDown(0);

            if (eMG.IsClicked)
            {
                Screen.fullScreen = true;
            }
        }
    }
}