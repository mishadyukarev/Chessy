using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class MistakeS : SystemModel
    {
        const float NEED_TIME_FOR_FADING = 1.3f;

        internal MistakeS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Update()
        {
            if (_eMG.MistakeT != MistakeTypes.None)
            {
                _eMG.MistakeTimerC.Timer += Time.deltaTime;

                if (_eMG.MistakeTimer >= NEED_TIME_FOR_FADING)
                {
                    _eMG.MistakeTC.MistakeT = MistakeTypes.None;
                    _eMG.NeedUpdateView = true;
                }
            }

            if (_eMG.MotionTimer > 0)
            {
                _eMG.MotionTimer -= Time.deltaTime;

                if (_eMG.MotionTimer <= 0) _eMG.NeedUpdateView = true;
            }
        }
    }
}