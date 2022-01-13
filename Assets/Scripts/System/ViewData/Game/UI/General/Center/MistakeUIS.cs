using UnityEngine;
using static Game.Game.EntityUpUIPool;

namespace Game.Game
{
    class MistakeUIS : IEcsRunSystem
    {
        float _neededTimeForFading = 1.3f;

        public void Run()
        {
            if (EntMistakeC.Mistake<MistakeC>().Mistake == MistakeTypes.None)
            {
                ResetAll();
            }
            else
            {
                EntMistakeC.Mistake<TimerC>().Timer += Time.deltaTime;

                if (EntMistakeC.Mistake<MistakeC>().Mistake == MistakeTypes.Economy)
                {
                    if (EntMistakeC.Mistake<TimerC>().Timer >= _neededTimeForFading)
                    {
                        EntMistakeC.Mistake<TimerC>().Timer = 0;
                        //EntMistakeC.ResetMistakeType();
                        //EntMistakeC.ClearAllNeeds();

                        for (var res = ResTypes.First; res < ResTypes.End; res++)
                        {
                            Economy<EconomyUpUIC>(res).Color = Color.white;
                        }
                    }

                    else
                    {
                        for (var res = ResTypes.First; res < ResTypes.End; res++)
                        {
                            //if (EntMistakeC.NeedRes(res))
                            //{
                            //    Economy<EconomyUpUIC>(res).Color = Color.red;
                            //    MistakeUIC.SetActiveRes(res, true);
                            //    MistakeUIC.SetText(res, ">= " + (-EntMistakeC.NeedResAmount(res)).ToString());
                            //}
                            //else
                            //{
                            //    Economy<EconomyUpUIC>(res).Color = Color.white;
                            //    MistakeUIC.SetActiveRes(res, false);
                            //}
                        }
                    }
                }

                else
                {
                    ResetAll();

                    MistakeUIC.ActiveBackgroud(true);

                    //if (EntMistakeC.CurTime >= _neededTimeForFading)
                    //{
                    //    EntMistakeC.CurTime = 0;
                    //    EntMistakeC.ResetMistakeType();
                    //}

                    //switch (EntMistakeC.MistakeType)
                    //{
                    //    case MistakeTypes.None:
                    //        break;

                    //    case MistakeTypes.Economy:
                    //        throw new Exception();

                    //    case MistakeTypes.NeedMoreSteps:
                    //        MistakeUIC.ActiveNeedSteps(true);
                    //        break;

                    //    case MistakeTypes.NeedOtherPlace:
                    //        MistakeUIC.ActiveNeedOtherPlace(true);
                    //        break;

                    //    case MistakeTypes.NeedMoreHp:
                    //        MistakeUIC.ActiveNeedMoreHealth(true);
                    //        break;

                    //    case MistakeTypes.NeedCity:
                    //        MistakeUIC.ActiveNeedCity(true);
                    //        break;

                    //    case MistakeTypes.ThatIsForOtherUnit:
                    //        MistakeUIC.ActiveThatsForOtherUnit(true);
                    //        break;

                    //    case MistakeTypes.NearBorder:
                    //        MistakeUIC.ActiveNearBorderZone(true);
                    //        break;

                    //    default:
                    //        throw new Exception();
                    //}
                }
            }
        }

        void ResetAll()
        {
            MistakeUIC.ActiveBackgroud(false);

            MistakeUIC.ActiveTextZone(false);
            MistakeUIC.ActiveNeedSteps(false);
            MistakeUIC.ActiveNeedMoreHealth(false);
            MistakeUIC.ActiveNeedOtherPlace(false);
            MistakeUIC.ActiveNeedCity(false);
            MistakeUIC.ActiveThatsForOtherUnit(false);
            MistakeUIC.ActiveNearBorderZone(false);

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                Economy<EconomyUpUIC>(res).Color = Color.white;
                MistakeUIC.SetActiveRes(res, false);
            }
        }
    }
}
