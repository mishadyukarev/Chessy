using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Entities.Game.General.Else.Data.Containers
{
    internal sealed class MistakeEconomyEventDataContainer
    {
        private EcsEntity _mistakeFoodEnt;
        internal ref UnityEventComponent MistakeFoodEventEnt_UnityEventCom => ref _mistakeFoodEnt.Get<UnityEventComponent>();


        private EcsEntity _mistakeWoodEnt;
        internal ref UnityEventComponent MistakeWoodEventEnt_UnityEventCom => ref _mistakeWoodEnt.Get<UnityEventComponent>();


        private EcsEntity _mistakeOreEnt;
        internal ref UnityEventComponent MistakeOreEventEnt_UnityEventCom => ref _mistakeOreEnt.Get<UnityEventComponent>();


        private EcsEntity _mistakeIronEnt;
        internal ref UnityEventComponent MistakeIronEventEnt_UnityEventCom => ref _mistakeIronEnt.Get<UnityEventComponent>();


        private EcsEntity _mistakeGoldEnt;
        internal ref UnityEventComponent MistakeGoldEventEnt_UnityEventCom => ref _mistakeGoldEnt.Get<UnityEventComponent>();


        internal MistakeEconomyEventDataContainer(EcsWorld gameWorld)
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
        }
    }
}
