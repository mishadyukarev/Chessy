using Leopotam.Ecs;
using Scripts.Common;
using System;
using UnityEngine;

namespace Scripts.Game
{
    internal sealed class CenterSupTextUISystem : IEcsRunSystem
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
                mistakeViewUICom.ActiveBackgroud(false);

                mistakeViewUICom.ActiveTextZone(false);
                mistakeViewUICom.ActiveNeedSteps(false);
                mistakeViewUICom.ActiveNeedMoreHealth(false);
                mistakeViewUICom.ActiveNeedOtherPlace(false);
                mistakeViewUICom.ActiveNeedCity(false);
                mistakeViewUICom.ActiveThatsForOtherUnit(false);
                mistakeViewUICom.ActiveNearBorderZone(false);
                mistakeViewUICom.ActiveNeedMoreResources(false);
            }
            else
            {
                if (mistakeDataUICom.MistakeTypes == MistakeTypes.Economy)
                {
                    for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                    {
                        if (mistakeDataUICom.GetNeedResources(resourceType))
                        {
                            _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.red);
                        }
                    }

                    mistakeViewUICom.ActiveNeedMoreResources(true);

                    mistakeDataUICom.CurrentTime += Time.deltaTime;

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
                }

                else
                {
                    mistakeViewUICom.ActiveBackgroud(true);

                    mistakeDataUICom.CurrentTime += Time.deltaTime;

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
    }
}
