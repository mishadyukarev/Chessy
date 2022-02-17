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



            if (Es.MistakeC.HaveMistake)
            {
                Es.TimerC.Timer += Time.deltaTime;

                if (Es.MistakeC.Is(MistakeTypes.Economy))
                {
                    if (Es.TimerC.Timer >= _neededTimeForFading)
                    {
                        Es.MistakeC.Set(MistakeTypes.None);
                    }

                    else
                    {
                        MistakeUIE.Zones<GameObjectVC>(Es.MistakeC.Mistake).SetActive(true);

                        for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
                        {
                            if (Es.MistakeEconomy(res).Resources > 0)
                            {
                                MistakeUIE.NeedAmountResources<TextUIC>(res).SetActive(true);

                                MistakeUIE.NeedAmountResources<TextUIC>(res).Text
                                    = ">= " + (int)(Es.MistakeEconomy(res).Resources * 100);
                            }
                        }
                    }
                }

                else
                {
                    MistakeUIE.Background<GameObjectVC>().SetActive(true);
                    MistakeUIE.Zones<GameObjectVC>(Es.MistakeC.Mistake).SetActive(true);

                    if (Es.TimerC.Timer >= _neededTimeForFading)
                    {
                        Es.MistakeC.Set(MistakeTypes.None);
                    }
                }
            }
        }
    }
}
