//using Assets.Scripts.ECS.Components;
//using Leopotam.Ecs;
//using UnityEngine.Events;

//namespace Assets.Scripts.Workers.Game.Else.Data
//{
//    internal struct MistakeEconomyEventDataWorker
//    {
//        private static EcsEntity _mistakeStepsEnt;
//        private static EcsEntity _mistakeNeedOtherPlaceEnt;

//        internal MistakeEconomyEventDataWorker(EcsWorld gameWorld)
//        {
//            _mistakeStepsEnt = gameWorld.NewEntity()
//                .Replace(new UnityEventComponent(new UnityEvent()));

//            _mistakeNeedOtherPlaceEnt = gameWorld.NewEntity()
//                .Replace(new UnityEventComponent(new UnityEvent()));
//        }

//    }
//}