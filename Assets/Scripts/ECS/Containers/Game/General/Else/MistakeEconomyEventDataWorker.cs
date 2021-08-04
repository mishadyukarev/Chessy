using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine.Events;

namespace Assets.Scripts.Workers.Game.Else.Data
{
    internal struct MistakeEconomyEventDataWorker
    {
        private static EcsEntity _mistakeStepsEnt;
        private static EcsEntity _mistakeNeedOtherPlaceEnt;

        internal MistakeEconomyEventDataWorker(EcsWorld gameWorld)
        {
            _mistakeStepsEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));

            _mistakeNeedOtherPlaceEnt = gameWorld.NewEntity()
                .Replace(new UnityEventComponent(new UnityEvent()));
        }

        private static UnityEvent UnityEventStepsMistake => _mistakeStepsEnt.Get<UnityEventComponent>().UnityEvent;
        private static UnityEvent NeedOtherPlaceUnityEventMistake => _mistakeNeedOtherPlaceEnt.Get<UnityEventComponent>().UnityEvent;

        internal static void AddListenerStepMistake(UnityAction unityAction) => UnityEventStepsMistake.AddListener(unityAction);
        internal static void AddListenerNeedOtherPlaceMistake(UnityAction unityAction) => NeedOtherPlaceUnityEventMistake.AddListener(unityAction);

        internal static void InvokeStepsMistake() => UnityEventStepsMistake.Invoke();
        internal static void InvokeNeedOtherPlace() => NeedOtherPlaceUnityEventMistake.Invoke();
    }
}