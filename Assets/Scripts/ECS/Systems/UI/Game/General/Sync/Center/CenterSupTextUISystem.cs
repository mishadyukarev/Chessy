using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View.Down
{
    internal sealed class CenterSupTextUISystem : IEcsRunSystem
    {
        private EcsFilter<MistakeViewUICom, MistakeDataUICom> _mistakeUIFilter = default;
        private EcsFilter<EconomyViewUICom> _economyUIFilter = default;

        private float _neededTimeForFading = 1.3f;
        private float _currentTime;
        private bool _isStartedMistake = true;

        public void Run()
        {
            ref var mistakeViewUICom = ref _mistakeUIFilter.Get1(0);
            ref var mistakeDataUICom = ref _mistakeUIFilter.Get2(0);

            switch (mistakeDataUICom.MistakeTypes)
            {
                case MistakeTypes.None:
                    mistakeViewUICom.SetActiveParent(false);
                    break;

                case MistakeTypes.Economy:
                    for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                    {
                        if (mistakeDataUICom.GetNeedResources(resourceType))
                        {
                            _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.red);
                        }
                    }

                    mistakeViewUICom.Text = "Need more resources";
                    mistakeViewUICom.SetActiveParent(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        mistakeViewUICom.SetActiveParent(false);
                        mistakeDataUICom.ResetMistakeType();
                        mistakeDataUICom.ClearAllNeeds();

                        for (ResourceTypes resourceType = (ResourceTypes)1; resourceType < (ResourceTypes)Enum.GetNames(typeof(ResourceTypes)).Length; resourceType++)
                        {
                            _economyUIFilter.Get1(0).SetMainColor(resourceType, Color.white);
                        }
                    }
                    break;

                case MistakeTypes.NeedKing:
                    break;

                case MistakeTypes.NeedSteps:
                    mistakeViewUICom.Text = "Need more steps";
                    mistakeViewUICom.SetActiveParent(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        mistakeViewUICom.SetActiveParent(false);
                        mistakeDataUICom.ResetMistakeType();
                    }
                    break;

                case MistakeTypes.NeedOtherPlace:
                    mistakeViewUICom.Text = "Need other place";
                    mistakeViewUICom.SetActiveParent(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        mistakeViewUICom.SetActiveParent(false);
                        mistakeDataUICom.ResetMistakeType();
                    }
                    break;

                default:
                    throw new Exception();

            }
        }
    }
}
