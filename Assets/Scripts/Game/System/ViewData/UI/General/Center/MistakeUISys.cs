﻿using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Game.Game
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
                            EconomyUIC.SetMainColor(res, Color.white);
                        }
                    }

                    else
                    {
                        for (var res = ResTypes.First; res < ResTypes.End; res++)
                        {
                            if (MistakeC.NeedRes(res))
                            {
                                EconomyUIC.SetMainColor(res, Color.red);
                                MistakeUIC.SetActiveRes(res, true);
                                MistakeUIC.SetText(res, ">= " + (-MistakeC.NeedResAmount(res)).ToString());
                            }
                            else
                            {
                                EconomyUIC.SetMainColor(res, Color.white);
                                MistakeUIC.SetActiveRes(res, false);
                            }
                        }
                    }
                }

                else
                {
                    ResetAll();

                    MistakeUIC.ActiveBackgroud(true);

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
                            MistakeUIC.ActiveNeedSteps(true);
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            MistakeUIC.ActiveNeedOtherPlace(true);
                            break;

                        case MistakeTypes.NeedMoreHp:
                            MistakeUIC.ActiveNeedMoreHealth(true);
                            break;

                        case MistakeTypes.NeedCity:
                            MistakeUIC.ActiveNeedCity(true);
                            break;

                        case MistakeTypes.ThatIsForOtherUnit:
                            MistakeUIC.ActiveThatsForOtherUnit(true);
                            break;

                        case MistakeTypes.NearBorder:
                            MistakeUIC.ActiveNearBorderZone(true);
                            break;

                        default:
                            throw new Exception();
                    }
                }
            }
        }

        private void ResetAll()
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
                EconomyUIC.SetMainColor(res, Color.white);
                MistakeUIC.SetActiveRes(res, false);
            }
        }
    }
}
