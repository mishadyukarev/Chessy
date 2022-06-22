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
            if (_e.MistakeT != MistakeTypes.None)
            {
                _e.MistakeTimer += Time.deltaTime;

                if (_e.MistakeTimer >= NEED_TIME_FOR_FADING)
                {
                    _e.MistakeT = MistakeTypes.None;
                    _e.NeedUpdateView = true;
                }
            }

            if (_e.MotionTimer > 0)
            {
                _e.MotionTimer -= Time.deltaTime;

                if (_e.MotionTimer <= 0) _e.NeedUpdateView = true;
            }
        }
    }
}