using System;
using UnityEngine;

namespace Game.Game
{
    sealed class MistakeUIS : SystemUIAbstract, IEcsRunSystem
    {
        float _neededTimeForFading = 1.3f;

        internal MistakeUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            MistakeUIE.Background<GameObjectVC>().SetActive(false);
            MistakeUIE.Background<TextUIC>().SetActive(false);


            foreach (var key in MistakeUIE.KeysMistake)
            {
                MistakeUIE.Zones<GameObjectVC>(key).SetActive(false);
            }

            foreach (var key in MistakeUIE.KeysResource)
            {
                MistakeUIE.NeedAmountResources<TextUIC>(key).SetActive(false);
            }



            if (Es.MistakeTC.HaveMistake)
            {
                Es.MistakeTimerC.Timer += Time.deltaTime;

                if (Es.MistakeTC.Is(MistakeTypes.Economy))
                {
                    if (Es.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        Es.MistakeTC.Mistake = MistakeTypes.None;
                    }

                    else
                    {
                        MistakeUIE.Zones<GameObjectVC>(Es.MistakeTC.Mistake).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (Es.MistakeEconomy(res).Resources > 0)
                            {
                                MistakeUIE.NeedAmountResources<TextUIC>(res).SetActive(true);

                                MistakeUIE.NeedAmountResources<TextUIC>(res).Text
                                    = ">= " + Math.Round(Es.MistakeEconomy(res).Resources, 2);
                            }
                        }
                    }
                }

                else
                {
                    MistakeUIE.Background<GameObjectVC>().SetActive(true);
                    MistakeUIE.Zones<GameObjectVC>(Es.MistakeTC.Mistake).SetActive(true);

                    if (Es.MistakeTimerC.Timer >= _neededTimeForFading)
                    {
                        Es.MistakeTC.Mistake = MistakeTypes.None;
                    }
                }
            }
        }
    }
}
