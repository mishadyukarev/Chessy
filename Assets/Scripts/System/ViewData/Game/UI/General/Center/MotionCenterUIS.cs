﻿using UnityEngine;
using static Game.Game.EntityCenterUIPool;

namespace Game.Game
{
    sealed class MotionCenterUIS : SystemUIAbstract, IEcsRunSystem
    {
        float _timer;

        internal MotionCenterUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            if (Es.MotionIsActiveC.IsActive)
            {
                Motion<MotionsUIEC>().Text = Es.MotionsC.Amount.ToString();
                Motion<MotionsUIEC>().SetActiveParent(true);

                _timer += Time.deltaTime;

                if (_timer >= 1)
                {
                    Motion<MotionsUIEC>().SetActiveParent(false);
                    Es.MotionIsActiveC.IsActive = false;
                    _timer = 0;
                }
            }
            else
            {
                Motion<MotionsUIEC>().SetActiveParent(false);
            }
        }
    }
}