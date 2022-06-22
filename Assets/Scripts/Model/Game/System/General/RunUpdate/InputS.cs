using Chessy.Game.Model.Entity;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    sealed class InputS : SystemModel
    {
        internal InputS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG) { }

        internal void Update()
        {
            _e.IsClicked = Input.GetMouseButtonDown(0);

            if (_e.IsClicked)
            {
                Screen.fullScreen = true;
            }
        }
    }
}