using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class MistakeUISys : IEcsRunSystem
    {
        private EcsFilter<MistakeDataUICom, MistakeViewUICom> _mistakeUIFilter = default;
        private EcsFilter<EconomyViewUICom> _economyUIFilter = default;

        private float _neededTimeForFading = 1.3f;

        public void Run()
        {
            ref var mistDatUICom = ref _mistakeUIFilter.Get1(0);
            ref var mistakeViewUICom = ref _mistakeUIFilter.Get2(0);


            if (mistDatUICom.MistakeType == MistakeTypes.None)
            {
                ResetAll();
            }
            else
            {
                mistDatUICom.CurTime += Time.deltaTime;

                if (mistDatUICom.MistakeType == MistakeTypes.Economy)
                {
                    if (mistDatUICom.CurTime >= _neededTimeForFading)
                    {
                        mistDatUICom.CurTime = 0;
                        mistDatUICom.ResetMistakeType();
                        mistDatUICom.ClearAllNeeds();

                        for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
                        {
                            _economyUIFilter.Get1(0).SetMainColor(resType, Color.white);
                        }
                    }

                    else
                    {
                        for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
                        {
                            if (mistDatUICom.NeedRes(resType))
                            {
                                _economyUIFilter.Get1(0).SetMainColor(resType, Color.red);
                                mistakeViewUICom.SetActiveRes(resType, true);
                                mistakeViewUICom.SetText(resType, "<= " + (-mistDatUICom.NeedResAmount(resType)).ToString());
                            }
                            else
                            {
                                _economyUIFilter.Get1(0).SetMainColor(resType, Color.white);
                                mistakeViewUICom.SetActiveRes(resType, false);
                            }
                        }
                    }
                }

                else
                {
                    ResetAll();

                    mistakeViewUICom.ActiveBackgroud(true);

                    if (mistDatUICom.CurTime >= _neededTimeForFading)
                    {
                        mistDatUICom.CurTime = 0;
                        mistDatUICom.ResetMistakeType();
                    }

                    switch (mistDatUICom.MistakeType)
                    {
                        case MistakeTypes.None:
                            break;

                        case MistakeTypes.Economy:
                            throw new Exception();

                        case MistakeTypes.NeedMoreSteps:
                            mistakeViewUICom.ActiveNeedSteps(true);
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            mistakeViewUICom.ActiveNeedOtherPlace(true);
                            break;

                        case MistakeTypes.NeedMoreHealth:
                            mistakeViewUICom.ActiveNeedMoreHealth(true);
                            break;

                        case MistakeTypes.NeedCity:
                            mistakeViewUICom.ActiveNeedCity(true);
                            break;

                        case MistakeTypes.ThatIsForOtherUnit:
                            mistakeViewUICom.ActiveThatsForOtherUnit(true);
                            break;

                        case MistakeTypes.NearBorder:
                            mistakeViewUICom.ActiveNearBorderZone(true);
                            break;

                        default:
                            throw new Exception();
                    }
                }
            }
        }

        private void ResetAll()
        {
            ref var mistakeViewUICom = ref _mistakeUIFilter.Get2(0);

            mistakeViewUICom.ActiveBackgroud(false);

            mistakeViewUICom.ActiveTextZone(false);
            mistakeViewUICom.ActiveNeedSteps(false);
            mistakeViewUICom.ActiveNeedMoreHealth(false);
            mistakeViewUICom.ActiveNeedOtherPlace(false);
            mistakeViewUICom.ActiveNeedCity(false);
            mistakeViewUICom.ActiveThatsForOtherUnit(false);
            mistakeViewUICom.ActiveNearBorderZone(false);

            for (var resType = Support.MinResType; resType < Support.MaxResType; resType++)
            {
                _economyUIFilter.Get1(0).SetMainColor(resType, Color.white);
                mistakeViewUICom.SetActiveRes(resType, false);
            }
        }
    }
}
