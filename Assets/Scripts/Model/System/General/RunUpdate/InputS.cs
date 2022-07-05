using Chessy.Model.Entity;
using Chessy.Model.System;
using UnityEngine;
namespace Chessy.Model
{
    sealed class InputS : SystemModelAbstract
    {
        internal InputS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG) { }

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