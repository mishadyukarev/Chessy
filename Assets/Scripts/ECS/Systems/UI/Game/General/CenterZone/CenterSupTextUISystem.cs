using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Common;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View.Down
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
                mistakeViewUICom.SetActiveParent(false);
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

                    mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NeedMoreResources);
                    mistakeViewUICom.SetActiveParent(true);

                    mistakeDataUICom.CurrentTime += Time.deltaTime;

                    if (mistakeDataUICom.CurrentTime >= _neededTimeForFading)
                    {
                        mistakeDataUICom.CurrentTime = 0;
                        mistakeViewUICom.SetActiveParent(false);
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
                    mistakeViewUICom.SetActiveParent(true);

                    mistakeDataUICom.CurrentTime += Time.deltaTime;

                    if (mistakeDataUICom.CurrentTime >= _neededTimeForFading)
                    {
                        mistakeDataUICom.CurrentTime = 0;
                        mistakeViewUICom.SetActiveParent(false);
                        mistakeDataUICom.ResetMistakeType();
                    }

                    switch (mistakeDataUICom.MistakeTypes)
                    {
                        case MistakeTypes.None:
                            break;

                        case MistakeTypes.Economy:
                            throw new Exception();

                        case MistakeTypes.NeedKing:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NeedSetKing);
                            break;

                        case MistakeTypes.NeedMoreSteps:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NeedMoreSteps);
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NeedOtherPlace); 
                            break;

                        case MistakeTypes.NeedMoreHealth:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NeedMoreHealth); 
                            break;

                        case MistakeTypes.PawnMustHaveTool:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.PawnMustHaveTool); 
                            break;

                        case MistakeTypes.PawnHaveTool:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.PawnHaveTool); 
                            break;

                        case MistakeTypes.NeedCity:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NeedSetCity);
                            break;

                        case MistakeTypes.ThatIsForOtherUnit:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.ThatsForOtherUnit); 
                            break;

                        case MistakeTypes.NearBorder:
                            mistakeViewUICom.Text = LanguageComComp.GetText(GameLanguageTypes.NearBorder); 
                            break;

                        default:
                            throw new Exception();
                    }
                }
            }
        }
    }
}
