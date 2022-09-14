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
            _inputC.IsClicked = Input.GetMouseButtonDown(0);


            

            ///Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);

            //Screen.fullScreen = true;
            //Screen.fullScreenMode = FullScreenMode.Windowed;

            //if (_inputC.IsClicked)
            //{
            //    //Screen.fullScreen = true;
            //}
        }
    }
}