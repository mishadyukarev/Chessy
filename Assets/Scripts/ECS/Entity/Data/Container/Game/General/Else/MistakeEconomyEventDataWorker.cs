using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Workers.Game.Else.Data
{
    internal struct MistakeEconomyEventDataWorker
    {
        private static EcsEntity _mistakeFoodEnt;
        private static EcsEntity _mistakeWoodEnt;
        private static EcsEntity _mistakeOreEnt;
        private static EcsEntity _mistakeIronEnt;
        private static EcsEntity _mistakeGoldEnt;
        private static EcsEntity _mistakeStepsEnt;
        private static EcsEntity _mistakeNeedOtherPlaceEnt;

        internal MistakeEconomyEventDataWorker(EcsWorld gameWorld)
        {
            _mistakeFoodEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeWoodEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeOreEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeIronEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeGoldEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeStepsEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeNeedOtherPlaceEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));
        }

        private static UnityEvent GetUnityEventEconomyMistake(ResourceTypes resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypes.None:
                    throw new Exception();

                case ResourceTypes.Food:
                    return _mistakeFoodEnt.Get<UnityEventComponent>().UnityEvent;

                case ResourceTypes.Wood:
                    return _mistakeWoodEnt.Get<UnityEventComponent>().UnityEvent;

                case ResourceTypes.Ore:
                    return _mistakeOreEnt.Get<UnityEventComponent>().UnityEvent;

                case ResourceTypes.Iron:
                    return _mistakeIronEnt.Get<UnityEventComponent>().UnityEvent;

                case ResourceTypes.Gold:
                    return _mistakeGoldEnt.Get<UnityEventComponent>().UnityEvent;

                default:
                    throw new Exception();
            }
        }
        private static UnityEvent UnityEventStepsMistake => _mistakeStepsEnt.Get<UnityEventComponent>().UnityEvent;
        private static UnityEvent NeedOtherPlaceUnityEventMistake => _mistakeNeedOtherPlaceEnt.Get<UnityEventComponent>().UnityEvent;

        internal static void AddListenerEconomyMistake(ResourceTypes resourceType, UnityAction unityAction) => GetUnityEventEconomyMistake(resourceType).AddListener(unityAction);
        internal static void AddListenerStepMistake(UnityAction unityAction) => UnityEventStepsMistake.AddListener(unityAction);
        internal static void AddListenerNeedOtherPlaceMistake(UnityAction unityAction) => NeedOtherPlaceUnityEventMistake.AddListener(unityAction);

        internal static void InvokeEconomyMistake(ResourceTypes resourceType) => GetUnityEventEconomyMistake(resourceType).Invoke();
        internal static void InvokeStepsMistake() => UnityEventStepsMistake.Invoke();
        internal static void InvokeNeedOtherPlace() => NeedOtherPlaceUnityEventMistake.Invoke();
    }
}