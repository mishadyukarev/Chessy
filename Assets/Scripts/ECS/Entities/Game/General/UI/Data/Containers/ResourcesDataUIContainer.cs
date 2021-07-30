using Assets.Scripts.ECS.Components;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets.Scripts.ECS.Entities.Game.General.UI.Data.Containers
{
    internal sealed class ResourcesDataUIContainer
    {
        private EcsEntity _foodInfoEnt;
        internal ref AmountResourcesDictComponent FoodInfoEnt_AmountResourcesDictCom => ref _foodInfoEnt.Get<AmountResourcesDictComponent>();


        private EcsEntity _woodInfoEnt;
        internal ref AmountResourcesDictComponent WoodInfoEnt_AmountResourcesDictCom => ref _woodInfoEnt.Get<AmountResourcesDictComponent>();


        private EcsEntity _oreInfoEnt;
        internal ref AmountResourcesDictComponent OreInfoEnt_AmountResourcesDictCom => ref _oreInfoEnt.Get<AmountResourcesDictComponent>();


        private EcsEntity _ironInfoEnt;
        internal ref AmountResourcesDictComponent IronInfoEnt_AmountResourcesDictCom => ref _ironInfoEnt.Get<AmountResourcesDictComponent>();


        private EcsEntity _goldInfoEnt;
        internal ref AmountResourcesDictComponent GoldInfoEnt_AmountResourcesDictCom => ref _goldInfoEnt.Get<AmountResourcesDictComponent>();


        internal ResourcesDataUIContainer(EcsWorld gameWorld)
        {
            _foodInfoEnt = gameWorld.NewEntity()
                .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()))
                .Replace(new UnityEventComponent(new UnityEvent()));

            _woodInfoEnt = gameWorld.NewEntity()
                .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()))
                .Replace(new UnityEventComponent(new UnityEvent()));

            _oreInfoEnt = gameWorld.NewEntity()
                .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()))
                .Replace(new UnityEventComponent(new UnityEvent()));

            _ironInfoEnt = gameWorld.NewEntity()
                .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()))
                .Replace(new UnityEventComponent(new UnityEvent()));

            _goldInfoEnt = gameWorld.NewEntity()
                .Replace(new AmountResourcesDictComponent(new Dictionary<bool, int>()))
                .Replace(new UnityEventComponent(new UnityEvent()));
        }
    }
}
