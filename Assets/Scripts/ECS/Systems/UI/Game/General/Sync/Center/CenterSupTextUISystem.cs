using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View.Down
{
    internal sealed class CenterSupTextUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<MistakeViewUICom> _mistakeUIFilter = default;

        private MistakeTypes _mistakeType;

        private float _neededTimeForFading = 1.3f;
        private float _currentTime;

        public void Init()
        {
            //MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Food, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            //MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Wood, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            //MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Ore, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            //MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Iron, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            //MainGameSystem.MistakeCom.AddListenerEconomyMistake(ResourceTypes.Gold, delegate { ExecuteMistakeText(MistakeTypes.Economy); });

            //MainGameSystem.MistakeCom.AddListenerStepMistake(delegate { ExecuteMistakeText(MistakeTypes.NeedSteps); });

            //MainGameSystem.MistakeCom.AddListenerNeedOtherPlaceMistake(delegate { ExecuteMistakeText(MistakeTypes.NeedOtherPlace); });
        }

        public void Run()
        {
            ref var mistakeViewUICom = ref _mistakeUIFilter.Get1(0);

            switch (_mistakeType)
            {
                case MistakeTypes.None:
                    mistakeViewUICom.SetActiveParent(false);
                    break;

                case MistakeTypes.Economy:
                    mistakeViewUICom.Text = "Need more resources";
                    mistakeViewUICom.SetActiveParent(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        mistakeViewUICom.SetActiveParent(false);
                        _mistakeType = default;
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
                        _mistakeType = default;
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
                        _mistakeType = default;
                    }
                    break;

                default:
                    throw new Exception();

            }
        }

        private void ExecuteMistakeText(MistakeTypes mistakeType)
        {
            _currentTime = default;
            _mistakeType = mistakeType;
        }
    }
}
