using System;
using UnityEngine;

namespace Chessy.Game
{
    static class MistakeUIS
    {
        const float NEED_TIME_FOR_FADING = 1.3f;

        public static void Sync(this MistakeUIE mistakeUIE, in float timer, in EntitiesModel e)
        {
            mistakeUIE.Background.SetActive(false);
            mistakeUIE.TextUIC.SetActive(false);


            foreach (var key in mistakeUIE.KeysMistake)
            {
                mistakeUIE.Zones(key).SetActive(false);
            }

            foreach (var key in mistakeUIE.KeysResource)
            {
                mistakeUIE.NeedAmountResources(key).SetActive(false);
            }



            if (e.MistakeTC.HaveMistake)
            {
                e.MistakeTimerC.Timer += Time.deltaTime + timer;

                if (e.MistakeTC.Is(MistakeTypes.Economy))
                {
                    if (e.MistakeTimerC.Timer >= NEED_TIME_FOR_FADING)
                    {
                        e.MistakeTC.Mistake = MistakeTypes.None;
                    }

                    else
                    {
                        mistakeUIE.Zones(e.MistakeTC.Mistake).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (e.MistakeEconomy(res).Resources > 0)
                            {
                                mistakeUIE.NeedAmountResources(res).SetActive(true);

                                mistakeUIE.NeedAmountResources(res).TextUI.text
                                    = ">= " + Math.Round(e.MistakeEconomy(res).Resources, 2);
                            }
                        }
                    }
                }

                else
                {
                    mistakeUIE.Background.SetActive(true);
                    mistakeUIE.Zones(e.MistakeTC.Mistake).SetActive(true);

                    if (e.MistakeTimerC.Timer >= NEED_TIME_FOR_FADING)
                    {
                        e.MistakeTC.Mistake = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
