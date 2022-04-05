﻿using Chessy.Game.Model.Entity;
using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class MistakeUIS : SystemUIAbstract
    {
        const float NEED_TIME_FOR_FADING = 1.3f;
        readonly MistakeUIE _mistakeUIE;


        internal MistakeUIS(in MistakeUIE mistakeUIE, in EntitiesModelGame eMG) : base(eMG)
        {
            _mistakeUIE = mistakeUIE;
        }

        public void Sync(in float timer)
        {
            foreach (var key in _mistakeUIE.KeysMistake)
            {
                _mistakeUIE.Zones(key).SetActive(false);
            }

            foreach (var key in _mistakeUIE.KeysResource)
            {
                _mistakeUIE.NeedAmountResources(key).SetActive(false);
            }



            if (e.MistakeT != MistakeTypes.None)
            {
                e.MistakeTimerC.Timer += Time.deltaTime + timer;

                if (e.MistakeT == MistakeTypes.Economy)
                {
                    if (e.MistakeTimer >= NEED_TIME_FOR_FADING)
                    {
                        e.MistakeTC.MistakeT = MistakeTypes.None;
                    }

                    else
                    {
                        _mistakeUIE.Zones(e.MistakeT).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (e.MistakeEconomy(res).Resources > 0)
                            {
                                _mistakeUIE.NeedAmountResources(res).SetActive(true);

                                _mistakeUIE.NeedAmountResources(res).TextUI.text
                                    = res == ResourceTypes.Iron || res == ResourceTypes.Gold ? ">= " + e.MistakeEconomy(res).Resources : ">= " + ((int)(100 * e.MistakeEconomy(res).Resources));
                            }
                        }
                    }
                }

                else
                {
                    _mistakeUIE.Zones(e.MistakeT).SetActive(true);

                    if (e.MistakeTimer >= NEED_TIME_FOR_FADING)
                    {
                        e.MistakeTC.MistakeT = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
