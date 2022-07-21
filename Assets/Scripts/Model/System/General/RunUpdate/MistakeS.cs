using Chessy.Model.Entity;
using Chessy.Model.System;
using UnityEngine;
namespace Chessy.Model
{
    sealed class MistakeS : SystemModelAbstract
    {
        const float NEED_TIME_FOR_FADING = 1.3f;

        internal MistakeS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
        {
        }

        internal void Update()
        {
            if (_mistakeC.MistakeT != MistakeTypes.None)
            {
                _mistakeC.Timer += Time.deltaTime;

                if (_mistakeC.Timer >= NEED_TIME_FOR_FADING)
                {
                    _mistakeC.MistakeT = MistakeTypes.None;
                    _updateAllViewC.NeedUpdateView = true;
                }
            }

            //if (_e.MotionTimer > 0)
            //{
            //    _e.MotionTimer -= Time.deltaTime;

            //    if (_e.MotionTimer <= 0) _updateAllViewC.NeedUpdateView = true;
            //}
        }
    }
}