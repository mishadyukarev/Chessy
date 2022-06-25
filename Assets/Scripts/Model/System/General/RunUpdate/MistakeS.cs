using Chessy.Model;
using Chessy.Model;
using UnityEngine;

namespace Chessy.Model
{
    sealed class MistakeS : SystemModel
    {
        const float NEED_TIME_FOR_FADING = 1.3f;

        internal MistakeS(in SystemsModel sMG, in EntitiesModel eMG) : base(sMG, eMG)
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