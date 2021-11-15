using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Chessy.Game
{
    public sealed class MistakeUISys : IEcsRunSystem
    {
        private float _neededTimeForFading = 1.3f;

        public void Run()
        {
            if (MistakeC.MistakeType == MistakeTypes.None)
            {
                ResetAll();
            }
            else
            {
                MistakeC.CurTime += Time.deltaTime;

                if (MistakeC.MistakeType == MistakeTypes.Economy)
                {
                    if (MistakeC.CurTime >= _neededTimeForFading)
                    {
                        MistakeC.CurTime = 0;
                        MistakeC.ResetMistakeType();
                        MistakeC.ClearAllNeeds();

                        for (var res = ResTypes.First; res < ResTypes.End; res++)
                        {
                            EconomyViewUIC.SetMainColor(res, Color.white);
                        }
                    }

                    else
                    {
                        for (var res = ResTypes.First; res < ResTypes.End; res++)
                        {
                            if (MistakeC.NeedRes(res))
                            {
                                EconomyViewUIC.SetMainColor(res, Color.red);
                                MistakeViewUIC.SetActiveRes(res, true);
                                MistakeViewUIC.SetText(res, ">= " + (-MistakeC.NeedResAmount(res)).ToString());
                            }
                            else
                            {
                                EconomyViewUIC.SetMainColor(res, Color.white);
                                MistakeViewUIC.SetActiveRes(res, false);
                            }
                        }
                    }
                }

                else
                {
                    ResetAll();

                    MistakeViewUIC.ActiveBackgroud(true);

                    if (MistakeC.CurTime >= _neededTimeForFading)
                    {
                        MistakeC.CurTime = 0;
                        MistakeC.ResetMistakeType();
                    }

                    switch (MistakeC.MistakeType)
                    {
                        case MistakeTypes.None:
                            break;

                        case MistakeTypes.Economy:
                            throw new Exception();

                        case MistakeTypes.NeedMoreSteps:
                            MistakeViewUIC.ActiveNeedSteps(true);
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            MistakeViewUIC.ActiveNeedOtherPlace(true);
                            break;

                        case MistakeTypes.NeedMoreHp:
                            MistakeViewUIC.ActiveNeedMoreHealth(true);
                            break;

                        case MistakeTypes.NeedCity:
                            MistakeViewUIC.ActiveNeedCity(true);
                            break;

                        case MistakeTypes.ThatIsForOtherUnit:
                            MistakeViewUIC.ActiveThatsForOtherUnit(true);
                            break;

                        case MistakeTypes.NearBorder:
                            MistakeViewUIC.ActiveNearBorderZone(true);
                            break;

                        default:
                            throw new Exception();
                    }
                }
            }
        }

        private void ResetAll()
        {
            MistakeViewUIC.ActiveBackgroud(false);

            MistakeViewUIC.ActiveTextZone(false);
            MistakeViewUIC.ActiveNeedSteps(false);
            MistakeViewUIC.ActiveNeedMoreHealth(false);
            MistakeViewUIC.ActiveNeedOtherPlace(false);
            MistakeViewUIC.ActiveNeedCity(false);
            MistakeViewUIC.ActiveThatsForOtherUnit(false);
            MistakeViewUIC.ActiveNearBorderZone(false);

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                EconomyViewUIC.SetMainColor(res, Color.white);
                MistakeViewUIC.SetActiveRes(res, false);
            }
        }
    }
}
