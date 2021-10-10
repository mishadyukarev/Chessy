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
            ref var mistakeDataUICom = ref _mistakeUIFilter.Get1(0);
            ref var mistakeViewUICom = ref _mistakeUIFilter.Get2(0);


            if (mistakeDataUICom.MistakeTypes == MistakeTypes.None)
            {
                ResetAll();
            }
            else
            {
                mistakeDataUICom.CurrentTime += Time.deltaTime;

                if (mistakeDataUICom.MistakeTypes == MistakeTypes.Economy)
                {
                    mistakeViewUICom.ActiveNeedMoreResources(true);          

                    if (mistakeDataUICom.CurrentTime >= _neededTimeForFading)
                    {
                        mistakeDataUICom.CurrentTime = 0;
                        mistakeDataUICom.ResetMistakeType();
                        mistakeDataUICom.ClearAllNeeds();

                        for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                        {
                            _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.white);
                        }
                    }

                    else
                    {
                        for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                        {
                            if (mistakeDataUICom.GetNeedResources(resourceType))
                            {
                                _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.red);
                            }
                            else
                            {
                                _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.white);
                            }
                        }
                    }
                }

                else
                {
                    ResetAll();

                    mistakeViewUICom.ActiveBackgroud(true);

                    if (mistakeDataUICom.CurrentTime >= _neededTimeForFading)
                    {
                        mistakeDataUICom.CurrentTime = 0;
                        mistakeDataUICom.ResetMistakeType();
                    }

                    switch (mistakeDataUICom.MistakeTypes)
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
            mistakeViewUICom.ActiveNeedMoreResources(false);

            for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
            {
                _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.white);
            }
        }
    }
}
