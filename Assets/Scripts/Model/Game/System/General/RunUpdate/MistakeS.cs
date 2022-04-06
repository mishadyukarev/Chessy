using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class MistakeS : SystemModel
    {
        const float NEED_TIME_FOR_FADING = 1.3f;

        internal MistakeS(in SystemsModelCommon sMC, in EntitiesModelCommon eMC, in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMC, eMC, sMG, eMG)
        {
        }

        internal void Update()
        {
            if (eMG.MistakeT != MistakeTypes.None)
            {
                eMG.MistakeTimerC.Timer += Time.deltaTime;

                if (eMG.MistakeTimer >= NEED_TIME_FOR_FADING)
                {
                    eMG.MistakeTC.MistakeT = MistakeTypes.None;
                    eMG.NeedUpdateView = true;
                }
            }

            if(eMG.MotionTimer > 0)
            {
                eMG.MotionTimer -= Time.deltaTime;

                if (eMG.MotionTimer <= 0) eMG.NeedUpdateView = true;
            }
        }
    }
}