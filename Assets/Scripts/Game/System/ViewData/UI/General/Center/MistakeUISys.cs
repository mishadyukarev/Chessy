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
            if (MistakeDataUIC.MistakeType == MistakeTypes.None)
            {
                ResetAll();
            }
            else
            {
                MistakeDataUIC.CurTime += Time.deltaTime;

                if (MistakeDataUIC.MistakeType == MistakeTypes.Economy)
                {
                    if (MistakeDataUIC.CurTime >= _neededTimeForFading)
                    {
                        MistakeDataUIC.CurTime = 0;
                        MistakeDataUIC.ResetMistakeType();
                        MistakeDataUIC.ClearAllNeeds();

                        for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
                        {
                            EconomyViewUIC.SetMainColor(resType, Color.white);
                        }
                    }

                    else
                    {
                        for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
                        {
                            if (MistakeDataUIC.NeedRes(resType))
                            {
                                EconomyViewUIC.SetMainColor(resType, Color.red);
                                MistakeViewUIC.SetActiveRes(resType, true);
                                MistakeViewUIC.SetText(resType, ">= " + (-MistakeDataUIC.NeedResAmount(resType)).ToString());
                            }
                            else
                            {
                                EconomyViewUIC.SetMainColor(resType, Color.white);
                                MistakeViewUIC.SetActiveRes(resType, false);
                            }
                        }
                    }
                }

                else
                {
                    ResetAll();

                    MistakeViewUIC.ActiveBackgroud(true);

                    if (MistakeDataUIC.CurTime >= _neededTimeForFading)
                    {
                        MistakeDataUIC.CurTime = 0;
                        MistakeDataUIC.ResetMistakeType();
                    }

                    switch (MistakeDataUIC.MistakeType)
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

            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                EconomyViewUIC.SetMainColor(resType, Color.white);
                MistakeViewUIC.SetActiveRes(resType, false);
            }
        }
    }
}
