using System;
using UnityEngine;

namespace Chessy.Game
{
    sealed class CenterMistakeUIS : SystemUIAbstract, IEcsRunSystem
    {
        float _neededTimeForFading = 1.3f;

        internal CenterMistakeUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {


            UIE.CenterEs.MistakeE.Background.SetActive(false);
            UIE.CenterEs.MistakeE.TextUIC.SetActive(false);


            foreach (var key in UIE.CenterEs.MistakeE.KeysMistake)
            {
                UIE.CenterEs.MistakeE.Zones(key).SetActive(false);
            }

            foreach (var key in UIE.CenterEs.MistakeE.KeysResource)
            {
                UIE.CenterEs.MistakeE.NeedAmountResources(key).SetActive(false);
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
                        UIE.CenterEs.MistakeE.Zones(E.MistakeTC.Mistake).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (E.MistakeEconomy(res).Resources > 0)
                            {
                                UIE.CenterEs.MistakeE.NeedAmountResources(res).SetActive(true);

                                UIE.CenterEs.MistakeE.NeedAmountResources(res).TextUI.text
                                    = ">= " + Math.Round(E.MistakeEconomy(res).Resources, 2);
                            }
                        }
                    }
                }

                else
                {
                    UIE.CenterEs.MistakeE.Background.SetActive(true);
                    UIE.CenterEs.MistakeE.Zones(E.MistakeTC.Mistake).SetActive(true);

                    if (E.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        E.MistakeTC.Mistake = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
