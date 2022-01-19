using UnityEngine;

namespace Game.Game
{
    class MistakeUIS : IEcsRunSystem
    {
        float _neededTimeForFading = 1.3f;

        public void Run()
        {
            MistakeUIE.Background<GameObjectVC>().SetActive(false);
            MistakeUIE.Background<TextMPUGUIC>().SetActive(false);


            foreach (var key in MistakeUIE.KeysMistake)
            {
                MistakeUIE.Zones<GameObjectVC>(key).SetActive(false);
            }

            foreach (var key in MistakeUIE.KeysResource)
            {
                MistakeUIE.NeedAmountResources<TextMPUGUIC>(key).SetActive(false);
            }



            if (MistakeE.Mistake<MistakeC>().Mistake != MistakeTypes.None)
            {
                MistakeE.Mistake<TimerC>().Timer += Time.deltaTime;

                if (MistakeE.Mistake<MistakeC>().Mistake == MistakeTypes.Economy)
                {
                    if (MistakeE.Mistake<TimerC>().Timer >= _neededTimeForFading)
                    {
                        MistakeE.Mistake<TimerC>().Timer = 0;
                        MistakeE.Mistake<MistakeC>().Reset();

                        //for (var res = ResTypes.First; res < ResTypes.End; res++)
                        //{
                        //    Economy<TextMPUGUIC>(res).Color = Color.white;
                        //}
                    }

                    else
                    {
                        MistakeUIE.Zones<GameObjectVC>(MistakeE.Mistake<MistakeC>().Mistake).SetActive(true);

                        for (var res = ResourceTypes.First; res < ResourceTypes.End; res++)
                        {
                            if (MistakeE.Mistake<AmountC>(res).Have)
                            {
                                MistakeUIE.NeedAmountResources<TextMPUGUIC>(res).SetActive(true);

                                MistakeUIE.NeedAmountResources<TextMPUGUIC>(res).Text
                                    = ">= " + MistakeE.Mistake<AmountC>(ResourceTypes.Food).Amount;

                                //Economy<EconomyUpUIC>(res).Color = Color.red;
                                //MistakeUIC.SetActiveRes(res, true);
                                //MistakeUIC.SetText(res, ">= " + (-EntMistakeC.NeedResAmount(res)).ToString());
                            }
                            else
                            {
                                //Economy<EconomyUpUIC>(res).Color = Color.white;
                                //MistakeUIC.SetActiveRes(res, false);
                            }
                        }
                    }
                }

                else
                {
                    MistakeUIE.Background<GameObjectVC>().SetActive(true);
                    MistakeUIE.Zones<GameObjectVC>(MistakeE.Mistake<MistakeC>().Mistake).SetActive(true);

                    if (MistakeE.Mistake<TimerC>().Timer >= _neededTimeForFading)
                    {
                        MistakeE.Mistake<TimerC>().Timer = 0;
                        MistakeE.Mistake<MistakeC>().Reset();
                    }
                }
            }
        }
    }
}
