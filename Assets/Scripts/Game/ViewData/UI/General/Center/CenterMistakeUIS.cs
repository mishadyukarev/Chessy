using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterMistakeUIS : SystemUIAbstract, IEcsRunSystem
    {
        float _neededTimeForFading = 1.3f;

        internal CenterMistakeUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {


            UIEs.CenterEs.MistakeE.Background.SetActive(false);
            UIEs.CenterEs.MistakeE.TextUIC.SetActive(false);


            foreach (var key in UIEs.CenterEs.MistakeE.KeysMistake)
            {
                UIEs.CenterEs.MistakeE.Zones(key).SetActive(false);
            }

            foreach (var key in UIEs.CenterEs.MistakeE.KeysResource)
            {
                UIEs.CenterEs.MistakeE.NeedAmountResources(key).SetActive(false);
            }



            if (E.MistakeTC.HaveMistake)
            {
                E.MistakeTimerC.Timer += Time.deltaTime;

                if (E.MistakeTC.Is(MistakeTypes.Economy))
                {
                    if (E.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        E.MistakeTC.Mistake = MistakeTypes.None;
                    }

                    else
                    {
                        UIEs.CenterEs.MistakeE.Zones(E.MistakeTC.Mistake).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (E.MistakeEconomy(res).Resources > 0)
                            {
                                UIEs.CenterEs.MistakeE.NeedAmountResources(res).SetActive(true);

                                UIEs.CenterEs.MistakeE.NeedAmountResources(res).TextUI.text
                                    = ">= " + Math.Round(E.MistakeEconomy(res).Resources, 2);
                            }
                        }
                    }
                }

                else
                {
                    UIEs.CenterEs.MistakeE.Background.SetActive(true);
                    UIEs.CenterEs.MistakeE.Zones(E.MistakeTC.Mistake).SetActive(true);

                    if (E.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        E.MistakeTC.Mistake = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
