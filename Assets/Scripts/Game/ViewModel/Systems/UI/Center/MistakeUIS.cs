using System;
using UnityEngine;

namespace Chessy.Game
{
    static class MistakeUIS
    {
        const float NEED_TIME_FOR_FADING = 1.3f;

        public static void Sync(this MistakeUIE mistakeUIE, in float timer, in EntitiesModel e)
        {
            foreach (var key in mistakeUIE.KeysMistake)
            {
                mistakeUIE.Zones(key).SetActive(false);
            }

            foreach (var key in mistakeUIE.KeysResource)
            {
                mistakeUIE.NeedAmountResources(key).SetActive(false);
            }



            if (e.MistakeC.MistakeT != MistakeTypes.None)
            {
                e.MistakeC.Timer += Time.deltaTime + timer;

                if (e.MistakeC.MistakeT == MistakeTypes.Economy)
                {
                    if (e.MistakeC.Timer >= NEED_TIME_FOR_FADING)
                    {
                        e.MistakeC.MistakeT = MistakeTypes.None;
                    }

                    else
                    {
                        mistakeUIE.Zones(e.MistakeC.MistakeT).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (e.MistakeEconomy(res).Resources > 0)
                            {
                                mistakeUIE.NeedAmountResources(res).SetActive(true);

                                mistakeUIE.NeedAmountResources(res).TextUI.text
                                    = ">= " + ((int)(100 * e.MistakeEconomy(res).Resources));
                            }
                        }
                    }
                }

                else
                {
                    mistakeUIE.Zones(e.MistakeC.MistakeT).SetActive(true);

                    if (e.MistakeC.Timer >= NEED_TIME_FOR_FADING)
                    {
                        e.MistakeC.MistakeT = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
