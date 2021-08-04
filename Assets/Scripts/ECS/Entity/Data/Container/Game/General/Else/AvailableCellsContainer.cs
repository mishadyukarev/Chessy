//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.ECS.Components;
//using Leopotam.Ecs;
//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.Workers.Game.Else
//{
//    internal struct AvailableCellsContainer
//    {
//        private static EcsEntity _availableCellsSettingEnt;
//        private static EcsEntity _availableCellsShiftEnt;
//        private static EcsEntity _availableCellsSimpleAttackEnt;
//        private static EcsEntity _availableCellsUniqueAttackEnt;

//        internal AvailableCellsContainer(EcsWorld gameWorld)
//        {
//            _availableCellsSettingEnt = gameWorld.NewEntity()
//                .Replace(new AvailableCellsComponent(new List<int[]>()));
//            _availableCellsShiftEnt = gameWorld.NewEntity()
//                .Replace(new AvailableCellsComponent(new List<int[]>()));
//            _availableCellsSimpleAttackEnt = gameWorld.NewEntity()
//                .Replace(new AvailableCellsComponent(new List<int[]>()));
//            _availableCellsUniqueAttackEnt = gameWorld.NewEntity()
//                .Replace(new AvailableCellsComponent(new List<int[]>()));
//        }

        
//    }
//}