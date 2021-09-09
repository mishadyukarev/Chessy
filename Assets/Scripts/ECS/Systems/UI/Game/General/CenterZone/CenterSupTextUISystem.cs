using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
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


            if(mistakeDataUICom.MistakeTypes == MistakeTypes.None)
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

                    mistakeViewUICom.Text = "Need more resources";
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
                            mistakeViewUICom.Text = "Need set king";
                            break;

                        case MistakeTypes.NeedMoreSteps:
                            mistakeViewUICom.Text = "Need more steps";
                            break;

                        case MistakeTypes.NeedOtherPlace:
                            mistakeViewUICom.Text = "Need other place";
                            break;

                        case MistakeTypes.NeedMoreHealth:
                            mistakeViewUICom.Text = "Need more health";
                            break;

                        case MistakeTypes.PawnMustHaveTool:
                            mistakeViewUICom.Text = "Pawn must have tool";
                            break;

                        case MistakeTypes.PawnHaveTool:
                            mistakeViewUICom.Text = "Pawn have tool";
                            break;

                        case MistakeTypes.NeedCity:
                            mistakeViewUICom.Text = "Need set city";
                            break;

                        case MistakeTypes.ThisIsForOtherUnit:
                            mistakeViewUICom.Text = "This is for other unit";
                            break;

                        case MistakeTypes.NearTheDesert:
                            mistakeViewUICom.Text = "Near the desert";
                            break;

                        default:
                            throw new Exception();
                    }
                }
            }
        }
    }
}
