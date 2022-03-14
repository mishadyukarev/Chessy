using System;
using UnityEngine;

namespace Chessy.Game
{
    static class CenterMistakeUIS
    {
        static float _neededTimeForFading = 1.3f;

        public static void Run(in float timer, in EntitiesViewUI eUI, in EntitiesModel e)
        {
            eUI.CenterEs.MistakeE.Background.SetActive(false);
            eUI.CenterEs.MistakeE.TextUIC.SetActive(false);


            foreach (var key in eUI.CenterEs.MistakeE.KeysMistake)
            {
                eUI.CenterEs.MistakeE.Zones(key).SetActive(false);
            }

            foreach (var key in eUI.CenterEs.MistakeE.KeysResource)
            {
                eUI.CenterEs.MistakeE.NeedAmountResources(key).SetActive(false);
            }



            if (e.MistakeTC.HaveMistake)
            {
                e.MistakeTimerC.Timer += Time.deltaTime + timer;

                if (e.MistakeTC.Is(MistakeTypes.Economy))
                {
                    if (e.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        e.MistakeTC.Mistake = MistakeTypes.None;
                    }

                    else
                    {
                        eUI.CenterEs.MistakeE.Zones(e.MistakeTC.Mistake).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (e.MistakeEconomy(res).Resources > 0)
                            {
                                eUI.CenterEs.MistakeE.NeedAmountResources(res).SetActive(true);

                                eUI.CenterEs.MistakeE.NeedAmountResources(res).TextUI.text
                                    = ">= " + Math.Round(e.MistakeEconomy(res).Resources, 2);
                            }
                        }
                    }
                }

                else
                {
                    eUI.CenterEs.MistakeE.Background.SetActive(true);
                    eUI.CenterEs.MistakeE.Zones(e.MistakeTC.Mistake).SetActive(true);

                    if (e.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        e.MistakeTC.Mistake = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
