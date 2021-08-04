//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.ECS.Components;
//using Leopotam.Ecs;
//using System;
//using System.Collections.Generic;

//namespace Assets.Scripts.Workers.Game.Else.Info.Units
//{
//    internal struct InitSystem.XyUnitsContitionCom
//    {
//        private static EcsEntity _kingInfoEnt;
//        private static EcsEntity _pawnInfoEnt;
//        private static EcsEntity _pawnSwordInfoEnt;
//        private static EcsEntity _rookInfoEnt;
//        private static EcsEntity _rookCrossbowInfoEnt;
//        private static EcsEntity _bishopInfoInGameEnt;
//        private static EcsEntity _bishopCrossbowInfoEnt;

//        internal InitSystem.XyUnitsContitionCom(EcsWorld gameWorld)
//        {
//            _kingInfoEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

//            _pawnInfoEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

//            _pawnSwordInfoEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

//            _rookInfoEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

//            _rookCrossbowInfoEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

//            _bishopInfoInGameEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));

//            _bishopCrossbowInfoEnt = gameWorld.NewEntity()
//                .Replace(new XyUnitsInConditionComponent((new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>(), new Dictionary<bool, List<int[]>>())));
//        }

//        #region Condition


//        #endregion
//    }
//}
