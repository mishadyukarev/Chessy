using Assets.Scripts.Workers.Game.Else.Data;
using Assets.Scripts.Workers.Game.UI.Middle.MistakeInfo;
using Leopotam.Ecs;
using System;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems.Game.General.UI.View.Down
{
    internal sealed class CenterSupTextUISystem : IEcsInitSystem, IEcsRunSystem
    {
        private MistakeTypes _mistakeType;

        private float _neededTimeForFading = 1.3f;
        private float _currentTime;

        public void Init()
        {
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Food, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Wood, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Ore, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Iron, delegate { ExecuteMistakeText(MistakeTypes.Economy); });
            MistakeEconomyEventDataWorker.AddListenerEconomyMistake(ResourceTypes.Gold, delegate { ExecuteMistakeText(MistakeTypes.Economy); });

            MistakeEconomyEventDataWorker.AddListenerStepMistake(delegate { ExecuteMistakeText(MistakeTypes.NeedSteps); });

            MistakeEconomyEventDataWorker.AddListenerNeedOtherPlaceMistake(delegate { ExecuteMistakeText(MistakeTypes.NeedOtherPlace); });
        }

        public void Run()
        {
            switch (_mistakeType)
            {
                case MistakeTypes.None:
                    CenterSupTextUIViewWorker.SetActiveCenterBlock(false);
                    break;

                case MistakeTypes.Economy:
                    CenterSupTextUIViewWorker.Text = "Need more resources";
                    CenterSupTextUIViewWorker.SetActiveCenterBlock(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        CenterSupTextUIViewWorker.SetActiveCenterBlock(false);
                        _mistakeType = default;
                    }
                    break;

                case MistakeTypes.NeedKing:
                    break;

                case MistakeTypes.NeedSteps:
                    CenterSupTextUIViewWorker.Text = "Need more steps";
                    CenterSupTextUIViewWorker.SetActiveCenterBlock(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        CenterSupTextUIViewWorker.SetActiveCenterBlock(false);
                        _mistakeType = default;
                    }
                    break;

                case MistakeTypes.NeedOtherPlace:
                    CenterSupTextUIViewWorker.Text = "Need other place";
                    CenterSupTextUIViewWorker.SetActiveCenterBlock(true);

                    _currentTime += Time.deltaTime;

                    if (_currentTime >= _neededTimeForFading)
                    {
                        _currentTime = 0;
                        CenterSupTextUIViewWorker.SetActiveCenterBlock(false);
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
